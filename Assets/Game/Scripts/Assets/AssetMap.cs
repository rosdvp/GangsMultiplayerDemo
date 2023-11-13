using System;
using System.Collections.Generic;
using Game.Types;
using UnityEngine;

namespace Game.Assets
{
[CreateAssetMenu(menuName = "GameAssets/Map")]
public class AssetMap : ScriptableObject
{
	public List<AssetBiz> Bizs;

	[Serializable]
	public class AssetBiz
	{
		public int      OwnerPlayer;
		public EBizType Type;
		public Vector3  Pos;
	}
}
}