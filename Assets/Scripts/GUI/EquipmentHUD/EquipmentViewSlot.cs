using Entities.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GUI.EquipmentHUD
{
    public class EquipmentViewSlot : MonoBehaviour
    {
        [Inject] private Player _player;
        public Image icon;
        public Button removeButton;
        private Equipments.Equipment _item;

        public void AddItem(Equipments.Equipment newItem)
        {
            _item = newItem;
            icon.sprite = _item.icon;
            icon.enabled = true;
            removeButton.interactable = true;
        }

        public void ClearSlot()
        {
            _item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

        public void OnRemoveButtonClick()
        {
            _player.EquipmentController.Unequip((int)_item.EquipmentSlot);
            _player.Inventory.Remove(_item);
        }
    }
}