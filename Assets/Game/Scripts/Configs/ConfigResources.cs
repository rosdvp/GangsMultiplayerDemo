using System;
using BlahEditor.Attributes;
using Game.Helpers;
using Game.Types;
using UnityEngine;

namespace Game.Configs
{
[CreateAssetMenu(menuName = "Configs/Resources")]
public class ConfigResources : ScriptableObject
{
	public double UnitPrice;
	
	[SerializeField, TableArray]
	private ResourcePrice[] _prices;


	public double GetPrice(EResourceType type)
		=> _prices.Find(p => p.ResType == type)?.Price ?? 
		   throw new Exception($"no price for {type}");
	
	
	[Serializable]
	private class ResourcePrice
	{
		public EResourceType ResType;
		public double        Price;
	}
}
}