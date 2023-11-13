using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
[CreateAssetMenu(menuName = "Configs/Context")]
public class ConfigsContext : ScriptableObject
{
	public ConfigCommon    Common;
	public ConfigResources Resources;
	public List<ConfigBiz> Bizs;
}
}