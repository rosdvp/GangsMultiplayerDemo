using Blah.Ecs;
using Blah.Pools;
using Blah.Systems;
using Game.Globals.Context;
using Game.Helpers;
using UnityEngine;

namespace Game.Features.Bizs
{
public class BizGenSystem : IBlahInitSystem, IBlahRunSystem
{
	private IBlahSignalProducer<BizResChangedEv> _bizResChangedEv;
	private IBlahSignalProducer<BizGenStartedEv> _bizGenStartedEv;
	
	private BlahEcsFilter<BizComp> _bizsFilter;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
	}

	public void Run()
	{
		foreach (var ent in _bizsFilter)
		{
			ref var biz = ref ent.Get<BizComp>();
			if (biz.OwnerPlayer == 0)
				continue;
			
			if (biz.Config.ProducingRes.Length == 0)
				continue;
			
			if (Time.time < biz.NextGenTime)
				continue;

			if (biz.IsGenStarted)
			{
				foreach (var res in biz.Config.ProducingRes)
				{
					biz.Storage.Change(res);

					_bizResChangedEv.AddFull(ent, res.Type);
				}
				biz.IsGenStarted = false;
			}

			var canStartGen = true;
			for (var i = 0; i < biz.Config.ConsumingRes.Length; i++)
				if (!biz.Storage.Has(biz.Config.ConsumingRes[i]))
				{
					canStartGen = false;
					break;
				}
			
			if (!canStartGen)
				continue;

			foreach (var res in biz.Config.ConsumingRes)
			{
				biz.Storage.Change(-res);

				_bizResChangedEv.AddFull(ent, res.Type);
			}

			biz.IsGenStarted = true;
			biz.NextGenTime  = Time.time + biz.Config.GenIntervalSecs;

			_bizGenStartedEv.Add().Ent = ent;
		}
	}
}
}