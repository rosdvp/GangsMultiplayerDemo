using System.Collections;
using System.Collections.Generic;
using Game.Assets;
using Game.Helpers;
using Game.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Bizs
{
public class ViewBiz : MonoBehaviour
{
	[Header("Comps")]
	[SerializeField]
	private SpriteRenderer _sr;
	[SerializeField]
	private Image _genFiller;
	[SerializeField]
	private Image _ownerImage;
	[SerializeField]
	private TextMeshProUGUI _unitsCountText;
	[SerializeField]
	private Image _fightFiller;
	[SerializeField]
	private ViewBizRes[] _viewsResPool;
	
	
	private AssetsContext _assetsContext;
	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private List<ViewBizRes> _freeViewsRes;

	private Dictionary<EResourceType, ViewBizRes> _resTypeToView = new();

	private int _ownerUnitsCount;
	private int _attackingUnitsCount;
	
	private Coroutine _crResGenAnim;
	private Coroutine _crFightAnim;
	
	public void Init(AssetsContext assetsContext)
	{
		_assetsContext = assetsContext;

		_genFiller.fillAmount     = 0;
		_fightFiller.fillAmount = 0;

		foreach (var view in _viewsResPool)
		{
			view.Init(assetsContext);
			view.TurnOff();
		}
		_freeViewsRes = new List<ViewBizRes>(_viewsResPool);
	}

	//-----------------------------------------------------------
	//-----------------------------------------------------------
	public void SetBiz(EBizType type)
	{
		_sr.sprite = _assetsContext.Common.FindBizSprite(type);
	}

	public void SetOwnerPlayer(int ownerPlayer)
	{
		_ownerImage.color = _assetsContext.Common.PlayersColors[ownerPlayer];
	}

	public void SetUnitsCount(int ownerUnitsCount, int attackingUnitsCount)
	{
		_ownerUnitsCount     = ownerUnitsCount;
		_attackingUnitsCount = attackingUnitsCount;
		UpdateUnitsTexts();
	}

	public void StartFightAnim(int winningPlayer, float duration)
	{
		if (_crFightAnim != null)
			StopCoroutine(_crFightAnim);
		_crFightAnim = StartCoroutine(CrFightAnim(winningPlayer, duration));
		UpdateUnitsTexts();
	}
	
	
	public void SetRes(Resource res)
	{
		if (res.Amount.IsZero())
		{
			if (_resTypeToView.TryGetValue(res.Type, out var view))
			{
				view.TurnOff();
				_resTypeToView.Remove(res.Type);
				_freeViewsRes.Add(view);
			}
		}
		else
		{
			if (_resTypeToView.TryGetValue(res.Type, out var view))
			{
				view.SetAmount(res.Amount);
			}
			else
			{
				view = _freeViewsRes.PopLast();
				view.TurnOn(res);
				_resTypeToView[res.Type] = view;
			}
		}
	}
	
	public void StartResGenAnim(float duration)
	{
		if (_crResGenAnim != null)
			StopCoroutine(_crResGenAnim);
		_crResGenAnim = StartCoroutine(CrResGenAnim(duration));
	}


	//-----------------------------------------------------------
	//-----------------------------------------------------------
	private void UpdateUnitsTexts()
	{
		bool isFighting = _crFightAnim != null;
		_unitsCountText.text = isFighting
			? $"{_ownerUnitsCount}:{_attackingUnitsCount}"
			: _ownerUnitsCount.ToString();
		_unitsCountText.gameObject.SetActiveSafe(isFighting || _ownerUnitsCount > 0);
	}

	private IEnumerator CrFightAnim(int winningPlayer, float duration)
	{
		_ownerImage.color  = _assetsContext.Common.FightColor;
		_fightFiller.color = _assetsContext.Common.PlayersColors[winningPlayer];

		yield return CrFillAnim(_fightFiller, duration);
		_crFightAnim = null;

		_ownerImage.color = _fightFiller.color;

		UpdateUnitsTexts();
	}

	private IEnumerator CrResGenAnim(float duration)
	{
		yield return CrFillAnim(_genFiller, duration);
		_crResGenAnim = null;
	}
	
	private IEnumerator CrFillAnim(Image filler, float duration)
	{
		filler.fillAmount = 0;

		float secsLeft = duration;
		while (secsLeft > 0)
		{
			float time = Time.time;
			yield return new WaitForSeconds(0.01f);
			secsLeft -= Time.time - time;

			filler.fillAmount = 1f - (secsLeft / duration);
		}

		filler.fillAmount = 0;
	}
}
}