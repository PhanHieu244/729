using System;
using UnityEngine;

public class SagaGidenleriTopla : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		BoxManager.instance.DestroySmallBox(other.gameObject);
		OyunYoneticisi.instance.collectMoney(FormulHesaplayicisi.instance._getMarketingDisplayValue(), true);
	}
}
