using System;
using BlahDebugConsole.Logger;
using ExitGames.Client.Photon;
using Game.QA;
using Photon.Pun;
using Photon.Realtime;

namespace Game.Globals.Network
{
public class PhotonManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
	public void StartConnection()
	{
		var nick = $"Player_{Guid.NewGuid().ToString()}";
        
		PhotonNetwork.NickName   = nick;
		PhotonNetwork.AuthValues = new AuthenticationValues(nick);
		PhotonNetwork.ConnectUsingSettings();
	}

	public bool IsConnected => PhotonNetwork.InRoom;

	public bool IsRoomFull
		=> PhotonNetwork.InRoom &&
		   PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
	
	public bool IsMasterClient => PhotonNetwork.IsMasterClient;

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void SendResTransportCmd()
	{
		
	}

	public void SendUnitsMoveCmd()
	{
		
	}
	
	public void OnEvent(EventData data)
	{
		
	}
	
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public override void OnConnectedToMaster()
	{
		BlahLogger.Info(ELogTag.Photon, $"connected to master-server {PhotonNetwork.CloudRegion}");

		if (!PhotonNetwork.InLobby)
			PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		BlahLogger.Info(ELogTag.Photon, $"joined lobby");

		var roomName = $"TestRoom";
		
		var options = new RoomOptions();
		options.IsOpen     = true;
		options.IsVisible  = true;
		options.MaxPlayers = 2;

		PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
	}

	public override void OnCreatedRoom()
	{
		BlahLogger.Info(ELogTag.Photon, $"created room {PhotonNetwork.CurrentRoom.Name}");
	}
	
	public override void OnJoinedRoom()
	{
		BlahLogger.Info(ELogTag.Photon, $"joined room {PhotonNetwork.CurrentRoom.Name}");
	}

	public override void OnLeftRoom()
	{
		BlahLogger.Info(ELogTag.Photon, $"left room");
	}

	public override void OnJoinRoomFailed(short returnCode, string msg)
	{
		BlahLogger.Info(ELogTag.Photon, $"failed to join room, code: {returnCode}, msg: {msg}");
	}

	public override void OnPlayerEnteredRoom(Player player)
	{
		BlahLogger.Info(ELogTag.Photon, $"player {player.UserId} joined room");
	}

	public override void OnPlayerLeftRoom(Player player)
	{
		BlahLogger.Info(ELogTag.Photon, $"player {player.UserId} left room");
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		BlahLogger.Info(ELogTag.Photon, $"master client switched to {newMasterClient.UserId}");
	}
}
}