using System;
using System.Collections.Generic;
using Blah.Features;
using Game.Features.Bizs;
using Game.Features.Network;

namespace Game.Features.ResTransport
{
public class FeatureResTransport : BlahFeatureBase
{
	public override HashSet<Type> ConsumingFromOutside { get; } = new()
	{
		typeof(ResTransportApplyCmd),
	};
	public override HashSet<Type> Producing { get; } = new()
	{
		typeof(BizResChangedEv),

		typeof(ResTransportCmd),
	};
	public override HashSet<Type> Services { get; } = new()
		{ };
	public override IReadOnlyList<Type> Systems { get; } = new[]
	{
		typeof(ResTransportInputSystem),
		typeof(ResTransportSystem)
	};
}
}