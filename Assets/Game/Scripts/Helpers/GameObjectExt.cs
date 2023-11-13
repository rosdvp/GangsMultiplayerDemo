using UnityEngine;

namespace Game.Helpers
{
public static class GameObjectExt
{
	public static void SetActiveSafe(this GameObject go, bool isActive)
	{
		if (go.activeSelf != isActive)
			go.SetActive(isActive);
	}
}
}