using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowCountCycle : MonoBehaviour {

	public enum FillType
	{
		None  = 1,
		Zero  = 2,
		HalfWidthSpace = 4
	};

	public FillType fillType = FillType.None;
	public int length = 5;
	public int directCount = 1;
	[Range(0.0f, 1.0f)] public float percent = 0.05f;
	[Range(0.0f, 1.0f)] public float invokeTime = 0.03f;

	private Text text;
	private int show;
	private int delta;
	private int next;
	private bool isCounting;

	void Awake ()
	{
		isCounting = false;
		show = 0;
		next = 0;
		text = GetComponent<Text>();
		Show ();
	}

	private int cycle
	{
		get{
			if (delta > 0) {
				return (int)((float)delta * percent) + directCount;
			} else {
				return (int)((float)delta * percent) - directCount;
			}
		}
	}

	private bool IsOver()
	{
		if (delta > 0) {
			return show >= next;
		} else {
			return show <= next;
		}
	}

	private void Show()
	{
		switch (fillType) {
		case FillType.None:
			text.text = show.ToString ();
			break;
		case FillType.HalfWidthSpace:
			text.text = show.ToString().PadLeft(length);
			break;
		case FillType.Zero:
			text.text = show.ToString().PadLeft(length, '0');
			break;
		}
	}

	private void ShowCoinCounter()
	{
		show += cycle;
		if (IsOver())
		{
			show = next;
			Show ();
			isCounting = false;
			return;
		}

		Show ();
		Invoke ("ShowCoinCounter", invokeTime);
	}

	public void GetNext (int next)
	{
		this.next = next;
		delta = next - show;
		if (isCounting || delta == 0)
		{
			return;
		}

		isCounting = true;
		ShowCoinCounter ();
	}
}