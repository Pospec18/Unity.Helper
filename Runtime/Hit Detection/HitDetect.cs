using UnityEngine;

namespace Pospec.Helper.Hit
{
    public class HitDetect
    {
        public static bool Hit(Component target, Component attacker, float damage, float knockback, Vector2 dir)
        {
            return Hit(target, attacker, damage, knockback, dir, target.GetComponent<Rigidbody2D>());
        }

        public static bool Hit(Component target, Component attacker, float damage, float knockback, Vector2 dir, Rigidbody2D targerRB)
        {
            if (targerRB != null)
                targerRB.velocity += dir.normalized * knockback;

            return Hit(target, attacker, damage);
        }

        public static bool Hit(Component target, Component attacker, float damage)
        {
            if (target.gameObject != attacker.gameObject && target.TryGetComponent(out IHitable hitable))
            {
                hitable.TakeHit(damage, attacker.transform);
                return true;
            }

            return false;
        }
    }
}