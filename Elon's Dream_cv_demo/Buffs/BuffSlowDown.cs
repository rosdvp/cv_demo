using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    [System.Serializable]
    public class BuffSlowDown : ABuff
    {
        [SerializeField]
        float _moveSpeedK = 1;
        [SerializeField]
        float _prepareDurationK = 1;
        [SerializeField]
        float _attackIntervalK = 1;


        protected override void OnBuffStart()
        {
            Target.MoveParams.MoveSpeed *= _moveSpeedK;
            Target.AttackParams.AttackInterval *= _attackIntervalK;
            Target.AttackParams.PrepareDelay *= _prepareDurationK;
        }

        protected override void OnBuffEnd()
        {
            Target.MoveParams.MoveSpeed /= _moveSpeedK;
            Target.AttackParams.AttackInterval /= _attackIntervalK;
            Target.AttackParams.PrepareDelay /= _prepareDurationK;
        }

        public override ABuff Clone()
            => new BuffSlowDown()
            {
                _id = _id,
                _duration = _duration,
                _effectId = _effectId,
                _moveSpeedK = _moveSpeedK,
                _attackIntervalK = _attackIntervalK,
                _prepareDurationK = _prepareDurationK
            };
    }
}