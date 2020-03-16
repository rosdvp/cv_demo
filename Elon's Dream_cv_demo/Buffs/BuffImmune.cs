using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    [System.Serializable]
    public class BuffImmune : ABuff
    {
        [Header("Params")]
        [Tooltip("Выключать коллизию с другими существами")]
        [SerializeField]
        bool _disableCreaturesCollisions;

        /*---------------------------------------------*/
        /*---------------------------------------------*/


        protected override void OnBuffStart()
        {
            if (_disableCreaturesCollisions)
            {
                Target.TargetEffects._renderer.sortingLayerName = "Effects";
                Target.gameObject.layer = LayerMask.NameToLayer("NotCreatureCollidable");
            }
            Target.HealthParams.ImmuneCounter += 1;
        }

        protected override void OnBuffEnd()
        {
            if (_disableCreaturesCollisions)
            {
                Target.TargetEffects._renderer.sortingLayerName = "Default";
                Target.gameObject.layer = LayerMask.NameToLayer("Creature");
            }
            Target.HealthParams.ImmuneCounter -= 1;
        }

        public override ABuff Clone()
            => new BuffImmune()
            {
                _id = _id,
                _duration = _duration,
                _effectId = _effectId,
                _disableCreaturesCollisions = _disableCreaturesCollisions
            };
    }
}