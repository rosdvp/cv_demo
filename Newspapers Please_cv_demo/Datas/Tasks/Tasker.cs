using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas.Tasks;
using System.Linq;

namespace Datas
{
    public class Tasker : MonoBehaviour
    {
        [Header("Assets")]
        [SerializeField]
        List<DataTask> _tasks;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static List<DataTask> Tasks { get; set; }

        private void Awake() => Tasks = _tasks;
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static DataTask GetTask(string name)
            => Tasks.Find(t => t.name == name);

        public static string GetSimpleTaskName()
        {
            var week = Global.Modeler.Week.weekNumber;
            string diff;
            if (week < Consts.Balance.TASK_MEDIUM_START_WEEK)
                diff = "EASY";
            else if (week < Consts.Balance.TASK_HARD_START_WEEK)
                diff = "MEDIUM";
            else
                diff = "HARD";
            return Tasks.Where(t => t.name.StartsWith(diff)).GetRand().name;
        }
    }
}