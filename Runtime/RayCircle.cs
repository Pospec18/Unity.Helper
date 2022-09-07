using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper
{
    public class RayCircle : RayShape
    {
        [SerializeField] private float radius = 1;
        [SerializeField] private Vector2 center;

        public override Collider2D Overlap(LayerMask layer)
        {
            return Physics2D.OverlapCircle((Vector2)transform.position + center, radius, layer);
        }

        public override Collider2D[] OverlapAll(LayerMask layer)
        {
            return Physics2D.OverlapCircleAll((Vector2)transform.position + center, radius, layer);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.DrawWireSphere((Vector2)transform.position + center, radius);
        }
    }
}
