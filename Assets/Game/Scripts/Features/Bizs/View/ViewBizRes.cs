using Game.Assets;
using Game.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Bizs
{
public class ViewBizRes : MonoBehaviour
{
	[SerializeField]
	private Image _icon;
	[SerializeField]
	private TextMeshProUGUI _countText;

	private AssetsContext _assets;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void Init(AssetsContext assets)
	{
		_assets = assets;
	}

	public void TurnOn(Resource res)
	{
		_icon.sprite = _assets.Common.FindResIcon(res.Type);
		SetAmount(res.Amount);
		
		gameObject.SetActive(true);
	}

	public void TurnOff()
	{
		gameObject.SetActive(false);
	}

	public void SetAmount(double amount)
	{
		_countText.text = Mathf.RoundToInt((float)amount).ToString();
	}
}
}