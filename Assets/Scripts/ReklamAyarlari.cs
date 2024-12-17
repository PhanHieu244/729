using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ReklamAyarlari : MonoBehaviour
{
	[HideInInspector]
	public static ReklamAyarlari instance;

	private int startGameDelayTime = 45;

	private int ad2adDelayTime = 45;

	private bool timeCheck;

	private Action adSuccess;

	private static Action __f__am_cache1;

	private static Action __f__am_cache2;

	private void Awake()
	{
		ReklamAyarlari.instance = this;
	}

	private void Start()
	{
       
        //if(AdsControl.Instance.GetRewardAvailable())
       // {
            UIManager.instance.x3Button.VideoAvailable();
            UIManager.instance.infoPanel.SetVideoAvailable(true);
            UIManager.instance.x2Button.VideoAvailable();
       // }



    }


	public void ShowIncentivizedAdForLevelUpDouble()
	{

        /*AdsControl.Instance.PlayDelegateRewardVideo(delegate
        {
            UIManager.instance.CollectX2LevelUp();
        });
        */

    }

    public void _ShowIncentivizedAdForDoubleCollectOfPrize(Action result)
	{
       
	}

	public void ShowIncentivizedAdForX3Speed()
	{

        /*AdsControl.Instance.PlayDelegateRewardVideo(delegate
        {
            UIManager.instance.ActivateX3Speed();
        });*/
    }

	public void ShowIncentivizedAdForX2Speed()
	{

        /*AdsControl.Instance.PlayDelegateRewardVideo(delegate
        {
            UIManager.instance.ActivateX2Speed();
        });*/
    }

}
