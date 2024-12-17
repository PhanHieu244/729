using System;
using UnityEngine;

public class FormulHesaplayicisi
{
	public static FormulHesaplayicisi instance = new FormulHesaplayicisi();

	public double _getHizYukseltmeValue()
	{
		int hizSeviyesi = OyunYoneticisi.instance.HizSeviyesi;
		double d;
		if (hizSeviyesi <= 5)
		{
			d = (double)(hizSeviyesi * 10);
		}
		else if (hizSeviyesi <= 46)
		{
			d = 35.0 * Math.Exp((double)hizSeviyesi * 0.098);
		}
		else
		{
			d = 3100.0 * Math.Exp((double)(hizSeviyesi - 46) * 0.1249);
		}
		return Math.Truncate(d);
	}

	public double _getHizDisplayValue()
	{
		int hizSeviyesi = OyunYoneticisi.instance.HizSeviyesi;
		return (double)(hizSeviyesi + 9);
	}

	public double _getPowerUpgradeValue()
	{
		int gucSeviyesi = OyunYoneticisi.instance.GucSeviyesi;
		if (gucSeviyesi == 1)
		{
			return 20.0;
		}
		if (gucSeviyesi == 2)
		{
			return 70.0;
		}
		double d;
		if (gucSeviyesi <= 25)
		{
			d = 71.0 * Math.Exp((double)gucSeviyesi * 0.1628);
		}
		else
		{
			d = 3900.0 * Math.Exp((double)(gucSeviyesi - 25) * 0.3264);
		}
		return Math.Truncate(d);
	}

	public double _getPowerDisplayValue()
	{
		int gucSeviyesi = OyunYoneticisi.instance.GucSeviyesi;
		if (gucSeviyesi == 1)
		{
			return 2.0;
		}
		if (gucSeviyesi == 2)
		{
			return 2.3;
		}
		double num;
		if (gucSeviyesi <= 25)
		{
			num = 1.7281 * Math.Exp((double)gucSeviyesi * 0.1401);
		}
		else
		{
			num = 57.3 * Math.Exp((double)(gucSeviyesi - 25) * 0.1398);
		}
		return Math.Truncate(num * 10.0) / 10.0;
	}

	public double _getGravityUpgradeValue()
	{
		int yercekimiSeviyesi = OyunYoneticisi.instance.YercekimiSeviyesi;
		double d = 0.0;
		if (yercekimiSeviyesi <= 10)
		{
			switch (yercekimiSeviyesi)
			{
			case 1:
				return 10000.0;
			case 2:
				return 10000.0;
			case 3:
				return 10000.0;
			case 4:
				return 10100.0;
			case 5:
				return 10200.0;
			case 6:
				return 10500.0;
			case 7:
				return 10700.0;
			case 8:
				return 11000.0;
			case 9:
				return 11300.0;
			case 10:
				return 11700.0;
			}
		}
		else if (yercekimiSeviyesi <= 20)
		{
			d = 27500.0 * Math.Exp((double)(yercekimiSeviyesi - 10) * 0.1573);
		}
		else
		{
			int num = (yercekimiSeviyesi - 1) / 10;
			double num2 = 1250000.0;
			num2 *= Math.Pow(36.0, (double)(num - 2));
			d = num2 * Math.Exp((double)(yercekimiSeviyesi - num * 10) * 0.1322);
		}
		return Math.Truncate(d);
	}

	public double _getGravityDisplayValue()
	{
		int yercekimiSeviyesi = OyunYoneticisi.instance.YercekimiSeviyesi;
		double num = 9.2493 * Math.Exp((double)yercekimiSeviyesi * 0.0583);
		if (yercekimiSeviyesi == 1)
		{
			return 9.8;
		}
		if (yercekimiSeviyesi == 2)
		{
			return 10.4;
		}
		return Math.Truncate(num * 10.0) / 10.0;
	}

	public double _getToothCountUpgradeValue()
	{
		int disSayisiSeviyesi = OyunYoneticisi.instance.DisSayisiSeviyesi;
		double d = 10000.0 * Math.Pow(3.0, (double)(disSayisiSeviyesi - 1));
		return Math.Truncate(d);
	}

	public double _getToothCountDisplayValue()
	{
		int disSayisiSeviyesi = OyunYoneticisi.instance.DisSayisiSeviyesi;
		return (double)(disSayisiSeviyesi + 3);
	}

	public double _getToothSizeUpgradeValue()
	{
		int disBoyutuSeviyesi = OyunYoneticisi.instance.DisBoyutuSeviyesi;
		double d = 1000.0 * Math.Pow(2.0, (double)(disBoyutuSeviyesi - 1));
		if (disBoyutuSeviyesi == 11)
		{
			d = -1.0;
		}
		return Math.Truncate(d);
	}

	public double _getToothSizeDisplayValue()
	{
		int num = OyunYoneticisi.instance.DisBoyutuSeviyesi;
		num = Math.Min(num, 11);
		return (double)(num + 9);
	}

	public double _getMarketingUpgradeValue()
	{
		int marketingLevel = OyunYoneticisi.instance.MarketingLevel;
		if (marketingLevel == 1)
		{
			return 184000.0;
		}
		if (marketingLevel == 2)
		{
			return 12000000.0;
		}
		return 12000000.0 * Math.Pow(10.0, (double)(marketingLevel - 2));
	}

	public double _getMarketingDisplayValue()
	{
		int marketingLevel = OyunYoneticisi.instance.MarketingLevel;
		return Math.Pow(10.0, (double)(marketingLevel - 1));
	}

	public double _getNumberOfBoxToDestroyForLevelComplete()
	{
		int gameLevel = OyunYoneticisi.instance.GameLevel;
		double d = 60.0 * Math.Exp((double)gameLevel * 0.85);
		if (gameLevel == 1)
		{
			d = 100.0;
		}
		return Math.Truncate(d);
	}

	public double _getLevelCompletePrizeValue()
	{
		int gameLevel = OyunYoneticisi.instance.GameLevel;
		double d = this._getOneDayIdlePrizeForLevel() / 200.0;
		return Math.Truncate(d);
	}

	public double getBonusAmount()
	{
		double num = this._getHizYukseltmeValue();
		double num2 = this._getPowerUpgradeValue();
		double num3 = (num + num2) / 2.0 * (double)UnityEngine.Random.Range(1.5f, 2.5f);
		return num3 * 0.35;
	}

	public double _getOneDayIdlePrizeForLevel()
	{
		int gameLevel = OyunYoneticisi.instance.GameLevel;
		double num = 100788.00000000001 * Math.Pow((double)gameLevel, 1.784);
		int num2 = gameLevel / 10;
		num *= Math.Pow(10.0, (double)num2);
		return Math.Truncate(num);
	}
}
