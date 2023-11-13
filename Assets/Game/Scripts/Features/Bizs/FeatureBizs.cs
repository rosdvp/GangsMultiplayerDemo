using System;
using System.Collections.Generic;
using Blah.Features;

namespace Game.Features.Bizs
{
public class FeatureBizs : BlahFeatureBase
{
	public override HashSet<Type> ConsumingFromOutside{ get; } = new()
	{
		typeof(BizFightStartedEv),
		typeof(BizOwnerChangedEv),
		typeof(BizUnitsChangedEv),
	};
public override HashSet<Type> Producing { get; } = new()
	{
		typeof(BizGenStartedEv),
		typeof(BizResChangedEv),
	};
	public override HashSet<Type> Services { get; }
	public override IReadOnlyList<Type> Systems { get; } = new[]
	{
		typeof(BizCreateSystem),
		typeof(BizGenSystem),
		typeof(BizViewSystem)
	};
}
}