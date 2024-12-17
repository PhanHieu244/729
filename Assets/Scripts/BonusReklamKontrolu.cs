using System;
using UnityEngine;

public class BonusReklamKontrolu : MonoBehaviour
{
	private int bonusAdTimer = 70;

	private bool timeCheckForBonus;

	public static BonusReklamKontrolu instance;

	private void Awake()
	{
		BonusReklamKontrolu.instance = this;
	}

	public void bonusReklaminiBeklet()
	{
		this.timeCheckForBonus = false;
		base.CancelInvoke("timeCheckForBonusActivate");
		base.Invoke("timeCheckForBonusActivate", (float)this.bonusAdTimer);
	}

	private void timeCheckForBonusActivate()
	{
		this.timeCheckForBonus = true;
	}

	public void showAdForBonus()
	{
		/*if (this.timeCheckForBonus && AdsControl.Instance.GetInterstitialAvailable())
		{
            AdsControl.Instance.showAds();
		}*/
	}

	public bool canShowAdForBonus()
	{
		return this.timeCheckForBonus;
	}
}
