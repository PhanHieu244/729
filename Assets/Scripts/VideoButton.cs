using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class VideoButton : MonoBehaviour
{
	private sealed class _TimerCountDown_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal VideoButton _this;

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

		public _TimerCountDown_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.isActive = true;
				this._this.UpdateTimerText();
				break;
			case 1u:
				this._this.timerSecond--;
				this._this.UpdateTimerText();
				if (this._this.timerSecond <= 0)
				{
					OyunYoneticisi.instance.leftMachine.ChangeMultiplier(1);
					OyunYoneticisi.instance.rightMachine.ChangeMultiplier(1);
					this._this.ChangeDefault();
					this._PC = -1;
					return false;
				}
				break;
			default:
				return false;
			}
			this._current = new WaitForSeconds(1f);
			if (!this._disposing)
			{
				this._PC = 1;
			}
			return true;
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

	public Text timerText;

	public GameObject x2VideoIcon;

	public Image videoImage;

	public Image bgImage;

	public int timerSecond;

	private Color yellowColor = new Color(0.8745098f, 0.9137255f, 0.20784314f, 1f);

	private Color greyColor = new Color(0.619607866f, 0.647058845f, 0.68235296f, 1f);

	private Color bgColor = new Color(0.443137258f, 0.470588237f, 0.5019608f, 1f);

	[HideInInspector]
	public bool isActive;

	private void Start()
	{
		this.videoImage.color = this.greyColor;
		base.GetComponent<Button>().interactable = false;
		this.isActive = false;
	}

	public void VideoAvailable()
	{
		this.videoImage.color = this.yellowColor;
		base.GetComponent<Button>().interactable = true;
	}

	public void SetTimer(int second)
	{
		base.StopCoroutine("TimerCountDown");
		this.x2VideoIcon.SetActive(false);
		this.timerText.gameObject.SetActive(true);
		this.bgImage.color = this.yellowColor;
		this.videoImage.color = this.greyColor;
		this.timerSecond = second;
		base.StartCoroutine("TimerCountDown");
	}

	public void StopTimer()
	{
		base.StopCoroutine("TimerCountDown");
		this.ChangeDefault();
	}

	private IEnumerator TimerCountDown()
	{
		VideoButton._TimerCountDown_c__Iterator0 _TimerCountDown_c__Iterator = new VideoButton._TimerCountDown_c__Iterator0();
		_TimerCountDown_c__Iterator._this = this;
		return _TimerCountDown_c__Iterator;
	}

	private void ChangeDefault()
	{
		this.x2VideoIcon.SetActive(true);
		this.timerText.gameObject.SetActive(false);
		this.bgImage.color = this.bgColor;
		this.videoImage.color = this.yellowColor;
		this.isActive = false;
	}

	private void UpdateTimerText()
	{
		this.timerText.text = this.timerSecond.ToString();
	}
}
