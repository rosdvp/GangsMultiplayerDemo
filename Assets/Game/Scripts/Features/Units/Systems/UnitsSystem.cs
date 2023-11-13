using Blah.Ecs;
using Blah.Pools;
using Blah.Systems;
using Game.Configs;
using Game.Features.Bizs;
using Game.Features.Units.Commands;
using Game.Globals.Context;
using UnityEngine;

namespace Game.Features.Units
{
public class UnitsSystem : IBlahInitSystem, IBlahRunSystem
{
	private IBlahSignalNextFrameConsumer<UnitsMoveApplyCmd> _unitsMoveCmd;

	private IBlahSignalProducer<BizUnitsChangedEv> _unitsChangedEv;
	private IBlahSignalProducer<BizFightStartedEv> _fightStartedEv;
	private IBlahSignalProducer<BizOwnerChangedEv> _ownerChangedEv;


	private BlahEcsFilter<BizComp> _bizsFilter;

	private ConfigCommon _configCommon;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
		_configCommon = context.Configs.Common;
	}

	public void Run()
	{
		foreach (ref var cmd in _unitsMoveCmd)
		{
			ref var fromBiz = ref cmd.FromBizEnt.Get<BizComp>();
			ref var toBiz   = ref cmd.ToBizEnt.Get<BizComp>();

			if (fromBiz.IsFighting)
				continue;
			if (fromBiz.OwnerPlayerUnitsCount == 0)
				continue;

			if (fromBiz.OwnerPlayer == toBiz.OwnerPlayer)
			{
				toBiz.OwnerPlayerUnitsCount += fromBiz.OwnerPlayerUnitsCount;
			}
			else
			{
				if (toBiz.IsFighting && toBiz.AttackingPlayer != fromBiz.OwnerPlayer)
					continue;

				toBiz.AttackingPlayer           =  fromBiz.OwnerPlayer;
				toBiz.AttackingPlayerUnitsCount += fromBiz.OwnerPlayerUnitsCount;
				
				int winningPlayer = toBiz.OwnerPlayerUnitsCount >= toBiz.AttackingPlayerUnitsCount
					? toBiz.OwnerPlayer
					: toBiz.AttackingPlayer;

				if (toBiz.IsFighting)
				{
					if (winningPlayer != toBiz.WinningPlayer)
					{
						toBiz.WinningPlayer = winningPlayer;
						toBiz.FightEndTime  = Time.time + _configCommon.BizFightDuration;

						_fightStartedEv.Add().Ent = cmd.ToBizEnt;
					}
				}
				else
				{
					toBiz.IsFighting      = true;
					toBiz.WinningPlayer   = winningPlayer;
					toBiz.FightEndTime    = Time.time + _configCommon.BizFightDuration;

					_fightStartedEv.Add().Ent = cmd.ToBizEnt;
				}
			}
			fromBiz.OwnerPlayerUnitsCount = 0;
			
			_unitsChangedEv.Add().Ent = cmd.FromBizEnt;
			_unitsChangedEv.Add().Ent = cmd.ToBizEnt;
		}

		foreach (var ent in _bizsFilter)
		{
			ref var biz = ref ent.Get<BizComp>();
			if (!biz.IsFighting || biz.FightEndTime > Time.time)
				continue;
			if (biz.WinningPlayer == biz.AttackingPlayer)
			{
				biz.OwnerPlayer           = biz.AttackingPlayer;
				biz.OwnerPlayerUnitsCount = biz.AttackingPlayerUnitsCount - biz.OwnerPlayerUnitsCount;

				_ownerChangedEv.Add().Ent = ent;
			}
			else
			{
				biz.OwnerPlayerUnitsCount -= biz.AttackingPlayerUnitsCount;
			}
				
			biz.AttackingPlayerUnitsCount = 0;
			biz.IsFighting                = false;

			_unitsChangedEv.Add().Ent = ent;
		}
	}
}
}