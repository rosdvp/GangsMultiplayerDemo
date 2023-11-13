using UnityEngine;

namespace Game.Assets
{
[CreateAssetMenu(menuName = "GameAssets/Context")]
public class AssetsContext : ScriptableObject
{
	public AssetCommon Common;
	public AssetMap    Map;
}
}