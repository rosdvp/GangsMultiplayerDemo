using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Bizs
{
public struct BizGenStartedEv : IBlahEntrySignal
{
	public BlahEcsEntity Ent;
}
}