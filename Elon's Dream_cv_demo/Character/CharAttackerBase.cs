using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Char
{
    public class CharAttackerBase : MonoBehaviour, Buffs.IAttackParams
    {
        [Tooltip("Базовое время на подготовку удара в секундах")]
        [SerializeField]
        float _prepareDelayBase;
        [Tooltip("Базовый интервал между атаками в секундах")]
        [SerializeField]
        float _attackIntervalBase;
        [Tooltip("Базовая длительность атаки")]
        [SerializeField]
        float _attackDurationBase;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public System.Action EvPrepareStart { get; set; } = delegate { };
        public System.Action EvPrepareDo { get; set; } = delegate { };
        public System.Action EvAttackStart { get; set; } = delegate { };
        public System.Action EvAttackDo { get; set; } = delegate { };
        public System.Action EvAttackEnd { get; set; } = delegate { };

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public Stuff.Timer AttackIntervalTimer { get; private set; } = new Stuff.Timer();
        public float AttackInterval
        {
            get => _attackIntervalBase;
            set
            {
                _attackIntervalBase = value;
                AttackIntervalTimer.Change(_attackIntervalBase);
            }
        }
        //----------------
        Stuff.Timer PrepareTimer { get; set; } = new Stuff.Timer();
        public float PrepareDelay
        {
            get => _prepareDelayBase;
            set
            {
                _prepareDelayBase = value;
                PrepareTimer.Change(_prepareDelayBase);
            }
        }
        //----------------
        Stuff.Timer AttackTimer { get; set; } = new Stuff.Timer();
        public float AttackDuration { get => _attackDurationBase; set => _attackDurationBase = value; }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public EState State { get; private set; }

        public void Begin()
        {
            State = EState.Preparing;

            PrepareTimer.Set(PrepareDelay);
            EvPrepareStart();
        }

        public void OnUpdate()
        {
            switch (State)
            {
                case EState.Reloading:
                    if (AttackIntervalTimer.IsDone)
                        State = EState.Ready;
                    break;

                case EState.Ready:
                    break;

                case EState.Preparing:
                    EvPrepareDo();
                    if (PrepareTimer.IsDone)
                        State = EState.Prepared;
                    break;

                case EState.Prepared:
                    AttackTimer.Set(AttackDuration);
                    EvAttackStart();
                    State = EState.Doing;
                    break;

                case EState.Doing:
                    EvAttackDo();
                    if (AttackTimer.IsDone)
                        State = EState.Done;
                    break;

                case EState.Done:
                    EvAttackEnd();
                    AttackIntervalTimer.Set(AttackInterval);
                    State = EState.Reloading;
                    break;
            }
        }
    }
}