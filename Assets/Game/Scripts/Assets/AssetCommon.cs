using System;
using BlahEditor.Attributes;
using Game.Helpers;
using Game.Types;
using UnityEngine;

namespace Game.Assets
{
[CreateAssetMenu(menuName = "GameAssets/Common")]
public class AssetCommon : ScriptableObject
{
	public GameObject PrefBiz;

	public Color   FightColor;
	public Color[] PlayersColors;

	[SerializeField, TableArray]
	private AssetRes[] _resources;
	[SerializeField, TableArray]
	private AssetBiz[] _bizs;

	public Sprite FindResIcon(EResourceType type) 
		=> _resources.Find(r => r.Type == type).Icon;

	public Sprite FindBizSprite(EBizType type)
		=> _bizs.Find(b => b.Type == type).Sprite;


	[Serializable]
	private class AssetRes
	{
		public EResourceType Type;
		public Sprite        Icon;
	}

	[Serializable]
	private class AssetBiz
	{
		public EBizType Type;
		public Sprite   Sprite;
	}
}
}