using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pospec.Helper.Hit
{
    public interface IHitable
    {
        public void TakeHit(float damage, Transform attacker);
    }
}