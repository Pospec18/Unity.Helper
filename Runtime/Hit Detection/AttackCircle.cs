using UnityEngine;

namespace Pospec.Helper.Hit
{
    public class AttackCircle : AttackArea
    {
        public float range;

        protected override Collider2D[] GetTargets(Vector2 center, Vector2 dir)
        {
            return Physics2D.OverlapCircleAll(center, range, hitLayer);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.DrawWireSphere(GetCenter(Vector2.right), range);
        }
    }
}
