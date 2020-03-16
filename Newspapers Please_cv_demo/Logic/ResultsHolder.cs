using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Results;

namespace Logic
{
    public static class ResultsHolder
    {
        public static List<Result> TasksResults { get; private set; } = new List<Result>();
        public static Result GetTaskResult(string taskId)
            => TasksResults.Find(t => t.name == taskId);


        public static List<ResultNews> NewsResults { get; private set; } = new List<ResultNews>();
        public static ResultNews GetNewsResult(string newsId)
            => NewsResults.Find(n => n.name == newsId);

        
        public static int HomeGuessedCount { get; set; }
        public static int HomeTotalCount { get; set; }
    }


    namespace Results
    {
        public class Result
        {
            public string name;
            public bool isDone;
            public EffectDeltas deltas;
            public EffectCausings causing;
        }

        public class ResultNews : Result
        {
            public NewsType type;

            public int[] parameters = new int[System.Enum.GetValues(typeof(NewsParamType)).Length];
            public int[] interests = new int[System.Enum.GetValues(typeof(AuditoryType)).Length];

            public Idea idea;
            public NewsRating rating;

            public int rewriteCount;
        }

        [System.Serializable]
        public class EffectDeltas
        {
            public int moneyDelta;
            public int repDelta;
            public int[] moodsDeltas = new int[System.Enum.GetValues(typeof(AuditoryType)).Length];
        }

        [System.Serializable]
        public class EffectCausings
        {
            public Datas.Tasks.DataTask task;
        }
    }
}
