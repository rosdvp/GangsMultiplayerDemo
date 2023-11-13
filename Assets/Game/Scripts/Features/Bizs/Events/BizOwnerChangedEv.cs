using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Bizs
{
public struct BizOwnerChangedEv : IBlahEntrySignal
{
	public BlahEcsEntity Ent;
}
}