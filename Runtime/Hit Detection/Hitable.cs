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

    public abstract class Deathable : MonoBehaviour, IHitable
    {
        [SerializeField, Min(1)] protected float maxHealth = 4;

        protected float _health;
        protected float Health
        {
            get => _health;
            set
            {
                _health = value;
                if(_health > maxHealth)
                    _health = maxHealth;

                if(_health <= 0)
                    Death();
            }
        }

        protected virtual void Start()
        {
            FullyHeal();
        }

        public void FullyHeal()
        {
            Health = maxHealth;
            OnFullHeal();
        }

        protected virtual void OnFullHeal() { }

        public void HealBy(float value)
        {
            Health += value;
            OnHealBy(value);
        }

        protected virtual void OnHealBy(float value) { }

        public void TakeHit(float damage, Transform attacker)
        {
            Health -= damage;
            OnTakeHit(damage, attacker);
        }

        protected virtual void OnTakeHit(float damage, Transform attacker) { }

        public abstract void Death();
    }
}