using Blah.Services;
using Blah.Systems;
using Game.Assets;
using Game.Configs;
using Game.Globals.Network;
using Game.Globals.View;

namespace Game.Globals.Context
{
public class GameContext : IContextForSystems, IContextForServices
{
	public ConfigsContext Configs { get; set; }
	public AssetsContext  Assets  { get; set; }
	
	public BlahContext    Blah    { get; set; }
	
	public ViewContext    View    { get; set; }

	public int CurrPlayer { get; set; }

	public PhotonManager PhotonManager { get; set; }
}

public interface IContextForSystems : IBlahSystemsInitData
{
	public ConfigsContext Configs { get; }
	public AssetsContext  Assets  { get; }

	public BlahContext Blah { get; }

	public ViewContext View { get; }

	public int           CurrPlayer    { get; }
	public PhotonManager PhotonManager { get; }
}

public interface IContextForServices : IBlahServicesInitData
{
	public ConfigsContext Configs { get; }
}
}