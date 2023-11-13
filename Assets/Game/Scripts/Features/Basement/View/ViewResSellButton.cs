using Blah.Pools;
using UnityEngine;

namespace Game.Features.Basement
{
public class ViewResSellButton : MonoBehaviour
{
	private IBlahSignalProducer<BasementResSellCmd> _resSellCmd;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSignalProducer<BasementResSellCmd> resSellCmd)
	{
		_resSellCmd = resSellCmd;
	}

	public void OnTap()
	{
		_resSellCmd.Add();
	}
}
}