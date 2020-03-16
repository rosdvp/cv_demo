using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades
{
    public abstract class AUpgradeBasic : AUpgrade
    {
        [Header("Params")]
        [SerializeField]
        int _allowedCount;

        public int AllowedCount => _allowedCount;
    }
}