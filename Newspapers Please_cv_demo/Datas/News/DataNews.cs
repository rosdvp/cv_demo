using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.News
{
    [CreateAssetMenu(menuName = "Datas/News")]
    public class DataNews : ScriptableObject
    {
        public NewsType type;


        public string String => Global.Stringer.GetNewsString(name);
        public string TypeString => Global.Stringer.GetNewsTypeString(type);
        public int Money => Global.Modeler.Week.moneySchedule.GetValue(name);
    }
}
