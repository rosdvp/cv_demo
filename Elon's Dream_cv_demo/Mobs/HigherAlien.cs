﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobs.Components;
using CharAnimator = Char.CharAnimator;

namespace Mobs
{
    public class HigherAlien : MonoBehaviour
    {
        [Header("Params")]
        [Tooltip("Расстояние отхода между ударами (от центра игрока до центра моба)")]
        [SerializeField]
        float _retreatDistance;
        [Tooltip("Коэфицент скорости перемещения при отходе")]
        [SerializeField]
        float _retreatMoveSpeedK;

        [Header("Components")]
        [SerializeField]
        MobPlayerDetector _detector;
        [SerializeField]
        MobMover _mover;
        [SerializeField]
        MobAttackerMelee _attacker;
        [SerializeField]
        MobHealther _healther;
        [SerializeField]
        CharAnimator _charAnim;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Start()
        {
            _mover.EvMoveStart += (targetPos) =>
            {
                if (_detector.IsPlayerDetected)
                    _charAnim.SetLookAt(_detector.PlayerPos);
                else
                    _charAnim.SetLookAt(targetPos);
                _charAnim.SetLegsMove();
            };
            _mover.EvMoveEnd += _charAnim.SetLegsIdle;

            _attacker.Base.EvPrepareStart += () =>
            {
                _charAnim.SetLookAt(_detector.PlayerPos);
                _charAnim.SetArmsPrepare();
            };
            _attacker.Base.EvAttackStart += _charAnim.SetArmsIdle;

            _healther.Base.EvKilled += () => Destroy(gameObject);
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        bool IsReatreating { get; set; }

        private void FixedUpdate()
        {
            if (_healther.Base.IsStunned)
                return;

            _attacker.Base.OnUpdate();
            _mover.OnUpdate();

            if (_attacker.Base.State == EState.Ready && _attacker.IsPossible)
            {
                _attacker.Base.Begin();
                _mover.ForceStop();
            }

            if (_mover.State == EState.Ready)
            {
                if (!_detector.IsPlayerDetected)
                    _mover.Begin(Stuff.PathUtil.GetNextPosToRandom(transform.position));
                else
                if (_attacker.Base.State == EState.Ready)
                {
                    _mover.Begin(Stuff.PathUtil.GetNextPosToTarget(transform.position, _detector.PlayerPos));
                    if (IsReatreating)
                    {
                        IsReatreating = false;
                        _mover.MoveSpeed /= _retreatMoveSpeedK;
                    }
                }
                else
                if (_attacker.Base.State == EState.Reloading && _detector.DistToPlayer <= _retreatDistance)
                {
                    _mover.Begin(Stuff.PathUtil.GetNextPosToOpposite(transform.position, _detector.PlayerPos));
                    if (!IsReatreating)
                    {
                        IsReatreating = true;
                        _mover.MoveSpeed *= _retreatMoveSpeedK;
                    }
                }
            }
        }
    }
}