using System;
using System.Collections.Generic;

namespace Game.Helpers
{
public static class CollectionsExt
{
	public static T Find<T>(this T[] arr, Predicate<T> pred) where T: class
	{
		foreach (var item in arr)
			if (pred(item))
				return item;
		return null;
	}
	
	public static T PopLast<T>(this List<T> list)
	{
		var item = list[^1];
		list.RemoveAt(list.Count - 1);
		return item;
	}
}
}