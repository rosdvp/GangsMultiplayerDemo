using System;
using System.Collections.Generic;
using Blah.Features;
using Game.Features.Bizs;

namespace Game.Features.Basement
{
public class FeatureBasement : BlahFeatureBase
{
	public override HashSet<Type> ConsumingFromOutside{ get; } = new()
	{
		typeof(BasementResSellApplyCmd),
		typeof(BasementUnitsBuyApplyCmd),
	};
public override HashSet<Type> Producing { get; } = new()
	{
		typeof(BizResChangedEv),
		typeof(BizUnitsChangedEv),
	};
	public override HashSet<Type> Services { get; }
	public override IReadOnlyList<Type> Systems { get; } = new[]
	{
		typeof(BasementResSellSystem),
		typeof(BasementUnitsBuySystem)
	};
}
}