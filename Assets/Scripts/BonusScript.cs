using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BonusScript : MonoBehaviour
{
	private sealed class _bonusTimer_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal BonusScript _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _bonusTimer_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.bonusTimeRemaining = 5f;
				this._this.bonusPanel.SetActive(true);
				break;
			case 1u:
				break;
			default:
				return false;
			}
			this._this.bonusTimeRemaining -= Time.deltaTime;
			this._this.bonusTimerImage.fillAmount = this._this.bonusTimeRemaining / 5f;
			if (this._this.bonusTimeRemaining > 0f)
			{
				this._current = new WaitForEndOfFrame();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._this.bonusPanel.SetActive(false);
			UIManager.instance.restartBonusShowTimer();
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _bonusClicked_c__AnonStorey1
	{
		internal double bonusAmount;

		internal void __m__0()
		{
			OyunYoneticisi.instance.collectMoney(this.bonusAmount, false);
			UIManager.instance.restartBonusShowTimer();
			if (BonusReklamKontrolu.instance.canShowAdForBonus())
			{
				BonusReklamKontrolu.instance.showAdForBonus();
			}
			else
			{
				RATESCRIPT.instance.oylamayiGoster();
			}
		}

		internal void __m__1()
		{
			

            /*AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {

                OyunYoneticisi.instance.collectMoney(this.bonusAmount * 2.0, false);
                UIManager.instance.restartBonusShowTimer();
            });*/
        }

		internal void __m__2()
		{
			OyunYoneticisi.instance.collectMoney(this.bonusAmount * 2.0, false);
			UIManager.instance.restartBonusShowTimer();
		}
	}

	public GameObject bonusPanel;

	public Image bonusTimerImage;

	private float bonusTimeRemaining;

	public static BonusScript instance;

	private void Awake()
	{
		BonusScript.instance = this;
	}

	public void showBonusPanel()
	{
		base.StopCoroutine("bonusTimer");
		if (!UIManager.instance.infoPanel.gameObject.activeInHierarchy)
		{
			base.StartCoroutine("bonusTimer");
		}
		else
		{
			UIManager.instance.restartBonusShowTimer();
		}
	}

	private IEnumerator bonusTimer()
	{
		BonusScript._bonusTimer_c__Iterator0 _bonusTimer_c__Iterator = new BonusScript._bonusTimer_c__Iterator0();
		_bonusTimer_c__Iterator._this = this;
		return _bonusTimer_c__Iterator;
	}

	public void bonusClicked()
	{
		this.bonusPanel.gameObject.SetActive(false);
		double bonusAmount = FormulHesaplayicisi.instance.getBonusAmount();
		UIManager.instance.infoPanel.OpenPanel("You sold rare material for :", bonusAmount, delegate
		{
			OyunYoneticisi.instance.collectMoney(bonusAmount, false);
			UIManager.instance.restartBonusShowTimer();
			if (BonusReklamKontrolu.instance.canShowAdForBonus())
			{
				BonusReklamKontrolu.instance.showAdForBonus();
			}
			else
			{
				RATESCRIPT.instance.oylamayiGoster();
			}
		}, delegate
		{
		

            /*AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {

                OyunYoneticisi.instance.collectMoney(bonusAmount * 2.0, false);
                UIManager.instance.restartBonusShowTimer();
            });*/
        });
	}
}
