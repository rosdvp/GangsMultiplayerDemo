using Game.Features.Basement;
using Game.Features.Lobby;
using UnityEngine;

namespace Game.Globals.View
{
public class ViewContext : MonoBehaviour
{
	[SerializeField]
	private ViewLobbyScreen _viewLobbyScreen;
	[SerializeField]
	private Transform _bizsHolder;
	[SerializeField]
	private ViewResSellButton _resSellButton;
	[SerializeField]
	private ViewUnitsBuyButton _unitsBuyButton;
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public ViewLobbyScreen LobbyScreen => _viewLobbyScreen;
	
	public Transform BizsHolder => _bizsHolder;

	public ViewResSellButton ResSellButton => _resSellButton;
	
	public ViewUnitsBuyButton UnitsBuyButton => _unitsBuyButton;
}
}