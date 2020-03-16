using Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    public class Buffer : MonoBehaviour
    {
        [Header("Assets")]
        [SerializeField]
        BuffSpeedUp _killSpeedUp;
        [SerializeField]
        BuffSlowDown _slowDownByMob;
        [SerializeField]
        BuffSlowDown _shooterRetreat;
        [SerializeField]
        BuffRage _killRage;
        [SerializeField]
        BuffImmune _immuneBase;
        [SerializeField]
        BuffImmune _immuneLong;


        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static Dictionary<BuffId, ABuff> Buffs { get; set; }
        static Dictionary<TargetBuffs, List<ABuff>> BuffsMap { get; set; } = new Dictionary<TargetBuffs, List<ABuff>>();

        static ABuff GetBuff(TargetBuffs target, BuffId buffId)
        {
            if (!BuffsMap.ContainsKey(target))
                BuffsMap.Add(target, new List<ABuff>());

            var buff = BuffsMap[target].Find(b => b.Id == buffId);
            if (buff == null)
            {
                buff = Buffs[buffId].Clone();
                BuffsMap[target].Add(buff);
            }
            return buff;
        }

        public static void SetBuff(TargetBuffs target, BuffId buffId, bool isOn)
            => GetBuff(target, buffId).Set(target, isOn);

        public static void PlayBuff(TargetBuffs target, BuffId buffId)
            => GetBuff(target, buffId).Play(target);


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        static Buffer Instance { get; set; }
        private void Awake()
        {
            Instance = this;
            Buffs = new Dictionary<BuffId, ABuff>
            {
                { BuffId.KillSpeedUp, Instance._killSpeedUp },
                { BuffId.SlowDownByMob, Instance._slowDownByMob },
                { BuffId.KillRage, Instance._killRage },
                { BuffId.ImmuneBase, Instance._immuneBase },
                { BuffId.ImmuneLong, Instance._immuneLong },
                { BuffId.ShooterRetreat, Instance._shooterRetreat }
            };
        }
    }
}