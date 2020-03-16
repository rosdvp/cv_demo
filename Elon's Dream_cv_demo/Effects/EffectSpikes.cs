using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class EffectSpikes : AEffect
    {
        protected override void OnEffectStart()
        {
            IEnumerator CrSpikes()
            {
                Target._spikesPS.gameObject.SetActive(true);
                yield return new WaitWhile(() => Target._spikesPS.isPlaying);
                Target._spikesPS.gameObject.SetActive(false);
            }
            Consts.CourutineHolder.StartCoroutine(CrSpikes());
        }

        protected override void OnEffectEnd() { }
        public override AEffect Clone()
            => new EffectSpikes()
            {
                _id = _id,
                _duration = _duration
            };
    }
}