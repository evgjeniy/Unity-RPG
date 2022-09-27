using System;
using GUI.HealthBarHUD;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Enemy
{
    [Serializable]
    public class EnemyState : EntityState
    {
        private Enemy _enemy;
        public EnemyGui Gui { get; private set; }

        public override void Initialize<T1, T2>(Entity<T1, T2> entity)
        {
            CurrentHealth = maxHealth;
            _enemy = entity as Enemy;
            Gui = _enemy.GetComponentInChildren<EnemyGui>();
        }
        
        public override void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
            
            _enemy.State.Blood.transform.position = _enemy.transform.position + Vector3.up;
            _enemy.State.Blood.Play();
            
            Gui.healthBar.UpdateHealth(this);
            if (CurrentHealth <= 0) Die();
        }

        public override void Attack<T1, T2>(Entity<T1, T2> otherEntity)
        {
            if (AttackCooldown <= 0.0f)
            {
                otherEntity.State.TakeDamage(damage.Value);
                AttackCooldown = attackSpeed;
            }
        }

        public override void Die() => _enemy.StartCoroutine(_enemy.Controller.Fall());
    }
}