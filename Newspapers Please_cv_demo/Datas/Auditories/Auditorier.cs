using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Datas.Auditories;

namespace Datas
{
    public class Auditorier : MonoBehaviour
    {
        [Header("Assets")]
        [SerializeField]
        List<DataAuditory> _auditories;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static List<DataAuditory> Auditories { get; set; }

        private void Awake() => Auditories = _auditories;
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static void GenerateAuditoriesPrefers()
        {
            foreach (var newsParam in System.Enum.GetValues(typeof(NewsParamType)))
            {
                var distribution = GetNewsParamDistribution();

                for (var aud = 0; aud < distribution.Length; aud++)
                {
                    Auditories.Find(a => a.Type == (AuditoryType)aud)
                        .opinionsNewsParams[(int)newsParam] = distribution[aud];
                }
            }
        }

        static int[] GetNewsParamDistribution()
        {
            var distribution = new int[System.Enum.GetValues(typeof(AuditoryType)).Length];

            var line = new int[Consts.Params.NEWS_PARAM_MAX];

            foreach (var aud in System.Enum.GetValues(typeof(AuditoryType)))
            {
                var freePointsCount = line.Count(p => p == 0);
                var avgProb = 1f / freePointsCount;
                var closeProb = avgProb * Consts.Balance.EDIT_PARAMS_DISPERSIA;
                var farProb = avgProb / Consts.Balance.EDIT_PARAMS_DISPERSIA;
                var closeCount = Mathf.RoundToInt(closeProb * 100);
                var farCount = Mathf.RoundToInt(farProb * 100);

                var indexes = new List<int>();
                for (var j = 0; j < line.Length; j++)
                    if (line[j] == 0)
                    {
                        if ((j - 1 < 0 || line[j - 1] == 0) && (j + 1 >= line.Length || line[j + 1] == 0))
                            indexes.AddRange(Enumerable.Repeat(j, farCount));
                        else
                            indexes.AddRange(Enumerable.Repeat(j, closeCount));
                    }
                var index = indexes[Random.Range(0, indexes.Count)];
                line[index] = 1;
                distribution[(int)aud] = index;
            }

            return distribution;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static int GetOpinionToNewsParam(AuditoryType auditory, NewsParamType param, int value)
        {
            var preferValue = Auditories.Find(a => a.Type == auditory)
                .opinionsNewsParams[(int)param];
            var opinion = preferValue - value;
            if (opinion < Consts.Params.OPINION_MIN)
                opinion = Consts.Params.OPINION_MIN;
            else if (opinion > Consts.Params.OPINION_MAX)
                opinion = Consts.Params.OPINION_MAX;
            return opinion;
        }

        public static int GetOpinionToNewsType(AuditoryType auditory, NewsType newsType)
            => Auditories.Find(a => a.Type == auditory)
                .opinionsNewsTypes[(int)newsType];
    }
}