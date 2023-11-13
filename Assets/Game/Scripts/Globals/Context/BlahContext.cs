using System.Collections.Generic;
using Blah.Features;
using Game.Features.Basement;
using Game.Features.Bizs;
using Game.Features.Network;
using Game.Features.ResTransport;
using Game.Features.Units;

namespace Game.Globals.Context
{
public class BlahContext : BlahContextBase
{
	protected override Dictionary<int, List<BlahFeatureBase>> FeaturesBySystemsGroups { get; } = new()
	{
		{
			(int)EBlahFeaturesGroup.Main, new List<BlahFeatureBase>()
			{
				new FeatureBizs(),
				new FeatureResTransport(),
				new FeatureBasement(),
				new FeatureUnits(),
				new FeatureNetwork()
			}
		}
	};
}

public enum EBlahFeaturesGroup
{
	Main,
}
}