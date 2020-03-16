using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Global
{
    public class PlayServicer : MonoBehaviour
    {
        private void Awake()
        {
            PlayGamesClientConfiguration config = new
                PlayGamesClientConfiguration.Builder()
                .Build();

            PlayGamesPlatform.DebugLogEnabled = true;

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();

            Auth(delegate 
            {
                ScoreServicer.LoadRank();
                AchievsServicer.LoadAchievsCounts();
            });
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public static bool IsAuth => PlayGamesPlatform.Instance.localUser.authenticated;

        public static void Auth(System.Action callbackOk)
        {
            if (Application.isEditor)
                return;

            if (!IsAuth)
            {
                PlayGamesPlatform.Instance.Authenticate(delegate(bool isOk)
                {
                    if (isOk)
                        callbackOk();
                });
            }
            else
                callbackOk();
        }
    }
}