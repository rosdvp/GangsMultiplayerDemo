using Game.Helpers;
using TMPro;
using UnityEngine;

namespace Game.Features.Lobby
{
public class ViewLobbyScreen : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _statusText;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void SetStatus(string status)
	{
		_statusText.text = status;
	}
	
	public void SetVisible(bool isVisible)
	{
		gameObject.SetActiveSafe(isVisible);
	}
}
}