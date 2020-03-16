using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Effects;
using Char;

namespace Player
{
    public class PlayerHealther : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        Screens.ControlScreen _controlScreen;

        [Header("Components")]
        [SerializeField]
        CharHealtherBase _healther;
        [SerializeField]
        Stuff.SoundEffect _soundDamaged;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public CharHealtherBase Base => _healther;

        public void Init()
        {
            _healther.Health = Global.Modeler.Session.CurrHealth;

            _healther.EvDamaged += delegate
            {
                Global.Modeler.Session.CurrHealth -= 1;
                _healther.Health = Global.Modeler.Session.CurrHealth;

                _controlScreen.HealthUI.SynchWithModel(); 
                _soundDamaged.Play();
            };

            _controlScreen.HealthUI.SynchWithModel();
        }
    }
}
