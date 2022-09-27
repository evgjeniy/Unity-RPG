using Entities.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GUI.InventoryHUD
{
    public class InventoryViewSlot : MonoBehaviour
    {
        [Inject] private Player _player;
        public Image icon;
        public Button removeButton;
        private Item _item;

        public void AddItem(Item newItem)
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
            if (_item.dropPrefab != null)
                Instantiate(_item.dropPrefab, _player.transform.position, Quaternion.identity);
            _player.Inventory.Remove(_item);
        }

        public void OnSlotIconClick()
        {
            if (_item != null) _item.Use();
        }
    }
}