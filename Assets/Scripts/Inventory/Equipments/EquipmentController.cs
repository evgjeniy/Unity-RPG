using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace Equipments
{
    [System.Serializable]
    public class EquipmentController
    {
        private Player _player;
        public List<MeshContainer> meshes = new List<MeshContainer>();

        [Space(2.0f)]
        public Equipment[] currentEquipment;
    
        public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
        public OnEquipmentChange onEquipmentChangeCallback;
    
        public void Initialize(Player player)
        {
            _player = player;
            int slotsAmount = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
            currentEquipment = new Equipment[slotsAmount];
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.U)) 
                UnequipAll();
        }

        public void Equip(Equipment newItem)
        {
            int slotIndex = (int) newItem.EquipmentSlot;

            Equipment oldItem = null;
            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                _player.Inventory.Add(oldItem);
            }

            currentEquipment[slotIndex] = newItem;
            onEquipmentChangeCallback?.Invoke(newItem, oldItem);
            _player.Gui.equipment.Update(currentEquipment);
            UpdateEquipmentMeshes();
        }

        public void Unequip(int slotIndex)
        {
            if (currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];

                if (_player.Inventory.Add(oldItem))
                {
                    currentEquipment[slotIndex] = null;
                    onEquipmentChangeCallback?.Invoke(null, oldItem);
                    UpdateEquipmentMeshes();
                    _player.Gui.equipment.Update(currentEquipment);
                }
            }
        }

        public void UnequipAll()
        {
            for (int i = 0; i < currentEquipment.Length; i++) 
                Unequip(i);
        }

        public void UpdateEquipmentMeshes()
        {
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                bool isCurrentEquipmentEmpty = currentEquipment[i] == null;
                foreach (MeshContainer meshContainer in meshes)
                {
                    if ((int)meshContainer.equipmentSlot == i)
                    {
                        foreach (GameObject defaultMesh in meshContainer.defaultMesh)
                            defaultMesh.SetActive(isCurrentEquipmentEmpty);

                        foreach (GameObject newMesh in meshContainer.newMesh)
                            newMesh.SetActive(!isCurrentEquipmentEmpty);
                    }
                }
            }
        }
    }
}