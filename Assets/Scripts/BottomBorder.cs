using System;
using UnityEngine;

public class BottomBorder : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("SmallBox"))
		{
			coll.gameObject.GetComponent<Box>().CollidedWithTerrain();
		}
	}
}
