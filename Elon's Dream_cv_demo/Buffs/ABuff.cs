using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Buffs
{
    [System.Serializable]
    public abstract class ABuff
    {
        [Tooltip("Id баффа (совпадает с названием)")]
        [SerializeField]
        protected BuffId _id;
        [Tooltip("Эффект сопровождающий бафф")]
        [SerializeField]
        protected EffectId _effectId;
        [Tooltip("Длительность баффа, если есть")]
        [SerializeField]
        protected float _duration;

        public BuffId Id => _id;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        protected TargetBuffs Target { get; private set; }

        public void Set(TargetBuffs target, bool isOn)
        {
            if (target == null || !target.gameObject.activeInHierarchy)
                return;

            Target = target;
            IsPlaying = isOn;

            if (isOn)
                OnBuffStart();
            else
                OnBuffEnd();
        }

        public void Play(TargetBuffs target)
        {
            if (target == null || !target.gameObject.activeInHierarchy)
                return;

            Target = target;

            if (IsPlaying)
                Timer.Set(_duration);
            else
                Consts.CourutineHolder.StartCoroutine(CrPlay());
        }


        protected abstract void OnBuffStart();
        protected abstract void OnBuffEnd();
        public abstract ABuff Clone();

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        bool IsPlaying { get; set; }
        Stuff.Timer Timer { get; set; } = new Stuff.Timer();

        IEnumerator CrPlay()
        {
            if (IsPlaying)
                yield return new WaitWhile(() => IsPlaying);
            IsPlaying = true;

            Effecter.SetEffect(Target.TargetEffects, _effectId, true);
            OnBuffStart();

            Timer.Set(_duration);
            yield return new WaitUntil(() => Timer.IsDone);

            OnBuffEnd();
            Effecter.SetEffect(Target.TargetEffects, _effectId, false);

            IsPlaying = false;
        }
    }
}