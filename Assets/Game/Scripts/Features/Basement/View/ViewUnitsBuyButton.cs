using Blah.Pools;
using UnityEngine;

namespace Game.Features.Basement
{
public class ViewUnitsBuyButton : MonoBehaviour
{
	private IBlahSignalProducer<BasementUnitsBuyCmd> _unitsBuyCmd;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(IBlahSignalProducer<BasementUnitsBuyCmd> unitsBuyCmd)
	{
		_unitsBuyCmd = unitsBuyCmd;
	}

	public void OnTap()
	{
		_unitsBuyCmd.Add();
	}
}
}