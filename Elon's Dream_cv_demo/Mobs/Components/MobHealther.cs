using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Char;

namespace Mobs.Components
{
    public class MobHealther : MonoBehaviour
    {
        [Header("Params")]
        [Tooltip("Сколько опыта получит игрок за убийство")]
        [SerializeField]
        int _expCost;

        [Header("Components")]
        [SerializeField]
        CharHealtherBase _healther;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public CharHealtherBase Base => _healther;


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Awake()
        {
            Base.EvKilled += delegate
            {
                Global.Modeler.Session.Exp += _expCost;
                Global.Modeler.Session.Kills += 1;
            };
        }
    }
}