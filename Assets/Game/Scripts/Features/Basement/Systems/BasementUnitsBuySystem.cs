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
public class BasementUnitsBuySystem : IBlahInitSystem, IBlahRunSystem
{
	private IBlahSignalNextFrameConsumer<BasementUnitsBuyApplyCmd> _unitsBuyCmd;

	private IBlahSignalProducer<BizResChangedEv>   _resChangedEv;
	private IBlahSignalProducer<BizUnitsChangedEv> _unitsChangedEv;
	
	private BlahEcsFilter<BizComp> _bizsFilter;

	private ConfigResources _configResources;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
		_configResources = context.Configs.Resources;

		context.View.UnitsBuyButton.Init(
			context.Blah.Pools.GetSignalProducer<BasementUnitsBuyCmd>()
		);
	}

	public void Run()
	{
		foreach (ref var cmd in _unitsBuyCmd)
		{
			foreach (var ent in _bizsFilter)
			{
				ref var biz = ref ent.Get<BizComp>();
				if (biz.Type != EBizType.Mansion || biz.OwnerPlayer != cmd.Player)
					continue;

				var money = biz.Storage.Get(EResourceType.Money);

				var    unitsCount = (int)(money.Amount / _configResources.UnitPrice);
				double finalPrice = unitsCount * _configResources.UnitPrice;

				biz.Storage.Change(new Resource(EResourceType.Money, -finalPrice));
				_resChangedEv.AddFull(ent, EResourceType.Money);

				biz.OwnerPlayerUnitsCount += unitsCount;
				_unitsChangedEv.Add().Ent =  ent;
			}
		}
	}
}
}