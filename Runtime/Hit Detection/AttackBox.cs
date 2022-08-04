using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper.Hit
{
    public class AttackBox : AttackArea
    {
        public Vector2 size;

        protected override Collider2D[] GetTargets(Vector2 center, Vector2 dir)
        {
            return Physics2D.OverlapBoxAll(center, size, dir.Angle());
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.DrawWireCube(GetCenter(Vector2.right), size);
        }
    }
}
