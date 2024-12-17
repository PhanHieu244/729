using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Box : MonoBehaviour
{
	private sealed class _flashColor_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Box _this;

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

		public _flashColor_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this._spriteRenderer != null)
				{
					this._this._spriteRenderer.color = new Color(0.945098042f, 1f, 0f, 1f);
					this._current = new WaitForSeconds(0.1f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				break;
			case 1u:
				this._this._spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
				break;
			default:
				return false;
			}
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

	private float life;

	public BoxType boxType;

	private Rigidbody2D rigid;

	private bool collidedWithTerrain;

	private bool changedLayer;

	private bool doNotTouchTooths;

	private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		this.rigid = base.GetComponent<Rigidbody2D>();
		this._spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.collidedWithTerrain = false;
		this.life = 3f;
		this.changedLayer = false;
	}

	public void init(BoxType boxTypeIn)
	{
		this.boxType = boxTypeIn;
	}

	private void FixedUpdate()
	{
		if (!this.changedLayer && base.transform.position.y <= 0f)
		{
			base.gameObject.layer = 0;
			this.changedLayer = true;
			BoxManager.instance.DecreaseSmallBoxCount();
		}
		if (!this.collidedWithTerrain)
		{
			return;
		}
		float num = Physics2D.gravity.magnitude / 9.8f;
		num = Mathf.Min(num, 4f);
		this.rigid.AddForce(Vector2.right * 15f * num);
	}

	public void CollidedWithTerrain()
	{
		this.collidedWithTerrain = true;
		float num = Physics2D.gravity.magnitude / 9.8f;
		num = Mathf.Min(num, 4f);
		this.rigid.velocity = Vector2.right * 3f * num;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (this.doNotTouchTooths)
		{
			return;
		}
		if (coll.contacts.Length > 0)
		{
			float num = Physics2D.gravity.magnitude / 9.8f;
			num = Mathf.Min(num, 4f);
			this.rigid.AddForce(coll.contacts[0].normal * 55f * num);
		}
		if (this.boxType != BoxType.SMALL && coll.gameObject.CompareTag("Tooth"))
		{
			this.life -= 0.75f;
			if (this.life <= 0f)
			{
				BoxManager.instance.DestroyAndCreateNewBoxes(this, this.boxType);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.boxType == BoxType.SMALL && other.CompareTag("Boxx"))
		{
			this.doNotTouchTooths = true;
			base.gameObject.transform.eulerAngles = Vector3.zero;
			base.gameObject.transform.position = new Vector3(0f, base.gameObject.transform.position.y);
			this.rigid.angularVelocity = 0f;
			base.gameObject.layer = 9;
		}
	}

	public void tapped()
	{
		this.life -= 0.44f;
		base.StopCoroutine("flashColor");
		base.StartCoroutine("flashColor");
		if (this.life <= 0f)
		{
			BoxManager.instance.DestroyAndCreateNewBoxes(this, this.boxType);
		}
	}

	private IEnumerator flashColor()
	{
		Box._flashColor_c__Iterator0 _flashColor_c__Iterator = new Box._flashColor_c__Iterator0();
		_flashColor_c__Iterator._this = this;
		return _flashColor_c__Iterator;
	}
}
