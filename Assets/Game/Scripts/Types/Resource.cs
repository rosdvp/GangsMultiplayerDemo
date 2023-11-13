using System;
using UnityEngine;

namespace Game.Types
{
[Serializable]
public struct Resource
{
	public EResourceType Type;
	public double        Amount;

	public Resource(EResourceType type)
	{
		Type   = type;
		Amount = 0;
	}
	
	public Resource(EResourceType type, double amount)
	{
		Type   = type;
		Amount = amount;
	}

	public readonly int AmountInt() => Mathf.RoundToInt((float)Amount);


	public static Resource operator +(Resource a, Resource b)
	{
		if (a.Type != b.Type)
			throw new Exception($"cannot sum {a.Type} and {b.Type}");
		return new Resource(a.Type, a.Amount + b.Amount);
	}

	public static Resource operator -(Resource a, Resource b)
	{
		if (a.Type != b.Type)
			throw new Exception($"cannot sub {a.Type} and {b.Type}");
		return new Resource(a.Type, a.Amount - b.Amount);
	}

	public static Resource operator -(Resource a)
	{
		return new Resource(a.Type, -a.Amount);
	}
}
}
