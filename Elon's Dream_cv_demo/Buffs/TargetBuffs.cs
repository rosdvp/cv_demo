using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Buffs
{
    public class TargetBuffs : MonoBehaviour
    {
        public TargetEffects TargetEffects { get; private set; }
        public IMoveParams MoveParams { get; private set; }
        public IAttackParams AttackParams { get; private set; }
        public IHealthParams HealthParams { get; private set; }

        private void Awake()
        {
            TargetEffects = GetComponentInChildren<TargetEffects>();
            MoveParams = GetComponentInChildren<IMoveParams>();
            AttackParams = GetComponentInChildren<IAttackParams>();
            HealthParams = GetComponentInChildren<IHealthParams>();
        }
    }
}