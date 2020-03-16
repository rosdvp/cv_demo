using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public abstract class AEffect
    {
        [Tooltip("Id эффекта (совпадает с названием)")]
        [SerializeField]
        protected EffectId _id;
        [Tooltip("Длительность, если используется эффектом")]
        [SerializeField]
        protected float _duration;

        public EffectId Id => _id;
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        protected TargetEffects Target { get; set; }

        public void Set(TargetEffects target, bool isOn)
        {
            if (target == null || !target.gameObject.activeInHierarchy)
                return;

            Target = target;
            IsPlaying = isOn;

            if (isOn)
                OnEffectStart();
            else
                OnEffectEnd();
        }

        public void Play(TargetEffects target)
        {
            if (target == null || !target.gameObject.activeInHierarchy)
                return;

            Target = target;

            if (IsPlaying)
                Timer.Set(_duration);
            else
                Consts.CourutineHolder.StartCoroutine(CrPlay());
        }

        protected abstract void OnEffectStart();
        protected abstract void OnEffectEnd();
        public abstract AEffect Clone();

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        protected bool IsPlaying { get; set; }
        Stuff.Timer Timer { get; set; } = new Stuff.Timer();

        IEnumerator CrPlay()
        {
            if (IsPlaying)
                yield return new WaitWhile(() => IsPlaying);
            IsPlaying = true;

            OnEffectStart();

            Timer.Set(_duration);
            yield return new WaitUntil(() => Timer.IsDone);

            OnEffectEnd();
            
            IsPlaying = false;
        }
    }
}