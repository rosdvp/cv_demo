using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Char;

namespace Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        PlayerLooker _looker;
        [SerializeField]
        Screens.ControlScreen _controlScreen;

        [Header("Components")]
        [SerializeField]
        CharAttackerBase _attacker;
        [SerializeField]
        CharDamagerHook _damager;
        [SerializeField]
        Stuff.SoundEffect _attackSound;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public CharAttackerBase Base => _attacker;
        public CharDamagerHook Damager => _damager;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void Init()
        {
            _controlScreen.AttackUI.Init(delegate 
            {
                if (_attacker.State == EState.Ready)
                    _attacker.Begin();
            }, _attacker.AttackIntervalTimer);

            _attacker.EvAttackStart += delegate
            {
                _attackSound.Play();
                _damager.TurnTo(transform.position, _looker.CurrDirection);
                _damager.Enable();
            };
            _attacker.EvAttackEnd += _damager.Disable;
        }

        public void Update()
        {
            _attacker.OnUpdate();
        }
    }
}