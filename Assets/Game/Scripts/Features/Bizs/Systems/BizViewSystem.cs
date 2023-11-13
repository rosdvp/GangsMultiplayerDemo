using Blah.Ecs;
using Blah.Ordering.Attributes;
using Blah.Pools;
using Blah.Systems;
using Game.Globals.Context;
using UnityEngine;

namespace Game.Features.Bizs
{
[BlahAfter(typeof(BizCreateSystem))]
public class BizViewSystem : IBlahInitSystem, IBlahRunSystem
{
	private IBlahSignalConsumer<BizOwnerChangedEv> _bizOwnerChangedEv;
	private IBlahSignalConsumer<BizResChangedEv>   _bizResChangedEv;
	private IBlahSignalConsumer<BizGenStartedEv>   _bizGenStartedEv;
	private IBlahSignalConsumer<BizUnitsChangedEv> _bizUnitsChangedEv;
	private IBlahSignalConsumer<BizFightStartedEv> _bizFightStartedEv;

	private BlahEcsFilter<BizComp> _bizsFilter;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;


		foreach (var ent in _bizsFilter)
		{
			ref var biz = ref ent.Get<BizComp>();
			
			biz.View.Init(context.Assets);
			biz.View.SetBiz(biz.Type);
			biz.View.SetOwnerPlayer(biz.OwnerPlayer);
			biz.View.SetUnitsCount(biz.OwnerPlayerUnitsCount, biz.AttackingPlayerUnitsCount);
			
			foreach (var resType in biz.Storage.Types)
				biz.View.SetRes(biz.Storage.Get(resType));
		}
	}

	public void Run()
	{
		foreach (ref var ev in _bizOwnerChangedEv)
		{
			ref var biz = ref ev.Ent.Get<BizComp>();
			biz.View.SetOwnerPlayer(biz.OwnerPlayer);
		}

		foreach (ref var ev in _bizResChangedEv)
		{
			ref var biz = ref ev.Ent.Get<BizComp>();
			biz.View.SetRes(biz.Storage.Get(ev.ResType));
		}

		foreach (ref var ev in _bizGenStartedEv)
		{
			ref var biz = ref ev.Ent.Get<BizComp>();
			biz.View.StartResGenAnim(biz.NextGenTime - Time.time);
		}

		foreach (ref var ev in _bizUnitsChangedEv)
		{
			ref var biz = ref ev.Ent.Get<BizComp>();
			biz.View.SetUnitsCount(biz.OwnerPlayerUnitsCount, biz.AttackingPlayerUnitsCount);
		}

		foreach (ref var ev in _bizFightStartedEv)
		{
			ref var biz = ref ev.Ent.Get<BizComp>();
			biz.View.StartFightAnim(biz.WinningPlayer, biz.FightEndTime - Time.time);
		}
	}
}
}