using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinGearMaddesi : MonoBehaviour
{
	private Color disabledColor = new Color(0.470588237f, 0.470588237f, 0.470588237f, 0.470588237f);

	private Color enabledColor = new Color(1f, 1f, 1f, 1f);

	private int skinId;

	public Image gearImage;

	public Button gearButton;

	public Text gearText;

	public Image youtubeSubscribeImage;

	public void setSkinId(int skinIdIn)
	{
		this.skinId = skinIdIn;
		this.gearImage.sprite = SkinManager.instance.skinSpriteList[this.skinId - 1];
	}

	public void checkForUnlock()
	{
		int num = SkinManager.instance.isSkinUnlocked(this.skinId);
		if (this.skinId == 3)
		{
			if (SkinManager.instance.SUBSCRIBED_TO_YOUTUBE == 0)
			{
				this.youtubeSubscribeImage.gameObject.SetActive(true);
			}
			else
			{
				this.youtubeSubscribeImage.gameObject.SetActive(false);
			}
		}
		if (num >= 1 || (this.skinId == 3 && SkinManager.instance.SUBSCRIBED_TO_YOUTUBE == 1))
		{
			this.gearText.gameObject.SetActive(false);
			this.gearImage.color = this.enabledColor;
			if (num == 2)
			{
				SkinManager.instance.showUnlem();
			}
		}
		else
		{
			this.gearText.gameObject.SetActive(true);
			this.gearImage.color = this.disabledColor;
		}
	}

	public void deselect()
	{
		this.gearButton.gameObject.SetActive(false);
	}

	public void select()
	{
		SkinManager.instance.deselectAll();
		this.gearButton.gameObject.SetActive(true);
		UnityEngine.Debug.Log(" $$$ New Skin selected : " + this.skinId);
		SkinManager.instance.SON_SECILEN_SKIN_NO = this.skinId;
		OyunYoneticisi.instance.skinChanged();
	}

	public void skinClicked()
	{
		int num = SkinManager.instance.isSkinUnlocked(this.skinId);
		if (this.skinId == 3 && SkinManager.instance.SUBSCRIBED_TO_YOUTUBE == 0)
		{
			OyunYoneticisi.instance.isComingFromVideoWatching = true;
			UnityEngine.Debug.Log(" $$$ Youtube Subscribe clicked.");
			base.Invoke("unlockSkin", 1f);
			Application.OpenURL("https://www.youtube.com/channel/UC5nQ8-95UeSYuMgnaf_iy5A");
			return;
		}
		if (!this.gearText.gameObject.activeInHierarchy || (this.skinId == 3 && SkinManager.instance.SUBSCRIBED_TO_YOUTUBE == 1))
		{
			this.select();
		}
	}

	private void unlockSkin()
	{
		this.youtubeSubscribeImage.gameObject.SetActive(false);
		SkinManager.instance.unlockedThirdGear();
		this.gearText.gameObject.SetActive(false);
		this.gearImage.color = this.enabledColor;
		this.select();
	}
}
