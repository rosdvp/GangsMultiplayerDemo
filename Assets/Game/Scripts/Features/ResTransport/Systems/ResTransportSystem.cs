using Blah.Ecs;
using Blah.Pools;
using Blah.Systems;
using Game.Features.Bizs;
using Game.Features.Network;
using Game.Globals.Context;
using Game.Types;

namespace Game.Features.ResTransport
{
public class ResTransportSystem : IBlahInitSystem, IBlahRunSystem
{
	//private IBlahSignalConsumer<ResTransportCmd> _transportCmd;
	private IBlahSignalNextFrameConsumer<ResTransportApplyCmd> _transportCmd;
	
	private IBlahSignalProducer<BizResChangedEv> _bizResChangedEv;

	
	private BlahEcsFilter<BizComp> _bizsFilter;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
	}

	public void Run()
	{
		foreach (ref var cmd in _transportCmd)
		{
			ref var fromBiz = ref cmd.FromBizEnt.Get<BizComp>();
			ref var toBiz   = ref cmd.ToBizEnt.Get<BizComp>();
			
			foreach (var resType in fromBiz.Storage.Types)
				if (toBiz.Type == EBizType.Mansion || toBiz.Storage.IsAllowed(resType))
				{
					var res = fromBiz.Storage.Get(resType);

					fromBiz.Storage.Change(-res);
					_bizResChangedEv.AddFull(cmd.FromBizEnt, res.Type);
					
					toBiz.Storage.Change(res);
					_bizResChangedEv.AddFull(cmd.ToBizEnt, res.Type);
				}
		}
	}
}
}