using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Units.Commands
{
public struct UnitsMoveApplyCmd : IBlahEntryNextFrameSignal
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