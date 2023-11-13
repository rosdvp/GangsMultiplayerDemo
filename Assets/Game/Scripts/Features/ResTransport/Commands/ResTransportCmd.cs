using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.ResTransport
{
public struct ResTransportCmd : IBlahEntrySignal
{
	public BlahEcsEntity FromBizEnt;
	public BlahEcsEntity ToBizEnt;
}

public static class ResTransportCmdExt
{
	public static void AddFull(
		this IBlahSignalProducer<ResTransportCmd> producer,
		BlahEcsEntity fromBiz,
		BlahEcsEntity toBiz)
	{
		ref var cmd = ref producer.Add();
		cmd.FromBizEnt = fromBiz;
		cmd.ToBizEnt   = toBiz;
	}
}
}