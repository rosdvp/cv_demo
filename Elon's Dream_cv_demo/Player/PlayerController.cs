using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        GameManager _gameManager;
        [SerializeField]
        PlayerHealther _healther;
        [SerializeField]
        PlayerAttacker _attacker;
        [SerializeField]
        PlayerMover _mover;
        [SerializeField]
        PlayerLooker _looker;
        [SerializeField]
        PlayerUpgrader _upgrader;
        [SerializeField]
        Buffs.TargetBuffs _targetBuffs;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public PlayerHealther Healther => _healther;
        public PlayerAttacker Attacker => _attacker;
        public PlayerMover Mover => _mover;
        public PlayerLooker Looker => _looker;
        public Buffs.TargetBuffs TargetBuffs => _targetBuffs;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void Init()
        {
            Healther.Init();
            Attacker.Init();
            _upgrader.Init(this);

            _healther.Base.EvKilled += OnDie;
        }


        public void OnDie()
        {
            gameObject.SetActive(false);
            _gameManager.OnDie();
        }
    }
}
