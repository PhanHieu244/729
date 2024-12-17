using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
	public bool isLeft;

	public float currentSpeed;

	public GameObject toothPrefab;

	private float RADIUS = 1.5f;

	private int multiplier;

	[HideInInspector]
	public List<GameObject> toothList;

	private void Start()
	{
		this.toothList = new List<GameObject>();
		this.currentSpeed = 90f;
		this.reArrangeTooths();
		this.ChangeSpeed(OyunYoneticisi.instance.HizSeviyesi);
		this.multiplier = 1;
	}

	public void reArrangeTooths()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.toothList.Clear();
		this.arrangeTooths((int)FormulHesaplayicisi.instance._getToothCountDisplayValue());
	}

	private void arrangeTooths(int toothCount)
	{
		float num = (float)(360 / toothCount);
		float num2 = 0.5f + 0.05f * (float)OyunYoneticisi.instance.DisBoyutuSeviyesi;
		for (int i = 0; i < toothCount; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.toothPrefab, Vector2.zero, Quaternion.identity);
			gameObject.transform.localScale = new Vector3(num2, num2, 1f);
			gameObject.gameObject.SetActive(true);
			gameObject.transform.SetParent(base.gameObject.transform);
			float num3 = Mathf.Cos((float)i * num * 0.0174532924f);
			float num4 = Mathf.Sin((float)i * num * 0.0174532924f);
			gameObject.transform.localPosition = new Vector2(this.RADIUS * num3, this.RADIUS * num4);
			gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 270f + (float)i * num);
			this.toothList.Add(gameObject);
		}
		if (!this.isLeft)
		{
			float z = OyunYoneticisi.instance.leftMachine.transform.eulerAngles.z;
			float z2 = z - num / 2f;
			base.transform.eulerAngles = new Vector3(0f, 0f, z2);
		}
	}

	private void Update()
	{
		float num = this.currentSpeed * (float)this.multiplier;
		if (num > 720f)
		{
			num = 720f;
		}
		if (this.isLeft)
		{
			base.transform.Rotate(Vector3.forward * -num * Time.deltaTime);
		}
		else
		{
			base.transform.Rotate(Vector3.forward * num * Time.deltaTime);
		}
	}

	public void ChangeSpeed(int value)
	{
		this.currentSpeed = (float)(90 + value * 9);
	}

	public void ChangeToothsSize()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			float num = 0.5f + 0.05f * (float)OyunYoneticisi.instance.DisBoyutuSeviyesi;
			base.transform.GetChild(i).localScale = new Vector3(num, num, 1f);
		}
	}

	public void ChangeMultiplier(int value)
	{
		this.multiplier = value;
	}
}
