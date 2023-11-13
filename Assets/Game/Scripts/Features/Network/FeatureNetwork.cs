using System;
using System.Collections.Generic;
using Blah.Features;
using Game.Features.Basement;
using Game.Features.ResTransport;
using Game.Features.Units.Commands;

namespace Game.Features.Network
{
public class FeatureNetwork : BlahFeatureBase
{
	public override HashSet<Type> ConsumingFromOutside { get; } = new()
	{
		typeof(BasementResSellCmd),
		typeof(BasementUnitsBuyCmd),

		typeof(ResTransportCmd),

		typeof(UnitsMoveCmd),
	};
	public override HashSet<Type> Producing { get; } = new()
	{
		typeof(BasementResSellApplyCmd),
		typeof(BasementUnitsBuyApplyCmd),

		typeof(ResTransportApplyCmd),

		typeof(UnitsMoveApplyCmd),
	};
	public override HashSet<Type> Services { get; }
	public override IReadOnlyList<Type> Systems { get; } = new[]
	{
		typeof(NetworkSyncSystem)
	};
}
}