using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Datas.Events
{
    [CreateAssetMenu(menuName = "Datas/Event")]
    public class DataEvent : ScriptableObject
    {
        public Logic.Results.EffectDeltas deltas;
        public Logic.Results.EffectCausings causings;


        public string String => Global.Stringer.GetEventString(name);
    }
}