using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Char
{
    public interface IAttackable
    {
        bool TakeDamage(GameObject sender);
    }
}