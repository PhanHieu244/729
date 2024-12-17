using System;

public static class Uzantilar
{
	public static string SecondToText(this float time)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)time);
		if (time >= 3600f)
		{
			return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}
		return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
	}

	public static string ToSymbol(this double val, bool showDetail = false)
	{
		if (val == 0.0)
		{
			return "0";
		}
		if (val < 999.0)
		{
			return val.ToString("#");
		}
		string text = Math.Ceiling(val).ToString("0." + new string('#', 339));
		string text2 = text.Substring(0, 3);
		int num = text.Length % 3;
		char c = text2[0];
		char c2 = text2[1];
		char c3 = text2[2];
		if (num != 0)
		{
			if (num == 1)
			{
				char[] value = new char[]
				{
					c,
					',',
					c2,
					c3
				};
				if (c3 == '0')
				{
					value = new char[]
					{
						c,
						',',
						c2
					};
				}
				text2 = new string(value);
			}
			else if (num == 2)
			{
				char[] value2 = new char[]
				{
					c,
					c2,
					',',
					c3
				};
				if (c3 == '0')
				{
					value2 = new char[]
					{
						c,
						c2
					};
				}
				text2 = new string(value2);
			}
		}
		if (text.Length <= 99)
		{
			text2 += Uzantilar.lengthToQuantity(text.Length);
		}
		else
		{
			int length = text.Length;
			int num2 = length / 96 * 96;
			int num3 = length - num2;
			if (num3 == 0)
			{
				num3 = 96;
			}
			text2 += Uzantilar.lengthToQuantity(num3);
		}
		return text2;
	}

	private static string lengthToQuantity(int length)
	{
		if (length <= 6)
		{
			return "K";
		}
		if (length <= 9)
		{
			return "M";
		}
		if (length <= 12)
		{
			return "B";
		}
		if (length <= 15)
		{
			return "T";
		}
		if (length <= 18)
		{
			return "q";
		}
		if (length <= 21)
		{
			return "Q";
		}
		if (length <= 24)
		{
			return "s";
		}
		if (length <= 27)
		{
			return "S";
		}
		if (length <= 30)
		{
			return "O";
		}
		if (length <= 33)
		{
			return "N";
		}
		if (length <= 36)
		{
			return "d";
		}
		if (length <= 39)
		{
			return "U";
		}
		if (length <= 42)
		{
			return "D";
		}
		if (length <= 45)
		{
			return "R";
		}
		if (length <= 48)
		{
			return "g";
		}
		if (length <= 51)
		{
			return "G";
		}
		if (length <= 54)
		{
			return "w";
		}
		if (length <= 57)
		{
			return "W";
		}
		if (length <= 60)
		{
			return "z";
		}
		if (length <= 63)
		{
			return "Z";
		}
		if (length <= 66)
		{
			return "e";
		}
		if (length <= 69)
		{
			return "E";
		}
		if (length <= 72)
		{
			return "p";
		}
		if (length <= 75)
		{
			return "P";
		}
		if (length <= 78)
		{
			return "h";
		}
		if (length <= 81)
		{
			return "H";
		}
		if (length <= 84)
		{
			return "f";
		}
		if (length <= 87)
		{
			return "F";
		}
		if (length <= 90)
		{
			return "v";
		}
		if (length <= 93)
		{
			return "V";
		}
		if (length <= 96)
		{
			return "c";
		}
		if (length <= 99)
		{
			return "C";
		}
		return "X";
	}
}
