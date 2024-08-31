using UnityEngine;

namespace Pospec.Helper.Hit
{
    public abstract class AttackArea : MonoBehaviour
    {
        public float offset;
        public LayerMask hitLayer;

        public void Attack(float damage, Vector2 dir)
        {
            Collider2D[] targets = GetTargets(GetCenter(dir), dir);
            for (int i = 0; i < targets.Length; i++)
            {
                HitDetect.Hit(targets[i], transform, damage);
            }
        }

        public void Attack(float damage, Vector2 dir, float knockback)
        {
            Collider2D[] targets = GetTargets(GetCenter(dir), dir);
            for (int i = 0; i < targets.Length; i++)
            {
                HitDetect.Hit(targets[i], transform, damage, knockback, dir);
            }
        }

        protected abstract Collider2D[] GetTargets(Vector2 center, Vector2 dir);

        public Vector2 GetCenter(Vector2 dir)
        {
            return (Vector2)transform.position + dir.normalized * offset;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
        }
    }
}