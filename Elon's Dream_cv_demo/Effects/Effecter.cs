using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Effecter : MonoBehaviour
    {
        [Header("Assets")]
        [SerializeField]
        GameObject _effectDeathPref;
        [SerializeField]
        EffectSpeedUp _effectSpeedUp;
        [SerializeField]
        EffectRage _effectRage;
        [SerializeField]
        EffectColor _effectSlow;
        [SerializeField]
        EffectColor _effectDamaged;
        [SerializeField]
        EffectImmune _effectImmune;
        [SerializeField]
        EffectSpikes _effectSpikes;
        [SerializeField]
        EffectBlockMissiles _effectBlockMissiles;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static GameObject EffectDeathPref { get; set; }
        public static GameObject GetEffectDeathInstance()
            => Instantiate(EffectDeathPref);

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        static Dictionary<EffectId, AEffect> Effects { get; set; }
        static Dictionary<TargetEffects, List<AEffect>> EffectsMap { get; set; } = new Dictionary<TargetEffects, List<AEffect>>();


        static AEffect GetEffect(TargetEffects target, EffectId effectId)
        {
            if (!EffectsMap.ContainsKey(target))
                EffectsMap.Add(target, new List<AEffect>());

            var effect = EffectsMap[target].Find(e => e.Id == effectId);
            if (effect == null)
            {
                effect = Effects[effectId].Clone();
                EffectsMap[target].Add(effect);
            }
            return effect;
        }


        public static void SetEffect(TargetEffects target, EffectId effectId, bool isOn)
            => GetEffect(target, effectId).Set(target, isOn);

        public static void PlayEffect(TargetEffects target, EffectId effectId)
            => GetEffect(target, effectId).Play(target);


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        static Effecter Instance { get; set; }
        private void Awake()
        {
            Instance = this;
            EffectDeathPref = _effectDeathPref;
            Effects = new Dictionary<EffectId, AEffect>
            {
                { EffectId.SpeedUp, Instance._effectSpeedUp },
                { EffectId.Rage, Instance._effectRage },
                { EffectId.Slow, Instance._effectSlow },
                { EffectId.Damaged, Instance._effectDamaged },
                { EffectId.Immune, Instance._effectImmune },
                { EffectId.Spikes, Instance._effectSpikes },
                { EffectId.BlockMissiles, Instance._effectBlockMissiles }
            };
        }
    }
}