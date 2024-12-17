using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
	public List<Sprite> skinSpriteList;

	public List<Color> skinToothColorList;

	public List<SkinGearMaddesi> skinGearList;

	public GameObject skinPanel;

	public GameObject skinUnlem;

	public static SkinManager instance;

	public DateTime FirstOpenDate
	{
		get
		{
			DateTime maxValue = DateTime.MaxValue;
			if (DateTime.TryParse(PlayerPrefs.GetString("FIRST_OPEN_DATE", DateTime.MaxValue.ToString()), out maxValue))
			{
				return maxValue;
			}
			return DateTime.MaxValue;
		}
		set
		{
			DateTime dateTime = new DateTime(value.Year, value.Month, value.Day, 1, 0, 0, value.Kind);
			PlayerPrefs.SetString("FIRST_OPEN_DATE", dateTime.ToString());
		}
	}

	public int SON_SECILEN_SKIN_NO
	{
		get
		{
			return PlayerPrefs.GetInt("SON_SECILEN_SKIN_NO", 1);
		}
		set
		{
			PlayerPrefs.SetInt("SON_SECILEN_SKIN_NO", value);
		}
	}

	public string TAMAMLANAN_SKINLER
	{
		get
		{
			return PlayerPrefs.GetString("TAMAMLANAN_SKINLER", "_1_");
		}
		set
		{
			PlayerPrefs.SetString("TAMAMLANAN_SKINLER", value);
		}
	}

	public int SUBSCRIBED_TO_YOUTUBE
	{
		get
		{
			return PlayerPrefs.GetInt("SUBSCRIBED_TO_YOUTUBE", 0);
		}
		set
		{
			PlayerPrefs.SetInt("SUBSCRIBED_TO_YOUTUBE", value);
		}
	}

	private void Awake()
	{
		SkinManager.instance = this;
	}

	private void Start()
	{
		if (this.FirstOpenDate.Date.Equals(DateTime.MaxValue.Date))
		{
			UnityEngine.Debug.Log(" $$$ FirstOpenDate1 : " + this.FirstOpenDate);
			this.FirstOpenDate = DateTime.Now;
		}
		for (int i = 0; i < this.skinGearList.Count; i++)
		{
			SkinGearMaddesi skinGearMaddesi = this.skinGearList[i];
			skinGearMaddesi.setSkinId(i + 1);
		}
		if (this.SON_SECILEN_SKIN_NO == 3 && this.SUBSCRIBED_TO_YOUTUBE == 0)
		{
			this.SON_SECILEN_SKIN_NO = 1;
		}
		this.skinGearList[this.SON_SECILEN_SKIN_NO - 1].select();
		this.checkForUnlocksPrivate();
		UnityEngine.Debug.Log(" $$$ FirstOpenDate : " + this.FirstOpenDate);
	}

	public void checkForUnlocks()
	{
		base.Invoke("checkForUnlocksPrivate", 0.5f);
	}

	private void checkForUnlocksPrivate()
	{
		foreach (SkinGearMaddesi current in this.skinGearList)
		{
			current.checkForUnlock();
		}
	}

	public int isSkinUnlocked(int skinId)
	{
		if (skinId == 1)
		{
			return 1;
		}
		if (this.TAMAMLANAN_SKINLER.Contains(string.Empty + skinId) || (skinId == 3 && this.SUBSCRIBED_TO_YOUTUBE == 1))
		{
			return 1;
		}
		double num = 0.0;
		if (!this.FirstOpenDate.Date.Equals(DateTime.MaxValue.Date))
		{
			DateTime now = DateTime.Now;
			num = (double)(now - this.FirstOpenDate).Days;
		}
		switch (skinId)
		{
		case 2:
			if (OyunYoneticisi.instance.GameLevel >= 2)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 3:
			return this.SUBSCRIBED_TO_YOUTUBE;
		case 4:
			if (num >= 1.0)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 5:
			if (OyunYoneticisi.instance.GameLevel >= 5)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 6:
			if (OyunYoneticisi.instance.GameLevel >= 7)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 7:
			if (num >= 3.0)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 8:
			if (OyunYoneticisi.instance.GameLevel >= 9)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 9:
			if (OyunYoneticisi.instance.GameLevel >= 10)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 10:
			if (num >= 7.0)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 11:
			if (OyunYoneticisi.instance.GameLevel >= 12)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		case 12:
			if (OyunYoneticisi.instance.GameLevel >= 15)
			{
				string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
				this.TAMAMLANAN_SKINLER = string.Concat(new object[]
				{
					tAMAMLANAN_SKINLER,
					"_",
					skinId,
					"_"
				});
				return 2;
			}
			return 0;
		default:
			return 0;
		}
	}

	public void deselectAll()
	{
		foreach (SkinGearMaddesi current in this.skinGearList)
		{
			current.deselect();
		}
	}

	public void showPanel()
	{
		this.skinPanel.gameObject.SetActive(true);
		this.skinUnlem.gameObject.SetActive(false);
	}

	public void showUnlem()
	{
		this.skinUnlem.gameObject.SetActive(true);
	}

	public void unlockedThirdGear()
	{
		this.SUBSCRIBED_TO_YOUTUBE = 1;
		string tAMAMLANAN_SKINLER = this.TAMAMLANAN_SKINLER;
		this.TAMAMLANAN_SKINLER = string.Concat(new object[]
		{
			tAMAMLANAN_SKINLER,
			"_",
			3,
			"_"
		});
	}
}
