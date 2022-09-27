using Entities.Player;
using UnityEngine;

namespace Equipments
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        [SerializeField] private EquipmentSlot equipmentSlot;
        [SerializeField] private int armorModifier;
        [SerializeField] private int damageModifier;
        public EquipmentSlot EquipmentSlot => equipmentSlot;
        public int ArmorModifier => armorModifier;
        public int DamageModifier => damageModifier;
        
        public override void Use()
        {
            Player.Instance.EquipmentController.Equip(this);
            Player.Instance.Inventory.Remove(this);
        }
    }

    public enum EquipmentSlot { Head, Chest, Hands, Legs, Feet, Weapon }
}