using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobs.Components;
using CharAnimator = Char.CharAnimator;
using TargetBuffs = Buffs.TargetBuffs;
using Buffer = Buffs.Buffer;

namespace Mobs
{
    public class Shooter : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField]
        float _retreatDistance;

        [Header("Components")]
        [SerializeField]
        MobPlayerDetector _detector;
        [SerializeField]
        MobMover _mover;
        [SerializeField]
        MobAttackerRange _attacker;
        [SerializeField]
        MobHealther _healther;
        [SerializeField]
        CharAnimator _charAnim;
        [SerializeField]
        TargetBuffs _targetBuffs;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Start()
        {
            _mover.EvMoveStart += (targetPos) =>
            {
                if (!_detector.IsPlayerDetected)
                    _charAnim.SetLookAt(targetPos);
                _charAnim.SetLegsMove();
            };
            _mover.EvMoveEnd += _charAnim.SetLegsIdle;

            _attacker.Base.EvPrepareStart += _charAnim.SetArmsPrepare;
            _attacker.Base.EvPrepareDo += () => _charAnim.SetLookAt(_detector.PlayerPos);
            _attacker.Base.EvAttackStart += _charAnim.SetArmsAttack;
            _attacker.Base.EvAttackEnd += _charAnim.SetArmsIdle;

            _healther.Base.EvKilled += () => Destroy(gameObject);
        }


        /*---------------------------------------------*/
        /*---------------------------------------------*/
        bool IsRetreating { get; set; }

        private void FixedUpdate()
        {
            if (_healther.Base.IsStunned)
                return;

            _mover.OnUpdate();
            _attacker.Base.OnUpdate();

            if (!IsRetreating && _detector.IsPlayerDetected && _detector.DistToPlayer <= _retreatDistance)
            {
                IsRetreating = true;
                Buffer.SetBuff(_targetBuffs, BuffId.ShooterRetreat, true);
            }
            else if (IsRetreating && _detector.DistToPlayer > _retreatDistance)
            {
                IsRetreating = false;
                Buffer.SetBuff(_targetBuffs, BuffId.ShooterRetreat, false);
            }

            if (_attacker.Base.State == EState.Ready && _attacker.IsPossible)
            {
                _attacker.Base.Begin();
                if (!IsRetreating)
                    _mover.ForceStop();
            }

            if (_mover.State == EState.Ready)
            {
                if (IsRetreating)
                    _mover.Begin(Stuff.PathUtil.GetNextPosToOpposite(transform.position, _detector.PlayerPos));
                else 
                if (_attacker.Base.State == EState.Ready)
                {
                    if (_detector.IsPlayerDetected)
                        _mover.Begin(Stuff.PathUtil.GetNextPosToTarget(transform.position, _detector.PlayerPos));
                    else
                        _mover.Begin(Stuff.PathUtil.GetNextPosToRandom(transform.position));
                }
            }
        }
    }
}
