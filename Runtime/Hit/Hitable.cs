using System;
using System.Collections;
using UnityEngine;

namespace Pospec.Helper.Hit
{
    public interface IHitable
    {
        public void TakeHit(int damage, Component attacker);
    }

    public abstract class Deathable : MonoBehaviour, IHitable
    {
        [Header("Health")]
        [SerializeField, Min(0)] protected int maxHealth = 4;
        [SerializeField, Min(0)] private float ignoreNextHitsTime = 1;

        private bool ignoreHits = false;
        private int _health;
        public int Health
        {
            get => _health;
            protected set
            {
                _health = value;
                if (_health > maxHealth)
                {
                    _health = maxHealth;
                    OnFullyHealed?.Invoke();
                }

                OnHealthChanged?.Invoke(_health);

                if (_health <= 0)
                    Death();
            }
        }

        public float MaxHealth => maxHealth;

        public event Action<int> OnHealthChanged;
        protected event Action OnFullyHealed;
        protected event Action<int> OnHealedBy;
        protected event Action<int, Component> OnTakeHit;

        private Animator _animator;
        protected Animator Animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();
                return _animator;
            }
        }

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
        }

        public void HealBy(int value)
        {
            Health += value;
            OnHealedBy?.Invoke(value);
        }

        public void TakeHit(int damage, Component attacker)
        {
            if (ignoreHits)
                return;
            Health -= damage;
            StartCoroutine(IgnoreNextHits());
            OnTakeHit?.Invoke(damage, attacker);
        }

        private IEnumerator IgnoreNextHits()
        {
            if (ignoreNextHitsTime <= 0)
                yield break;
            ignoreHits = true;
            yield return Helper.GetWait(ignoreNextHitsTime);
            ignoreHits = false;
        }

        public abstract void Death();
    }
}
