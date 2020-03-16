using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Datas.Events;

namespace Datas
{
    public class Eventer : MonoBehaviour
    {
        [Header("Assets")]
        [SerializeField]
        List<DataEvent> _events;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static List<DataEvent> Events { get; set; }

        private void Awake() => Events = _events;
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static DataEvent GetEvent(string name)
            => Events.Find(e => e.name == name);

        public static List<DataEvent> GetWeeklyEvents()
            => Events.FindAll(e => e.name.StartsWith($"WEEKLY_{Global.Modeler.Week.weekNumber}"));
    }
}
