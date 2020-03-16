using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Screens;
using WeekOverRank = Screens.WeekOver.WeekOverRank;
using ResultsHolder = Logic.ResultsHolder;

namespace Global
{
    public class ScoreServicer : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        WeekOverRank _weekOverRank;
        [SerializeField]
        EndScreen _endScreen;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Awake()
        {
            _weekOverRank.EvRankUpdateStart += UpdateScoreByWeekEnd;
            _endScreen.EvGameEnd += UpdateScoreByGameEnd;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static void ShowLeaderboard()
            => PlayServicer.Auth(PlayGamesPlatform.Instance.ShowLeaderboardUI);

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public static bool IsRankReady => Rank != -1;
        public static int Rank { get; private set; } = -1;

        public static void LoadRank()
        {
            Rank = -1;

            PlayGamesPlatform.Instance.LoadScores(
             GPGSIds.leaderboard_top_operators,
             LeaderboardStart.PlayerCentered,
             1,
             LeaderboardCollection.Public,
             LeaderboardTimeSpan.AllTime,
             delegate (LeaderboardScoreData data)
             {
                 if (data.Status == ResponseStatus.Success && data.PlayerScore != null && data.PlayerScore.rank != 0)
                     Rank = data.PlayerScore.rank;
             });
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        void UpdateScoreByWeekEnd()
        {
            CalculateNewScore();

            if (PlayServicer.IsAuth)
            {
                Rank = -1;
                PlayGamesPlatform.Instance.ReportScore(Modeler.Game.score, GPGSIds.leaderboard_top_operators,
                    delegate (bool isOk)
                    {
                        if (isOk)
                            LoadRank();
                    });
            }
        }

        void UpdateScoreByGameEnd(EndType end)
        {
            if (end != EndType.PROTEST_OPP)
                return;

            var avgScorePerWeek = Mathf.RoundToInt((float)Modeler.Game.score / Modeler.Week.weekNumber);
            var weeksLeft = Consts.Balance.END_WEEK - Modeler.Week.weekNumber;
            Modeler.Game.score += weeksLeft * avgScorePerWeek;

            if (PlayServicer.IsAuth)
            {
                Rank = -1;
                PlayGamesPlatform.Instance.ReportScore(Modeler.Game.score, GPGSIds.leaderboard_top_operators,
                    delegate (bool isOk)
                    {
                        if (isOk)
                            LoadRank();
                    });
            }
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        static void CalculateNewScore()
        {
            var score = 0;

            foreach (var newsResult in ResultsHolder.NewsResults)
                if (newsResult.isDone)
                    score += (int)newsResult.rating * Consts.Services.SCORE_PER_NEWS_RATING;

            foreach (var taskResult in ResultsHolder.TasksResults)
                if (taskResult.isDone)
                    score += Consts.Services.SCORE_PER_TASK_DONE;

            score += ResultsHolder.HomeGuessedCount * Consts.Services.SCORE_PER_HOME_GUESSED;

            Modeler.Game.score += score;
        }
    }
}
