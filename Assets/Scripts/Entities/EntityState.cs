using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public abstract class EntityState
    {
        [SerializeField] protected int maxHealth = 100;
        [SerializeField] protected Stat damage;
        [SerializeField] protected Stat armor;
        [SerializeField] protected float attackSpeed = 1.0f;

        public int CurrentHealth { get; set; }
        public int MaxHealth => maxHealth;
        public ParticleSystem Blood { get; set; }
        public float AttackCooldown { get; set; }

        public abstract void Attack<T1, T2>(Entity<T1, T2> otherEntity)
            where T1 : EntityController where T2 : EntityState;
        public abstract void Initialize<T1, T2>(Entity<T1, T2> entity)
            where T1 : EntityController where T2 : EntityState;
        public abstract void TakeDamage(int amount);
        public abstract void Die();
    }
}