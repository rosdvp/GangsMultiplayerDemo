using System;
using Blah.Ecs;
using Blah.Pools;
using Blah.Systems;
using ExitGames.Client.Photon;
using Game.Features.Basement;
using Game.Features.Bizs;
using Game.Features.ResTransport;
using Game.Features.Units.Commands;
using Game.Globals.Context;
using Photon.Pun;
using Photon.Realtime;

namespace Game.Features.Network
{
public class NetworkSyncSystem : IBlahInitSystem, IBlahRunSystem, IBlahResumePauseSystem, IOnEventCallback
{
	private IBlahSignalConsumer<ResTransportCmd>     _resTransportCmd;
	private IBlahSignalConsumer<UnitsMoveCmd>        _unitsMoveCmd;
	private IBlahSignalConsumer<BasementResSellCmd>  _resSellCmd;
	private IBlahSignalConsumer<BasementUnitsBuyCmd> _unitsBuyCmd;

	private IBlahSignalNextFrameProducer<ResTransportApplyCmd>     _resTransportApplyCmd;
	private IBlahSignalNextFrameProducer<UnitsMoveApplyCmd>        _unitsMoveApplyCmd;
	private IBlahSignalNextFrameProducer<BasementResSellApplyCmd>  _resSellApplyCmd;
	private IBlahSignalNextFrameProducer<BasementUnitsBuyApplyCmd> _unitsBuyApplyCmd;

	private BlahEcsFilter<BizComp> _bizsFilter;
    
	
	private int _currPlayer;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private const byte CODE_RES_TRANSPORT_CMD = 1;
	private const byte CODE_UNITS_MOVE_CMD    = 2;
	private const byte CODE_RES_SELL_CMD      = 3;
	private const byte CODE_UNITS_BUY_CMD     = 4;
	
	
	public void Init(IBlahSystemsInitData initData)
	{
		var context = (IContextForSystems)initData;
		_currPlayer = context.CurrPlayer;
	}

	public void Run()
	{
		foreach (ref var cmd in _resTransportCmd)
		{
			SendResTransportApplyCmd(
				cmd.FromBizEnt.Get<BizComp>().Id,
				cmd.ToBizEnt.Get<BizComp>().Id
			);
		}
		foreach (ref var cmd in _unitsMoveCmd)
		{
			SendUnitsMoveCmd(
				cmd.FromBizEnt.Get<BizComp>().Id,
				cmd.ToBizEnt.Get<BizComp>().Id
			);
		}
		foreach (ref var cmd in _resSellCmd)
		{
			SendResSellCmd();
		}
		foreach (ref var cmd in _unitsBuyCmd)
		{
			SendUnitsBuyCmd();
		}
	}

	public void Resume()
	{
		PhotonNetwork.AddCallbackTarget(this);
	}

	public void Pause()
	{
		PhotonNetwork.RemoveCallbackTarget(this);
	}

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private void SendResTransportApplyCmd(int fromBizId, int toBizId)
	{
		PhotonNetwork.RaiseEvent(
			CODE_RES_TRANSPORT_CMD,
			new object[] { fromBizId, toBizId },
			new RaiseEventOptions { Receivers = ReceiverGroup.All },
			SendOptions.SendReliable
		);
	}

	private void SendUnitsMoveCmd(int fromBizId, int toBizId)
	{
		PhotonNetwork.RaiseEvent(
			CODE_UNITS_MOVE_CMD,
			new object[] { fromBizId, toBizId },
			new RaiseEventOptions { Receivers = ReceiverGroup.All },
			SendOptions.SendReliable
		);
	}

	private void SendResSellCmd()
	{
		PhotonNetwork.RaiseEvent(
			CODE_RES_SELL_CMD,
			_currPlayer,
			new RaiseEventOptions { Receivers = ReceiverGroup.All },
			SendOptions.SendReliable
		);
	}

	private void SendUnitsBuyCmd()
	{
		PhotonNetwork.RaiseEvent(
			CODE_UNITS_BUY_CMD,
			_currPlayer,
			new RaiseEventOptions { Receivers = ReceiverGroup.All },
			SendOptions.SendReliable
		);
	}

	public void OnEvent(EventData ev)
	{
		if (ev.Code == CODE_RES_TRANSPORT_CMD)
		{
			var data      = (object[])ev.CustomData;
			var fromBizId = (int)data[0];
			var toBizId   = (int)data[1];
			_resTransportApplyCmd.AddNextFrame().Fill(
				FindBizEnt(fromBizId),
				FindBizEnt(toBizId)
			);
		}
		else if (ev.Code == CODE_UNITS_MOVE_CMD)
		{
			var data      = (object[])ev.CustomData;
			var fromBizId = (int)data[0];
			var toBizId   = (int)data[1];
			_unitsMoveApplyCmd.AddNextFrame().Fill(
				FindBizEnt(fromBizId),
				FindBizEnt(toBizId)
			);
		}
		else if (ev.Code == CODE_RES_SELL_CMD)
		{
			_resSellApplyCmd.AddNextFrame().Player = (int)ev.CustomData;
		}
		else if (ev.Code == CODE_UNITS_BUY_CMD)
		{
			_unitsBuyApplyCmd.AddNextFrame().Player = (int)ev.CustomData;
		}
	}

	private BlahEcsEntity FindBizEnt(int bizId)
	{
		foreach (var ent in _bizsFilter)
			if (ent.Get<BizComp>().Id == bizId)
				return ent;
		throw new Exception($"no biz with id {bizId} found");
	}
}
}