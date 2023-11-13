using System;
using System.Collections.Generic;
using Blah.Features;
using Game.Features.Bizs;
using Game.Features.Units.Commands;

namespace Game.Features.Units
{
public class FeatureUnits : BlahFeatureBase
{
	public override HashSet<Type> ConsumingFromOutside { get; } = new()
	{
		typeof(UnitsMoveApplyCmd),
	};
	public override HashSet<Type> Producing{ get; } = new()
	{
		typeof(BizFightStartedEv),
		typeof(BizOwnerChangedEv),
		typeof(BizUnitsChangedEv),

		typeof(UnitsMoveCmd),
	};
public override HashSet<Type> Services { get; } = new()
		{ };
	public override IReadOnlyList<Type> Systems { get; } = new[]
	{
		typeof(UnitsInputSystem),
		typeof(UnitsSystem)
	};
}
}