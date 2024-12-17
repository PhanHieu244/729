using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
	public GameObject largeBoxPrefab;

	public GameObject middleBoxPrefab;

	public GameObject smallBoxPrefab;

	private List<Box> largeAndMiddleBoxList;

	private float MIN_X = -2f;

	private float MAX_X = 2f;

	private float Y_POS = 8f;

	private int NUMBER_OF_MAX_ACTIVE_BOX = 20;

	private int largeCount;

	private int middleCount;

	private int smallCount;

	public static BoxManager instance;

	private bool inDrag;

	private void Awake()
	{
		BoxManager.instance = this;
	}

	private void Start()
	{
		this.largeAndMiddleBoxList = new List<Box>();
		if (UIManager.instance.IS_TUTORIAL_COMPLETED == 0 && OyunYoneticisi.instance.GameLevel > 1)
		{
			UIManager.instance.IS_TUTORIAL_COMPLETED = 1;
		}
		if (UIManager.instance.IS_TUTORIAL_COMPLETED == 0)
		{
			UIManager.instance.showTutorialPanel();
		}
		else
		{
			this.createInitialBoxes();
		}
	}

	public void createInitialBoxes()
	{
		int num = 4;
		int num2 = 4;
		int num3 = 4;
		this.largeCount = 4;
		this.middleCount = 4;
		this.smallCount = 4;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.smallBoxPrefab, Vector2.zero, Quaternion.identity);
			gameObject.SetActive(true);
			Box component = gameObject.GetComponent<Box>();
			component.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(this.MIN_X, this.MAX_X), this.Y_POS);
			component.init(BoxType.SMALL);
		}
		for (int j = 0; j < num2; j++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.middleBoxPrefab, Vector2.zero, Quaternion.identity);
			gameObject2.SetActive(true);
			Box component2 = gameObject2.GetComponent<Box>();
			component2.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(this.MIN_X, this.MAX_X), this.Y_POS);
			component2.init(BoxType.MIDDLE);
			this.largeAndMiddleBoxList.Add(component2);
		}
		for (int k = 0; k < num3; k++)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.largeBoxPrefab, Vector2.zero, Quaternion.identity);
			gameObject3.SetActive(true);
			Box component3 = gameObject3.GetComponent<Box>();
			component3.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(this.MIN_X, this.MAX_X), this.Y_POS);
			component3.init(BoxType.LARGE);
			this.largeAndMiddleBoxList.Add(component3);
		}
		base.InvokeRepeating("CreateNewBoxes", 3f, 1.5f);
	}

	private void CreateNewBoxes()
	{
		if (this.middleCount + this.largeCount > this.NUMBER_OF_MAX_ACTIVE_BOX)
		{
			UnityEngine.Debug.Log("geldiiii: " + (this.middleCount + this.largeCount));
			return;
		}
		int num = UnityEngine.Random.Range(4, 7);
		int num2 = UnityEngine.Random.Range(5, 15);
		if (this.largeCount < num)
		{
			for (int i = 0; i < num - this.largeCount; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.largeBoxPrefab, Vector2.zero, Quaternion.identity);
				gameObject.SetActive(true);
				Box component = gameObject.GetComponent<Box>();
				component.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(this.MIN_X, this.MAX_X), this.Y_POS);
				component.init(BoxType.LARGE);
				this.largeAndMiddleBoxList.Add(component);
				this.largeCount++;
			}
		}
		if (this.middleCount < num2)
		{
			int num3 = this.NUMBER_OF_MAX_ACTIVE_BOX - this.smallCount - num - this.middleCount;
			for (int j = 0; j < num3; j++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.middleBoxPrefab, Vector2.zero, Quaternion.identity);
				gameObject2.SetActive(true);
				Box component2 = gameObject2.GetComponent<Box>();
				component2.gameObject.transform.position = new Vector2(UnityEngine.Random.Range(this.MIN_X, this.MAX_X), this.Y_POS);
				component2.init(BoxType.MIDDLE);
				this.largeAndMiddleBoxList.Add(component2);
				this.middleCount++;
			}
		}
	}

	public void DestroyAndCreateNewBoxes(Box box, BoxType destroyedType)
	{
		this.CalculateNewCounts(destroyedType);
		Vector2 a = box.transform.position;
		float num = box.transform.localScale.x / 2f;
		UnityEngine.Object.Destroy(box.gameObject);
		this.largeAndMiddleBoxList.Remove(box);
		BoxType boxType = (destroyedType != BoxType.LARGE) ? BoxType.SMALL : BoxType.MIDDLE;
		GameObject original = (boxType != BoxType.MIDDLE) ? this.smallBoxPrefab : this.middleBoxPrefab;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, a + new Vector2(num, num), Quaternion.identity);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original, a + new Vector2(-num, num), Quaternion.identity);
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(original, a + new Vector2(num, -num), Quaternion.identity);
		Box component = gameObject.GetComponent<Box>();
		Box component2 = gameObject2.GetComponent<Box>();
		Box component3 = gameObject3.GetComponent<Box>();
		if (destroyedType == BoxType.LARGE)
		{
			component.init(BoxType.MIDDLE);
			component2.init(BoxType.MIDDLE);
			component3.init(BoxType.MIDDLE);
			this.largeAndMiddleBoxList.Add(component);
			this.largeAndMiddleBoxList.Add(component2);
			this.largeAndMiddleBoxList.Add(component3);
		}
		else
		{
			component.init(BoxType.SMALL);
			component2.init(BoxType.SMALL);
			component3.init(BoxType.SMALL);
		}
	}

	public void DecreaseSmallBoxCount()
	{
		this.smallCount--;
		if (this.smallCount < 0)
		{
			this.smallCount = 0;
		}
	}

	public void DestroySmallBox(GameObject go)
	{
		UnityEngine.Object.Destroy(go);
	}

	private void CalculateNewCounts(BoxType type)
	{
		if (type == BoxType.LARGE)
		{
			this.largeCount--;
			if (this.largeCount < 0)
			{
				this.largeCount = 0;
			}
			this.middleCount += 3;
		}
		else if (type == BoxType.MIDDLE)
		{
			this.middleCount--;
			if (this.middleCount < 0)
			{
				this.middleCount = 0;
			}
			this.smallCount += 3;
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.inDrag = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.inDrag = false;
		}
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 vector = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			foreach (Box current in this.largeAndMiddleBoxList.ToList<Box>())
			{
				if (current != null && current.boxType != BoxType.SMALL)
				{
					float num = vector.x - current.transform.position.x;
					float num2 = vector.y - current.transform.position.y;
					float num3 = 0.7f;
					if (current.boxType == BoxType.MIDDLE)
					{
						num3 = 0.35f;
					}
					if (num * num + num2 * num2 < num3)
					{
						current.tapped();
						break;
					}
				}
			}
		}
	}
}
