using BlahDebugConsole.Console;
using BlahDebugConsole.Logger;
using Cysharp.Threading.Tasks;
using Game.Assets;
using Game.Configs;
using Game.Globals.Context;
using Game.Globals.Network;
using Game.Globals.View;
using UnityEngine;

namespace Game.Globals.Startup
{
public class GameStartup : MonoBehaviour
{
	[Header("Params")]
	[SerializeField]
	private bool _isSingleMode;
	
	[Header("Dependencies")]
	[SerializeField]
	private BlahConsole _blahConsole;
	[SerializeField]
	private ConfigsContext _configsContext;
	[SerializeField]
	private AssetsContext _assetsContext;
	[SerializeField]
	private ViewContext _viewContext;
	[SerializeField]
	private PhotonManager _photonManager;

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private GameContext _context;

	private bool _isInited;
	
	private async UniTaskVoid Start()
	{
		_isInited = false;
		
		BlahLogger.Init(new BlahLoggerConfig
		{
			MinimalLogType = ELogType.Verbose,
			UseUnityConsoleLevels = true,
			IsWriteIntoFile = false,
			CustomListenersCount = 0,
		});
		_blahConsole.Init();
        
		_context         = new GameContext();
		_context.Configs = _configsContext;
		_context.Assets  = _assetsContext;
		_context.View    = _viewContext;
		
		_context.View.LobbyScreen.SetVisible(true);
		_context.View.LobbyScreen.SetStatus("Joining the game..");
		
		_photonManager.StartConnection();
		await UniTask.WaitWhile(() => !_photonManager.IsConnected);
		_context.View.LobbyScreen.SetStatus("Wait another player..");
		if (!_isSingleMode)
			await UniTask.WaitWhile(() => !_photonManager.IsRoomFull);

		_context.CurrPlayer = _photonManager.IsMasterClient ? 1 : 2;
		

		_context.Blah = new BlahContext();
		
		_context.Blah.Init(_context, _context);
		_context.Blah.RequestSwitchSystemsGroup((int)EBlahFeaturesGroup.Main, true);

		_context.View.LobbyScreen.SetVisible(false);
		
		_isInited = true;
	}


	private void Update()
	{
		if (!_isInited)
			return;
		
		_context.Blah.Run();
	}
}
}