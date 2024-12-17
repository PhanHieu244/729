using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private sealed class _bonusShower_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _bonusRemainingTime___0;

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

		public _bonusShower_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._bonusRemainingTime___0 = (float)UnityEngine.Random.Range(25, 35);
				break;
			case 1u:
				break;
			default:
				return false;
			}
			this._bonusRemainingTime___0 -= Time.deltaTime;
			if (this._bonusRemainingTime___0 > 0f)
			{
				this._current = new WaitForEndOfFrame();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			BonusScript.instance.showBonusPanel();
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

	public Color enabledColor;

	public Color disabledColor;

	public Button firstBoosterButton;

	public Button secondBoosterButton;

	public Button speedButton;

	public Button powerButton;

	public Button reducerButton;

	public Button gravityButton;

	public Button toothCountButton;

	public Button toothSizeButton;

	public Button marketingButton;

	public Text speedValueText;

	public Text powerValueText;

	public Text reducerValueText;

	public Text gravityValueText;

	public Text toothCountValueText;

	public Text toothSizeValueText;

	public Text marketingValueText;

	public Text speedUpgradeValueText;

	public Text powerUpgradeValueText;

	public Text reducerUpgradeValueText;

	public Text gravityUpgradeValueText;

	public Text toothCountUpgradeValueText;

	public Text toothSizeUpgradeValueText;

	public Text marketingUpgradeValueText;

	public Text moneyText;

	public Text moneyShortenText;

	public Image levelProgressBar;

	public Button levelUpButton;

	public Text levelText;

	public InfoPanelManager infoPanel;

	public VideoButton x3Button;

	public VideoButton x2Button;

	public static UIManager instance;

	public GameObject tutorialPanel;

	public GameObject tutorialPanelStep1;

	public GameObject tutorialPanelStep2;

	private static Action __f__am_cache0;

	public int IS_TUTORIAL_COMPLETED
	{
		get
		{
			return PlayerPrefs.GetInt("IS_TUTORIAL_COMPLETED", 0);
		}
		set
		{
			PlayerPrefs.SetInt("IS_TUTORIAL_COMPLETED", value);
		}
	}

	private void Awake()
	{
		UIManager.instance = this;
        Application.targetFrameRate = 60;
	}

	private void Start()
	{
		base.InvokeRepeating("checkButtonStates", 1f, 0.5f);
		this.restartBonusShowTimer();
	}

	public void initUI()
	{
		this.checkButtonStates();
	}

	private void checkButtonStates()
	{
		this.speedValueText.text = string.Empty + FormulHesaplayicisi.instance._getHizDisplayValue().ToSymbol(false);
		this.powerValueText.text = string.Empty + FormulHesaplayicisi.instance._getPowerDisplayValue().ToSymbol(false);
		this.gravityValueText.text = string.Empty + FormulHesaplayicisi.instance._getGravityDisplayValue();
		this.toothCountValueText.text = string.Empty + FormulHesaplayicisi.instance._getToothCountDisplayValue().ToSymbol(false);
		this.toothSizeValueText.text = string.Empty + FormulHesaplayicisi.instance._getToothSizeDisplayValue().ToSymbol(false);
		this.marketingValueText.text = string.Empty + FormulHesaplayicisi.instance._getMarketingDisplayValue().ToSymbol(false);
		this.speedUpgradeValueText.text = string.Empty + FormulHesaplayicisi.instance._getHizYukseltmeValue().ToSymbol(false);
		this.powerUpgradeValueText.text = string.Empty + FormulHesaplayicisi.instance._getPowerUpgradeValue().ToSymbol(false);
		this.gravityUpgradeValueText.text = string.Empty + FormulHesaplayicisi.instance._getGravityUpgradeValue().ToSymbol(false);
		this.toothCountUpgradeValueText.text = string.Empty + FormulHesaplayicisi.instance._getToothCountUpgradeValue().ToSymbol(false);
		this.toothSizeUpgradeValueText.text = string.Empty + FormulHesaplayicisi.instance._getToothSizeUpgradeValue().ToSymbol(false);
		if (OyunYoneticisi.instance.DisBoyutuSeviyesi >= 11)
		{
			this.toothSizeUpgradeValueText.text = "MAX";
		}
		this.marketingUpgradeValueText.text = string.Empty + FormulHesaplayicisi.instance._getMarketingUpgradeValue().ToSymbol(false);
		double money = OyunYoneticisi.instance.money;
		if (money >= FormulHesaplayicisi.instance._getHizYukseltmeValue())
		{
			this.speedButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.speedButton.GetComponent<Image>().color = this.disabledColor;
		}
		if (money >= FormulHesaplayicisi.instance._getPowerUpgradeValue())
		{
			this.powerButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.powerButton.GetComponent<Image>().color = this.disabledColor;
		}
		this.reducerButton.GetComponent<Image>().color = this.disabledColor;
		if (money >= FormulHesaplayicisi.instance._getGravityUpgradeValue())
		{
			this.gravityButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.gravityButton.GetComponent<Image>().color = this.disabledColor;
		}
		if (money >= FormulHesaplayicisi.instance._getToothCountUpgradeValue())
		{
			this.toothCountButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.toothCountButton.GetComponent<Image>().color = this.disabledColor;
		}
		if (money >= FormulHesaplayicisi.instance._getToothSizeUpgradeValue() && OyunYoneticisi.instance.DisBoyutuSeviyesi <= 10)
		{
			this.toothSizeButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.toothSizeButton.GetComponent<Image>().color = this.disabledColor;
		}
		if (money >= FormulHesaplayicisi.instance._getMarketingUpgradeValue())
		{
			this.marketingButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.marketingButton.GetComponent<Image>().color = this.disabledColor;
		}
	}

	public void updateMoney(double amount)
	{
		this.moneyText.text = "$" + amount.ToString("N0");
		this.moneyShortenText.text = ((amount >= 1000.0) ? amount.ToSymbol(false) : string.Empty);
	}

	public void updateProgressBar(float percentage)
	{
		this.levelProgressBar.fillAmount = percentage;
		if (percentage >= 1f)
		{
			this.levelUpButton.GetComponent<Image>().color = this.enabledColor;
		}
		else
		{
			this.levelUpButton.GetComponent<Image>().color = this.disabledColor;
		}
	}

	public void levelUpButtonClicked()
	{
		if (this.levelProgressBar.fillAmount >= 1f)
		{
			this.restartBonusShowTimer();
			this.infoPanel.OpenPanel("Level Up Bonus:", FormulHesaplayicisi.instance._getLevelCompletePrizeValue(), delegate
			{
				OyunYoneticisi.instance.collectMoney(FormulHesaplayicisi.instance._getLevelCompletePrizeValue(), false);
				OyunYoneticisi.instance.levelUped();
				this.levelProgressBar.fillAmount = 0f;
				this.levelUpButton.GetComponent<Image>().color = this.disabledColor;
                //AdsControl.Instance.showAds();
				UIManager.instance.restartBonusShowTimer();
				SkinManager.instance.checkForUnlocks();
			}, delegate
			{
				ReklamAyarlari.instance.ShowIncentivizedAdForLevelUpDouble();
				UIManager.instance.restartBonusShowTimer();
				SkinManager.instance.checkForUnlocks();
			});
		}
	}

	public void CollectX2LevelUp()
	{
		OyunYoneticisi.instance.collectMoney(FormulHesaplayicisi.instance._getLevelCompletePrizeValue() * 2.0, false);
		OyunYoneticisi.instance.levelUped();
		this.levelProgressBar.fillAmount = 0f;
		this.levelUpButton.GetComponent<Image>().color = this.disabledColor;
	}

	public void updateLevelText()
	{
		this.levelText.text = "Level " + OyunYoneticisi.instance.GameLevel;
	}

	public void speedUpgradeButtonClicked()
	{
		double num = FormulHesaplayicisi.instance._getHizYukseltmeValue();
		if (OyunYoneticisi.instance.money >= num)
		{
			OyunYoneticisi.instance.spendMoney(num);
			OyunYoneticisi.instance.HizSeviyesi++;
			this.checkButtonStates();
			OyunYoneticisi.instance.CarkHiziniArttır();
		}
	}

	public void powerUpgradeButtonClicked()
	{
		double num = FormulHesaplayicisi.instance._getPowerUpgradeValue();
		if (OyunYoneticisi.instance.money >= num)
		{
			OyunYoneticisi.instance.spendMoney(num);
			OyunYoneticisi.instance.GucSeviyesi++;
			this.checkButtonStates();
		}
	}

	public void reduceUpgradeButtonClicked()
	{
	}

	public void gravityUpgradeButtonClicked()
	{
		double num = FormulHesaplayicisi.instance._getGravityUpgradeValue();
		if (OyunYoneticisi.instance.money >= num)
		{
			OyunYoneticisi.instance.spendMoney(num);
			OyunYoneticisi.instance.YercekimiSeviyesi++;
			this.checkButtonStates();
			OyunYoneticisi.instance.ChangeGravity();
		}
	}

	public void toothCountUpgradeButtonClicked()
	{
		double num = FormulHesaplayicisi.instance._getToothCountUpgradeValue();
		if (OyunYoneticisi.instance.money >= num)
		{
			OyunYoneticisi.instance.spendMoney(num);
			OyunYoneticisi.instance.toothCountUpdated();
			this.checkButtonStates();
		}
	}

	public void toothSizeUpgradeButtonClicked()
	{
		double num = FormulHesaplayicisi.instance._getToothSizeUpgradeValue();
		if (OyunYoneticisi.instance.DisBoyutuSeviyesi < 11 && OyunYoneticisi.instance.money >= num)
		{
			OyunYoneticisi.instance.spendMoney(num);
			OyunYoneticisi.instance.DisBoyutuSeviyesi++;
			this.checkButtonStates();
			OyunYoneticisi.instance.UpgradeToothesSize();
		}
	}

	public void marketingUpgradeButtonClicked()
	{
		double num = FormulHesaplayicisi.instance._getMarketingUpgradeValue();
		if (OyunYoneticisi.instance.money >= num)
		{
			OyunYoneticisi.instance.spendMoney(num);
			OyunYoneticisi.instance.MarketingLevel++;
			this.checkButtonStates();
		}
	}

	public void WatchedAdForDoubleCollect()
	{
	}

	public void X3ButtonClicked()
	{
		ReklamAyarlari.instance.ShowIncentivizedAdForX3Speed();
	}

	public void X2ButtonClicked()
	{
		if (this.x3Button.isActive)
		{
			return;
		}
		ReklamAyarlari.instance.ShowIncentivizedAdForX2Speed();
	}

	public void ActivateX2Speed()
	{
		OyunYoneticisi.instance.leftMachine.ChangeMultiplier(2);
		OyunYoneticisi.instance.rightMachine.ChangeMultiplier(2);
		this.x2Button.SetTimer(60);
	}

	public void ActivateX3Speed()
	{
		if (this.x2Button.isActive)
		{
			this.x2Button.StopTimer();
		}
		OyunYoneticisi.instance.leftMachine.ChangeMultiplier(3);
		OyunYoneticisi.instance.rightMachine.ChangeMultiplier(3);
		this.x3Button.SetTimer(180);
	}

	public void restartBonusShowTimer()
	{
		base.StopCoroutine("bonusShower");
		base.StartCoroutine("bonusShower");
	}

	private IEnumerator bonusShower()
	{
		return new UIManager._bonusShower_c__Iterator0();
	}

	public void tutorialNextClicked(int step)
	{
		if (step == 1)
		{
			this.tutorialPanelStep2.SetActive(true);
			this.tutorialPanelStep1.SetActive(false);
		}
		else
		{
			this.tutorialPanel.SetActive(false);
			this.IS_TUTORIAL_COMPLETED = 1;
			this.moneyText.gameObject.SetActive(true);
			this.moneyShortenText.gameObject.SetActive(true);
			BoxManager.instance.createInitialBoxes();
		}
	}

	public void showTutorialPanel()
	{
		this.moneyText.gameObject.SetActive(false);
		this.moneyShortenText.gameObject.SetActive(false);
		this.tutorialPanel.SetActive(true);
		this.tutorialPanelStep1.SetActive(true);
		this.tutorialPanelStep2.SetActive(false);
	}
}
