using System;
using Equipments;
using UnityEngine;

namespace Entities.Player
{
    [Serializable]
    public class PlayerState : EntityState
    {
        private Player _player;

        public override void Initialize<T1, T2>(Entity<T1, T2> entity)
        {
            CurrentHealth = maxHealth;
            _player = entity as Player;
            _player.EquipmentController.onEquipmentChangeCallback += OnEquipmentChange;
        }

        public override void TakeDamage(int amount)
        {
            amount = (int)(Mathf.Lerp(0, 1, amount / (float)armor.Value) * amount);
            
            CurrentHealth -= amount;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

            _player.Invoke("SetHitAnimation", 0.5f);
            _player.Invoke("ResetHitAnimation", 0.6f);
            
            _player.Gui.healthBar.UpdateHealth(this);
            if (CurrentHealth == 0) Die();
        }

        public override void Attack<T1, T2>(Entity<T1, T2> otherEntity)
        {
            if (AttackCooldown <= 0.0f)
            {
                if (otherEntity != null)
                {
                    _player.Animator.SetBool("IsAttacking", true);
                    Vector3 direction = (otherEntity.transform.position - _player.transform.position).normalized;
                    _player.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
                    otherEntity.State.TakeDamage(damage.Value);
                }
                AttackCooldown = attackSpeed;
            }
        }

        public override void Die() => _player.StartCoroutine(_player.Controller.Fall());

        private void OnEquipmentChange(Equipment newItem, Equipment oldItem)
        {
            if (newItem != null)
            {
                damage.AddModifier(newItem.DamageModifier);
                armor.AddModifier(newItem.ArmorModifier);
            }

            if (oldItem != null)
            {
                damage.RemoveModifier(oldItem.DamageModifier);
                armor.RemoveModifier(oldItem.ArmorModifier);
            }
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
            _player.Gui.healthBar.UpdateHealth(this);
        }
    }
}