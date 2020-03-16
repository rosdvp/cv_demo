using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class EffectImmune : AEffect
    {
        [Header("Params")]
        [Tooltip("На какую прозрачность скачет (0 - полная, 1 - нет)")]
        [SerializeField]
        float _alpha;
        [Tooltip("Частота мигания (в секундах)")]
        [SerializeField]
        float _rate;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        protected override void OnEffectStart()
            => Target.StartCoroutine(CrEffect());

        protected override void OnEffectEnd() { }


        IEnumerator CrEffect()
        {
            var isTransp = false;
            Color color;

            while (IsPlaying)
            {
                isTransp = !isTransp;

                color = Target._renderer.color;
                color.a = isTransp ? _alpha : 1;
                Target._renderer.color = color;

                yield return new WaitForSeconds(_rate);
            }
            if (isTransp)
            {
                color = Target._renderer.color;
                color.a = 1;
                Target._renderer.color = color;
            }
        }


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public override AEffect Clone()
            => new EffectImmune()
            {
                _id = _id,
                _duration = _duration,
                _alpha = _alpha,
                _rate = _rate
            };
    }
}