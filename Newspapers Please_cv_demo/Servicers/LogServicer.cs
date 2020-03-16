using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;
using Firebase.Crashlytics;
using Firebase.Analytics;

using Screens;

namespace Global
{
    public class LogServicer : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        ErrorCatcher _errorCatcher;
        [SerializeField]
        WaitScreen _waitScreen;
        [SerializeField]
        Screens.Wait.WaitNotif _waitNotif;
        [SerializeField]
        List<Screens.Duty.TaskItem> _tasks;
        [SerializeField]
        PublishScreen _publishScreen;
        [SerializeField]
        HomeScreen _homeScreen;
        [SerializeField]
        WeekOverScreen _weekOverScreen;
        [SerializeField]
        EndScreen _endScreen;
        [SerializeField]
        ReviewPopup _reviewPopup;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        private bool IsFirebaseInited { get; set; }

        private void Awake()
        {
            if (Application.platform != RuntimePlatform.Android)
                return;
            
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == Firebase.DependencyStatus.Available)
                {
                    Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                    IsFirebaseInited = true;
                }
            });

            _errorCatcher.EvExceptionCaught += LogModelToCrashlystic;

            _waitScreen.EvSkipByAd += () => LogEventAd("wait");
            _waitNotif.EvNotifChanged += LogEventNotifChanged;

            _tasks.ForEach(t => t.EvTaskEnd += LogEventTaskEnd);

            _publishScreen.EvPublished += LogEventNewsDone;
            _publishScreen.EvRewriteByAd += () => LogEventAd("rewrite");

            _homeScreen.EvCheckEnd += LogEventHomeEnd;
            _homeScreen.EvAgainByAd += () => LogEventAd("home");

            _weekOverScreen.EvWeekOver += LogEventWeekEnd;

            _endScreen.EvGameEnd += LogEventGameEnd;

            _reviewPopup.EvReviewPosted += LogEventReviewPosted;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void LogModelToCrashlystic()
        {
            Debug.Log("Model sent to Crashlystic");

            if (!IsFirebaseInited)
                return;
            Crashlytics.Log("Week: " + JsonUtility.ToJson(Modeler.Week));
            Crashlytics.Log("Game: " + JsonUtility.ToJson(Modeler.Game));
        }
        
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        const string PARAM_WEEK = "week";
        const string PARAM_REP = "rep";
        const string PARAM_PROTEST = "protest";
        const string PARAM_NEWS_RATING = "news_rating";
        const string PARAM_NEWS_IDEA = "news_idea";
        const string PARAM_NEWS_REWRITE = "news_rewrite";
        const string PARAM_TASK_DONE = "task_done";
        const string PARAM_HOME_UNGUESSED = "home_unguessed";
        const string PARAM_AD_MOMENT = "ad_moment";
        const string PARAM_END = "end";
        const string PARAM_NOTIF_ENABLED = "notif_enabled";

        private void LogEventWeekEnd()
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("week_end", new Parameter[]
                {
                    new Parameter(PARAM_WEEK, Modeler.Week.weekNumber.ToString()),
                    new Parameter(PARAM_REP, Modeler.Week.Rep.ToString()),
                    new Parameter(PARAM_PROTEST, Modeler.Week.Protest.ToString())
                });
            Debug.Log($"EVENT: week_end\n" +
                $"{PARAM_WEEK}: {Modeler.Week.weekNumber}\n{PARAM_REP}: {Modeler.Week.Rep}\n" +
                $"{PARAM_PROTEST}: {Modeler.Week.Protest}");
        }

        private void LogEventGameEnd(EndType end)
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("game_end", new Parameter[]
                {
                    new Parameter(PARAM_END, end.ToString()),
                    new Parameter(PARAM_WEEK, Modeler.Week.weekNumber.ToString())
                });
            Debug.Log($"EVENT: game_end\n" +
                $"{PARAM_END}: {end}  {PARAM_WEEK}: {Modeler.Week.weekNumber}");
        }

        private void LogEventNewsDone(Logic.Results.ResultNews newsResult)
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("news_published",
                new Parameter[]
                {
                    new Parameter(PARAM_NEWS_RATING, newsResult.rating.ToString()),
                    new Parameter(PARAM_NEWS_IDEA, newsResult.idea.ToString()),
                    new Parameter(PARAM_NEWS_REWRITE, newsResult.rewriteCount.ToString())
                });
            Debug.Log($"EVENT: news_published\n" +
                $"{PARAM_NEWS_RATING}: {newsResult.rating}\n" +
                $"{PARAM_NEWS_IDEA}: {newsResult.idea}\n" +
                $"{PARAM_NEWS_REWRITE}: {newsResult.rewriteCount}\n");
        }

        private void LogEventTaskEnd(bool isDone)
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("task_end", PARAM_TASK_DONE, isDone ? "DONE" : "FAILED");
            Debug.Log($"EVENT: task_end\n{PARAM_TASK_DONE}: {(isDone ? "DONE" : "FAILED")}");
        }

        private void LogEventHomeEnd(int guessedCount, int totalCount)
        {
            var unguessedCount = totalCount - guessedCount;

            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("home_end", PARAM_HOME_UNGUESSED, unguessedCount.ToString());
            Debug.Log($"EVENT: home_end\n{PARAM_HOME_UNGUESSED}: {unguessedCount}");
        }

        private void LogEventAd(string moment)
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("ad_watched", PARAM_AD_MOMENT, moment);
            Debug.Log($"EVENT: ad_watched\n{PARAM_AD_MOMENT}: {moment}");
        }

        private void LogEventNotifChanged(bool isOn)
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("notif", PARAM_NOTIF_ENABLED, isOn ? "ON" : "OFF");
            Debug.Log($"EVENT: notif\n{PARAM_NOTIF_ENABLED}: {(isOn ? "ON" : "OFF")}");
        }

        private void LogEventReviewPosted()
        {
            if (IsFirebaseInited)
                FirebaseAnalytics.LogEvent("review_posted");
            Debug.Log("EVENT: review_posted");
        }
    }
}
