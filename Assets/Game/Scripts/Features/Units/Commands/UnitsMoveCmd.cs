using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Units.Commands
{
public struct UnitsMoveCmd : IBlahEntrySignal
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