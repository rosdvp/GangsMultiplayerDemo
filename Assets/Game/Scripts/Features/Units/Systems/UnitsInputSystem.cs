using Blah.Ecs;
using Blah.Pools;
using Blah.Systems;
using Game.Features.Bizs;
using Game.Features.Units.Commands;
using Game.Globals.Context;
using UnityEngine;

namespace Game.Features.Units
{
public class UnitsInputSystem : IBlahInitSystem, IBlahRunSystem
{
	private IBlahSignalProducer<UnitsMoveCmd> _unitsMoveCmd;
	
	private BlahEcsFilter<BizComp> _bizsFilter;


	private int    _currPlayer;
	private Camera _camera;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private const float BIZ_POS_OFFSET_Y  = -1f;
	private const float VALID_DIST_TO_BIZ = 1f;

	private BlahEcsEntity? _fromBizEnt;
	
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
		_currPlayer = context.CurrPlayer;

		_camera = Camera.main;
	}

	public void Run()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (TryGetBizCloseToCursor(true, out var ent))
			{
				_fromBizEnt = ent;
			}
			else
				_fromBizEnt = null;
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (_fromBizEnt != null && 
			    TryGetBizCloseToCursor(false, out var ent) &&
			    _fromBizEnt != ent)
			{
				_unitsMoveCmd.Add().Fill(_fromBizEnt.Value, ent);
			}
			else
			{
				_fromBizEnt = null;
			}
		}
	}
	
	private bool TryGetBizCloseToCursor(bool onlyCurrPlayer, out BlahEcsEntity bestEnt)
	{
		var targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
		targetPos.y += BIZ_POS_OFFSET_Y;
		targetPos.z =  0;
		
		bestEnt = default;
		var bestDist = float.MaxValue;

		foreach (var ent in _bizsFilter)
		{
			ref var biz = ref ent.Get<BizComp>();
			if (onlyCurrPlayer && biz.OwnerPlayer != _currPlayer)
				continue;
			
			var   pos  = biz.View.transform.position;
			float dist = Vector3.Distance(targetPos, pos);

			if (dist < bestDist)
			{
				bestEnt  = ent;
				bestDist = dist;
			}
		}
		return bestDist <= VALID_DIST_TO_BIZ;
	}
}
}