using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OyunYoneticisi : MonoBehaviour
{
   
	public static OyunYoneticisi instance;

	public double numberOfRequiredBlockDestruction;

	public int currentLevelCompletePercentange;

	public int numberOfDestroyedBLockInThisLevel;

	public Machine leftMachine;

	public Machine rightMachine;

	public Transform boxTriggerTransform;

	public double money;

	[HideInInspector]
	public bool isComingFromVideoWatching;

	private string OYUNDAN_SON_CIKIS_ZAMANI = "OYUNDAN_SON_CIKIS_ZAMANI";

	public string SonIdleOdulu
	{
		get
		{
			return PlayerPrefs.GetString("SonIdleOdulu", "0");
		}
		set
		{
			PlayerPrefs.SetString("SonIdleOdulu", value);
		}
	}

	public string HafizadakiPara
	{
		get
		{
			return PlayerPrefs.GetString("HafizadakiPara", "0");
		}
		set
		{
			PlayerPrefs.SetString("HafizadakiPara", value);
		}
	}

	public int HizSeviyesi
	{
		get
		{
			return PlayerPrefs.GetInt("HizSeviyesi", 1);
		}
		set
		{
			PlayerPrefs.SetInt("HizSeviyesi", value);
		}
	}

	public int GucSeviyesi
	{
		get
		{
			return PlayerPrefs.GetInt("GucSeviyesi", 1);
		}
		set
		{
			PlayerPrefs.SetInt("GucSeviyesi", value);
		}
	}

	public int Azaltici
	{
		get
		{
			return PlayerPrefs.GetInt("Azaltici", 1);
		}
		set
		{
			PlayerPrefs.SetInt("Azaltici", value);
		}
	}

	public int YercekimiSeviyesi
	{
		get
		{
			return PlayerPrefs.GetInt("YercekimiSeviyesi", 1);
		}
		set
		{
			PlayerPrefs.SetInt("YercekimiSeviyesi", value);
		}
	}

	public int DisSayisiSeviyesi
	{
		get
		{
			return PlayerPrefs.GetInt("DisSayisiSeviyesi", 1);
		}
		set
		{
			PlayerPrefs.SetInt("DisSayisiSeviyesi", value);
		}
	}

	public int DisBoyutuSeviyesi
	{
		get
		{
			return PlayerPrefs.GetInt("DisBoyutuSeviyesi", 1);
		}
		set
		{
			PlayerPrefs.SetInt("DisBoyutuSeviyesi", value);
		}
	}

	public int MarketingLevel
	{
		get
		{
			return PlayerPrefs.GetInt("MarketingLevel", 1);
		}
		set
		{
			PlayerPrefs.SetInt("MarketingLevel", value);
		}
	}

	public int GameLevel
	{
		get
		{
			return PlayerPrefs.GetInt("GameLevel", 1);
		}
		set
		{
			PlayerPrefs.SetInt("GameLevel", value);
		}
	}

	public int BuSeviyedeYokEdilenBlokSayisi
	{
		get
		{
			return PlayerPrefs.GetInt("BuSeviyedeYokEdilenBlokSayisi", 0);
		}
		set
		{
			PlayerPrefs.SetInt("BuSeviyedeYokEdilenBlokSayisi", value);
		}
	}

	private void Awake()
	{
		OyunYoneticisi.instance = this;
		Application.targetFrameRate = 60;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.M))
		{
			this.money += 500000000.0;
			UIManager.instance.updateMoney(this.money);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.D))
		{
			PlayerPrefs.DeleteAll();
		}
	}

	private void Start()
	{
		Application.targetFrameRate = 60;
		this.money = double.Parse(this.HafizadakiPara);
		this.numberOfDestroyedBLockInThisLevel = this.BuSeviyedeYokEdilenBlokSayisi;
		this.numberOfRequiredBlockDestruction = FormulHesaplayicisi.instance._getNumberOfBoxToDestroyForLevelComplete();
		UIManager.instance.initUI();
		UIManager.instance.updateMoney(this.money);
		UIManager.instance.updateLevelText();
		UIManager.instance.updateProgressBar((float)((double)((float)this.numberOfDestroyedBLockInThisLevel * 1f) / this.numberOfRequiredBlockDestruction));
		this.CarkHiziniArttır();
		this.ChangeGravity();
		base.Invoke("verilecekOfflineOduluHesapla", 0.5f);
	}

	public void collectMoney(double amount, bool isABlockDestroyed)
	{
		if (isABlockDestroyed && (double)this.numberOfDestroyedBLockInThisLevel < this.numberOfRequiredBlockDestruction)
		{
			this.numberOfDestroyedBLockInThisLevel++;
			UIManager.instance.updateProgressBar((float)((double)((float)this.numberOfDestroyedBLockInThisLevel * 1f) / this.numberOfRequiredBlockDestruction));
		}
		this.money += amount;
		UIManager.instance.updateMoney(this.money);
	}

	public void spendMoney(double amount)
	{
		this.money -= amount;
		UIManager.instance.updateMoney(this.money);
	}

	public void levelUped()
	{
		this.GameLevel++;
		this.numberOfDestroyedBLockInThisLevel = 0;
		this.numberOfRequiredBlockDestruction = FormulHesaplayicisi.instance._getNumberOfBoxToDestroyForLevelComplete();
		UIManager.instance.updateLevelText();
		UIManager.instance.updateProgressBar((float)((double)((float)this.numberOfDestroyedBLockInThisLevel * 1f) / this.numberOfRequiredBlockDestruction));
	}

	public void toothCountUpdated()
	{
		this.DisSayisiSeviyesi++;
		this.leftMachine.reArrangeTooths();
		this.rightMachine.reArrangeTooths();
	}

	private void saveLastState()
	{
		this.HafizadakiPara = string.Empty + Math.Truncate(this.money);
		this.BuSeviyedeYokEdilenBlokSayisi = this.numberOfDestroyedBLockInThisLevel;
	}

	private void OnApplicationQuit()
	{
		this.saveLastState();
		this.saveLeaveTime();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			this.saveLastState();
			this.saveLeaveTime();
		}
		else
		{
			if (!this.isComingFromVideoWatching)
			{
				this.verilecekOfflineOduluHesapla();
			}
			this.isComingFromVideoWatching = false;
		}
	}

	public void saveLeaveTime()
	{
		DateTime now = DateTime.Now;
		PlayerPrefs.SetString(this.OYUNDAN_SON_CIKIS_ZAMANI, now.ToString());
	}

	public DateTime getLeaveTime()
	{
		DateTime result = DateTime.MaxValue;
		string @string = PlayerPrefs.GetString(this.OYUNDAN_SON_CIKIS_ZAMANI);
		if (@string != null && @string.Length > 0)
		{
			result = DateTime.Parse(@string);
		}
		return result;
	}

	private void verilecekOfflineOduluHesapla()
	{
		if (UIManager.instance.IS_TUTORIAL_COMPLETED == 0 && OyunYoneticisi.instance.GameLevel > 1)
		{
			UIManager.instance.IS_TUTORIAL_COMPLETED = 1;
		}
		if (UIManager.instance.infoPanel.gameObject.activeInHierarchy || UIManager.instance.IS_TUTORIAL_COMPLETED == 0)
		{
			return;
		}
		DateTime now = DateTime.Now;
		double num = (now - this.getLeaveTime()).TotalSeconds;
		UnityEngine.Debug.Log(" $$$ totalPassedSeconds  " + num);
		if (num < 5.0)
		{
			return;
		}
		if (num >= 86400.0)
		{
			num = 86400.0;
		}
		double result = num / 86400.0 * FormulHesaplayicisi.instance._getOneDayIdlePrizeForLevel();
		if (result < 100.0)
		{
			result = (double)UnityEngine.Random.Range(50, 150);
		}
		UIManager.instance.infoPanel.OpenPanel("Earned\nwhile you were away:", result, delegate
		{
			this.collectMoney(result, false);
			UIManager.instance.restartBonusShowTimer();
			SkinManager.instance.checkForUnlocks();
		}, delegate
		{

            /*AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {
                this.collectMoney(result * 2.0, false);
                UIManager.instance.restartBonusShowTimer();
                SkinManager.instance.checkForUnlocks();
            });*/
        });
	}

	public void CarkHiziniArttır()
	{
		float num = 0.13f + (float)this.HizSeviyesi * 0.014f;
		num = Math.Min(num, 0.8f);
		this.boxTriggerTransform.localScale = new Vector3(this.boxTriggerTransform.localScale.x, num, this.boxTriggerTransform.localScale.z);
		int hizSeviyesi = this.HizSeviyesi;
		this.leftMachine.ChangeSpeed(hizSeviyesi);
		this.rightMachine.ChangeSpeed(hizSeviyesi);
	}

	public void UpgradeToothesSize()
	{
		this.leftMachine.ChangeToothsSize();
		this.rightMachine.ChangeToothsSize();
	}

	public void ChangeGravity()
	{
		float num = (float)FormulHesaplayicisi.instance._getGravityDisplayValue();
		if (num > 30f)
		{
			num = 30f;
		}
		Physics2D.gravity = new Vector2(0f, num * -1f);
	}

	public void skinChanged()
	{
		Sprite sprite = SkinManager.instance.skinSpriteList[SkinManager.instance.SON_SECILEN_SKIN_NO - 1];
		Color color = SkinManager.instance.skinToothColorList[SkinManager.instance.SON_SECILEN_SKIN_NO - 1];
		this.leftMachine.GetComponent<SpriteRenderer>().sprite = sprite;
		this.rightMachine.GetComponent<SpriteRenderer>().sprite = sprite;
		foreach (GameObject current in this.leftMachine.toothList)
		{
			current.GetComponent<SpriteRenderer>().color = color;
		}
		foreach (GameObject current2 in this.rightMachine.toothList)
		{
			current2.GetComponent<SpriteRenderer>().color = color;
		}
	}
}
