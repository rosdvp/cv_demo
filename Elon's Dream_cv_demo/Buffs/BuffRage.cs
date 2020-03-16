using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    [System.Serializable]
    public class BuffRage : ABuff
    {
        [SerializeField]
        float _attackIntervalK;

        protected override void OnBuffStart()
            => Target.AttackParams.AttackInterval *= _attackIntervalK;

        protected override void OnBuffEnd()
            => Target.AttackParams.AttackInterval /= _attackIntervalK;

        public override ABuff Clone()
            => new BuffRage()
            {
                _id = _id,
                _duration = _duration,
                _effectId = _effectId,
                _attackIntervalK = _attackIntervalK
            };
    }
}