using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    public interface IAttackParams
    {
        float AttackInterval { get; set; }
        float PrepareDelay { get; set; }
    }
}
