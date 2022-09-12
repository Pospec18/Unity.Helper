using System;
using System.Collections;
using UnityEngine;

namespace Pospec.Helper.Hit
{
    public interface IHitable
    {
        public void TakeHit(float damage, Transform attacker);
    }

    public abstract class Deathable : MonoBehaviour, IHitable
    {
        [Header("Health")]
        [SerializeField, Min(1)] protected float maxHealth = 4;
        [SerializeField, Min(1)] private float ignoreNextHitsTime = 1;

        private bool ignoreHits = false;
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

        protected virtual void OnEnable()
        {
            ignoreHits = false;
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
            if (ignoreHits)
                return;
            Health -= damage;
            StartCoroutine(IgnoreNextHits());
            OnTakeHit?.Invoke(damage, attacker);
        }

        private IEnumerator IgnoreNextHits()
        {
            if(ignoreNextHitsTime <= 0)
                yield break;
            ignoreHits = true;
            yield return Helper.GetWait(ignoreNextHitsTime);
            ignoreHits = false;
        }

        public abstract void Death();
    }
}