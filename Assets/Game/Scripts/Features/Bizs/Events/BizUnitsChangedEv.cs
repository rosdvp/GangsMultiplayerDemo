using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Bizs
{
public struct BizUnitsChangedEv : IBlahEntrySignal
{
	public BlahEcsEntity Ent;
}
}