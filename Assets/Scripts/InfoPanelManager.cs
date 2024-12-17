using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour
{
   
	public Text infoText;

	public Text priceText;

	public Button collect;

	public Button collectX2;

	private Color yellowColor = new Color(0.8745098f, 0.9137255f, 0.20784314f, 1f);

	private Color greyColor = new Color(0.619607866f, 0.647058845f, 0.68235296f, 1f);

	private void Awake()
	{
		this.SetVideoAvailable(false);
	}

	public void OpenPanel(string info, double price, Action collectAction, Action collectX2Action)
	{
		this.infoText.text = info;
		this.priceText.text = "$" + price.ToSymbol(false);
		this.collect.onClick.RemoveAllListeners();
		this.collectX2.onClick.RemoveAllListeners();
		this.collect.onClick.AddListener(delegate
		{
			if (collectAction != null)
			{
				collectAction();
			}
			this.gameObject.SetActive(false);
		});
		this.collectX2.onClick.AddListener(delegate
		{
			if (collectX2Action != null)
			{
				collectX2Action();
			}
			this.gameObject.SetActive(false);
		});
		base.gameObject.SetActive(true);
        //this.SetVideoAvailable(ReklamAyarlari.instance.isIncentivizedAvailable());
       // this.SetVideoAvailable(AdsControl.Instance.GetRewardAvailable());
    }

	public void SetVideoAvailable(bool isAvailable)
	{
		this.collectX2.interactable = isAvailable;
		this.collectX2.GetComponent<Image>().color = ((!isAvailable) ? this.greyColor : this.yellowColor);
	}
}
