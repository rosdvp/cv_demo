using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Global
{
    public static class AdServicer
    {
        static RewardedAd Ad { get; set; }

        public static void Init()
        {
            if (Application.platform != RuntimePlatform.Android)
                return;
            MobileAds.Initialize(initStatus => { });
            Ad = GetNewAdd();
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        static RewardedAd GetNewAdd()
        {
            var adUnitId = Debug.isDebugBuild ?
                "ca-app-pub-3940256099942544/5224354917" : "ca-app-pub-5703854124887851/8350103781";

            var ad = new RewardedAd(adUnitId);
            ad.LoadAd(new AdRequest.Builder().Build());
            return ad;
        }

        public static void ShowAd(System.Action callbackReward, System.Action callbackEnd)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (!Ad.IsLoaded())
                {
                    callbackReward();
                    callbackEnd();
                    return;
                }
                IsShowing = true;
                IsReward = false;
                Consts.CourutinesHolder.StartCoroutine(CrWorkaroundAd(callbackReward, callbackEnd));

                Ad.OnUserEarnedReward += delegate { IsReward = true; };
                Ad.OnAdClosed += delegate { IsShowing = false; };
                Ad.Show();
            }
            else
            {
                callbackReward();
                callbackEnd();
            }
        }

        static bool IsShowing { get; set; }
        static bool IsReward { get; set; }
        static IEnumerator CrWorkaroundAd(System.Action callbackReward, System.Action callbackEnd)
        {
            yield return new WaitWhile(() => IsShowing);
            if (IsReward)
                callbackReward();
            callbackEnd();
            Ad = GetNewAdd();
        }
    }
}