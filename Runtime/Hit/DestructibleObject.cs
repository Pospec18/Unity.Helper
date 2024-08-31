using UnityEngine;
using UnityEngine.Events;

namespace Pospec.Helper.Hit
{
    public class DestructibleObject : Deathable
    {
        public UnityEvent onDeath;
        public UnityEvent onHit;

        protected override void OnEnable()
        {
            base.Start();
            OnTakeHit += OnHit;
        }

        private void OnDisable()
        {
            OnTakeHit -= OnHit;
        }

        private void OnHit(int damage, Component attacker)
        {
            onHit?.Invoke();
        }

        public override void Death()
        {
            onDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
