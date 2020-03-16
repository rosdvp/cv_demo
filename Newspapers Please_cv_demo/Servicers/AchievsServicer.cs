using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using Logic;
using Logic.Results;
using Screens;

namespace Global
{
    public class AchievsServicer : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        WeekStartScreen _weekStartScreen;
        [SerializeField]
        DutyScreen _dutyScreen;
        [SerializeField]
        PublishScreen _publishScreen;
        [SerializeField]
        EndScreen _endScreen;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static void ShowAchievements()
            => PlayServicer.Auth(PlayGamesPlatform.Instance.ShowAchievementsUI);

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public static bool IsAchievsReady => TotalCount != 0;
        public static int UnlockedCount { get; private set; }
        public static int TotalCount { get; private set; }

        static Dictionary<string, bool> Map { get; set; } = new Dictionary<string, bool>();
        static bool IsLoading { get; set; }

        public static void LoadAchievsCounts()
        {
            if (!PlayServicer.IsAuth || IsLoading)
                return;

            UnlockedCount = 0;
            TotalCount = 0;
            IsLoading = true;

            PlayGamesPlatform.Instance.LoadAchievements(achievements =>
            {
                Map = new Dictionary<string, bool>();
                foreach (var achiev in achievements)
                {
                    TotalCount += 1;
                    if (achiev.completed)
                        UnlockedCount += 1;

                    Map.Add(achiev.id, achiev.completed);
                }
                SetAchievsTriggers();
                IsLoading = false;

                PlayGamesPlatform.Instance.SetGravityForPopups(GooglePlayGames.BasicApi.Gravity.TOP);
            });
            PlayGamesPlatform.Instance.LoadAchievementDescriptions(delegate { });
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static bool WasTriggersSet { get; set; }

        static void SetAchievsTriggers()
        {
            if (WasTriggersSet)
                return;
            WasTriggersSet = true;

            RepBalancer.EvExtremeRep += () => CheckAchiev(GPGSIds.achievement_wellwishers, WellWishers);

            Instance._weekStartScreen.EvWeekStart += () =>
            {
                CheckAchiev(GPGSIds.achievement_red_may, RedMay);
                CheckAchiev(GPGSIds.achievement_proletarian_unite, ProletarianUnite);
                CheckAchiev(GPGSIds.achievement_fathers_and_sons, FathersAndSons);
                CheckAchiev(GPGSIds.achievement_bourgeois_revolution, BourgeoisRevolution);
            };

            Instance._dutyScreen.EvDutyEnd += () => CheckAchiev(GPGSIds.achievement_twofaced, TwoFaced);

            Instance._publishScreen.EvPublished += (newsResult) =>
            {
                CheckAchiev(GPGSIds.achievement_but_this_is_a_good_stability, () => ButThisIsAGoodStability(newsResult));
                CheckAchiev(GPGSIds.achievement_evening_as, () => EveningAS(newsResult));
                CheckAchiev(GPGSIds.achievement_foreign_agent, () => ForeignAgent(newsResult));
                CheckAchiev(GPGSIds.achievement_voice_of_storm, () => VoiceOfStorm(newsResult));
                CheckAchiev(GPGSIds.achievement_not_my_business, () => NotMyBusiness(newsResult));
                CheckAchiev(GPGSIds.achievement_newsmaker, () => Newsmaker());
                CheckAchiev(GPGSIds.achievement_maestro, () => Maestro());
            };

            Instance._endScreen.EvGameEnd += (end) =>
            {
                CheckAchiev(GPGSIds.achievement_to_the_barricades, () => ToTheBarricades(end));
                CheckAchiev(GPGSIds.achievement_kingmaker, () => Kingmaker(end));
                CheckAchiev(GPGSIds.achievement_public_enemy, () => PublicEnemy(end));
                CheckAchiev(GPGSIds.achievement_for_the_glory_of_government, () => ForTheGloryOfGovernment(end));
                CheckAchiev(GPGSIds.achievement_conformism, () => Conformism(end));
                CheckAchiev(GPGSIds.achievement_underground_leftist, () => UndergroundLeftist(end));
                CheckAchiev(GPGSIds.achievement_victim_of_regime, () => VictimOfRegime(end));
                CheckAchiev(GPGSIds.achievement_lynch_mob, () => LynchMob(end));
            };
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static void CheckAchiev(string achievId, System.Func<AchievAction> callbackAchiev)
        {
            if (!PlayServicer.IsAuth || !IsAchievsReady || Map[achievId])
                return;
            var action = callbackAchiev();
            if (action == AchievAction.DONE)
                PlayGamesPlatform.Instance.UnlockAchievement(achievId);
            if (action == AchievAction.INC)
                PlayGamesPlatform.Instance.IncrementAchievement(achievId, 1, delegate { });
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/


        static AchievAction ButThisIsAGoodStability(ResultNews newsResult)
            => newsResult.idea == Idea.GOV ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction EveningAS(ResultNews newsResult)
            => newsResult.idea == Idea.GOV ? AchievAction.INC : AchievAction.NONE;

        static AchievAction ForeignAgent(ResultNews newsResult)
            => newsResult.idea == Idea.OPP ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction VoiceOfStorm(ResultNews newsResult)
            => newsResult.idea == Idea.OPP ? AchievAction.INC : AchievAction.NONE;

        static AchievAction NotMyBusiness(ResultNews newsResult)
            => newsResult.idea == Idea.NEU ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction TwoFaced()
            => ResultsHolder.NewsResults.Count == 2 &&
            ResultsHolder.NewsResults[0].isDone && ResultsHolder.NewsResults[1].isDone &&
            ResultsHolder.NewsResults[0].idea != Idea.NEU && ResultsHolder.NewsResults[1].idea != Idea.NEU &&
            ResultsHolder.NewsResults[0].idea != ResultsHolder.NewsResults[1].idea ?
            AchievAction.DONE : AchievAction.NONE;


        static AchievAction Newsmaker()
            => AchievAction.INC;

        static AchievAction Maestro()
            => AchievAction.INC;



        static AchievAction RedMay()
            => Modeler.Week.moods.Get(0) == Consts.Params.MOOD_MAX ? 
            AchievAction.DONE : AchievAction.NONE;

        static AchievAction ProletarianUnite()
            => Modeler.Week.moods.Get(1) == Consts.Params.MOOD_MAX ?
            AchievAction.DONE : AchievAction.NONE;

        static AchievAction FathersAndSons()
            => Modeler.Week.moods.Get(2) == Consts.Params.MOOD_MAX ?
            AchievAction.DONE : AchievAction.NONE;

        static AchievAction BourgeoisRevolution()
            => Modeler.Week.moods.Get(3) == Consts.Params.MOOD_MAX ?
            AchievAction.DONE : AchievAction.NONE;



        static AchievAction ToTheBarricades(EndType end)
            => end == EndType.PROTEST_OPP ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction Kingmaker(EndType end)
            => end == EndType.PROTEST_NEUTRAL ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction PublicEnemy(EndType end)
            => end == EndType.PROTEST_GOV ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction ForTheGloryOfGovernment(EndType end)
            => end == EndType.WEEKS_GOV ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction Conformism(EndType end)
            => end == EndType.WEEKS_NEUTRAL ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction UndergroundLeftist(EndType end)
            => end == EndType.WEEKS_OPP ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction WellWishers()
            => Modeler.Week.Rep == Consts.Params.REP_MAX || Modeler.Week.Rep == 0 ?
            AchievAction.DONE : AchievAction.NONE;

        static AchievAction VictimOfRegime(EndType end)
            => end == EndType.EXTREME_OPP ? AchievAction.DONE : AchievAction.NONE;

        static AchievAction LynchMob(EndType end)
            => end == EndType.EXTREME_GOV ? AchievAction.DONE : AchievAction.NONE;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static AchievsServicer Instance { get; set; }

        private void Awake()
            => Instance = this;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        enum AchievAction
        {
            NONE = 0,
            INC = 1,
            DONE = 2
        }
    }
}