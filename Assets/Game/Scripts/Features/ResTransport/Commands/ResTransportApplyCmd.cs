using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.ResTransport
{
public struct ResTransportApplyCmd : IBlahEntryNextFrameSignal
{
	public BlahEcsEntity FromBizEnt;
	public BlahEcsEntity ToBizEnt;

	public void Fill(BlahEcsEntity fromBizEnt, BlahEcsEntity toBizEnt)
	{
		FromBizEnt = fromBizEnt;
		ToBizEnt   = toBizEnt;
	}
}
}