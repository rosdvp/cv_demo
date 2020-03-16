using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.Tasks
{
    [CreateAssetMenu(menuName = "Datas/Task")]
    public class DataTask : ScriptableObject
    {
        public int successesCount = 1;
        public bool isReversed;

        public bool withRating;
        public NewsRating minRating;

        public bool withType;
        public NewsType newsType;

        public bool withIdea;
        public Idea idea;

        public bool withInterests;
        public List<int> minInterests;


        public string String => Global.Stringer.GetTaskString(name);
        public int Money => Global.Modeler.Week.moneySchedule.GetValue(name);
    }
}