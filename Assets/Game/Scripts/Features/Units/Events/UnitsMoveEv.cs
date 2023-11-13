using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Units
{
public struct UnitsMoveEv : IBlahEntrySignal
{
	public BlahEcsEntity FromEnt;
	public BlahEcsEntity ToEnt;
}
}