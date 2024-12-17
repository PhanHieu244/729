using System;
using UnityEngine;

public class RATESCRIPT : MonoBehaviour
{
	private string oyunlinki_1 = "https://play.google.com/store/apps/details?id=com.foxgames.IdleCrush";

	private string oyunlinki_2 = "https://itunes.apple.com";

	private int oyunBittiSayisi;

	private int GAME_OPEN_COUNT;

	public static RATESCRIPT instance;

	public GameObject kendiPaneli;

	private void Awake()
	{
		RATESCRIPT.instance = this;
	}

	private void Start()
	{
		this.oyunBittiSayisi = 0;
		this.GAME_OPEN_COUNT = PlayerPrefs.GetInt("GAME_OPEN_COUNT", 0);
		this.GAME_OPEN_COUNT++;
		PlayerPrefs.SetInt("GAME_OPEN_COUNT", this.GAME_OPEN_COUNT);
	}

	public void oylamayiGoster()
	{
		this.oyunBittiSayisi++;
		if (this.oyunBittiSayisi > 1)
		{
			return;
		}
		if (this.GAME_OPEN_COUNT >= 2 && PlayerPrefs.GetInt("HASRATED", 0) == 0)
		{
			this.kendiPaneli.SetActive(true);
		}
	}

	public void closeee(int deger)
	{
		this.kendiPaneli.SetActive(false);
		if (deger != -1)
		{
			if (deger == 1)
			{
				PlayerPrefs.SetInt("HASRATED", 1);
				if (!string.IsNullOrEmpty(this.oyunlinki_1))
				{
					Application.OpenURL(this.oyunlinki_1);
				}
			}
		}
		else
		{
			PlayerPrefs.SetInt("HASRATED", -1);
		}
	}
}
