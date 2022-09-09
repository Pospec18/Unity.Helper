using System;
using UnityEngine;

namespace Pospec.Helper.Hit
{
    public interface IHitable
    {
        public void TakeHit(float damage, Transform attacker);
    }

    public abstract class Deathable : MonoBehaviour, IHitable
    {
        [SerializeField, Min(1)] protected float maxHealth = 4;

        private float _health;
        public float Health
        {
            get => _health;
            protected set
            {
                if(_health != value)
                    OnHealthChanged?.Invoke(value);

                _health = value;
                if(_health > maxHealth)
                    _health = maxHealth;

                if(_health <= 0)
                    Death();
            }
        }

        public event Action<float> OnHealthChanged;
        protected event Action OnFullHeal;
        protected event Action<float> OnHealBy;
        protected event Action<float, Transform> OnTakeHit;

        protected virtual void Start()
        {
            FullyHeal();
        }

        public void FullyHeal()
        {
            Health = maxHealth;
            OnFullHeal?.Invoke();
        }

        public void HealBy(float value)
        {
            Health += value;
            OnHealBy?.Invoke(value);
        }

        public void TakeHit(float damage, Transform attacker)
        {
            Health -= damage;
            OnTakeHit?.Invoke(damage, attacker);
        }

        public abstract void Death();
    }
}