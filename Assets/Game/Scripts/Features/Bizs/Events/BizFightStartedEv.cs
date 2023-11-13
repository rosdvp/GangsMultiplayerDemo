using Blah.Ecs;
using Blah.Pools;

namespace Game.Features.Bizs
{
public struct BizFightStartedEv : IBlahEntrySignal
{
	public BlahEcsEntity Ent;
}
}