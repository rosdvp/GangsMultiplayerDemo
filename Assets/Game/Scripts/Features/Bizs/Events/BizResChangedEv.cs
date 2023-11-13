using Blah.Ecs;
using Blah.Pools;
using Game.Types;

namespace Game.Features.Bizs
{
public struct BizResChangedEv : IBlahEntrySignal
{
	public BlahEcsEntity Ent;

	public EResourceType ResType;
}

public static class BizResChangedEvExt
{
	public static void AddFull(
		this IBlahSignalProducer<BizResChangedEv> producer,
		BlahEcsEntity                             ent,
		EResourceType                             resType)
	{
		ref var ev = ref producer.Add();
		ev.Ent     = ent;
		ev.ResType = resType;
	}
}
}