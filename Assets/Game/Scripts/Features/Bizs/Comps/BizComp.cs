using Blah.Ecs;
using Game.Configs;
using Game.Types;

namespace Game.Features.Bizs
{
public struct BizComp : IBlahEntryEcs
{
	public int      Id;
	public EBizType Type;

	public ResourcesStorage Storage;

	public int OwnerPlayer;
	public int OwnerPlayerUnitsCount;
	public int AttackingPlayer;
	public int AttackingPlayerUnitsCount;
	public int WinningPlayer;

	public bool  IsFighting;
	public float FightEndTime;

	public bool  IsGenStarted;
	public float NextGenTime;
	
	public ConfigBiz Config;
	public ViewBiz   View;
}
}