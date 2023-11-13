using Blah.Ecs;
using Blah.Ordering.Attributes;
using Blah.Pools;
using Blah.Systems;
using Game.Configs;
using Game.Features.Bizs;
using Game.Globals.Context;
using Game.Types;

namespace Game.Features.Basement
{
[BlahAfter(typeof(BizCreateSystem))]
public class BasementResSellSystem : IBlahInitSystem, IBlahRunSystem
{
	private IBlahSignalNextFrameConsumer<BasementResSellApplyCmd> _sellCmd;

	private IBlahSignalProducer<BizResChangedEv> _bizResChangedEv;
	
	private BlahEcsFilter<BizComp> _bizsFilter;

	private ConfigResources _configResources;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
		_configResources = context.Configs.Resources;

		context.View.ResSellButton.Init(
			context.Blah.Pools.GetSignalProducer<BasementResSellCmd>()
		);
	}

	public void Run()
	{
		foreach (ref var cmd in _sellCmd)
		{
			foreach (var ent in _bizsFilter)
			{
				ref var biz = ref ent.Get<BizComp>();
				if (biz.Type != EBizType.Mansion || biz.OwnerPlayer != cmd.Player)
					continue;

				foreach (var resType in biz.Storage.Types)
					if (resType != EResourceType.Money)
					{
						var    res         = biz.Storage.Get(resType);
						double price       = _configResources.GetPrice(resType);
						double totalIncome = res.Amount * price;

						biz.Storage.Change(-res);
						biz.Storage.Change(new Resource(EResourceType.Money, totalIncome));

						_bizResChangedEv.AddFull(ent, resType);
					}
				_bizResChangedEv.AddFull(ent, EResourceType.Money);
			}
		}
	}
}
}