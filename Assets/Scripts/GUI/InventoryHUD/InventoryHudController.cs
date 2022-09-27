namespace GUI.InventoryHUD
{
    [System.Serializable]
    public class InventoryController
    {
        public InventoryView View;

        public void Update(Inventory inventory)
        {
            for (int i = 0; i < View.slots.Length; i++)
            {
                if (i < inventory.items.Count)
                    View.slots[i].AddItem(inventory.items[i]);
                else
                    View.slots[i].ClearSlot();
            }
        }

        public void OnInventoryButtonClick()
        {
            View.itemsParent.SetActive(!View.itemsParent.activeSelf);
            View.title.SetActive(!View.title.activeSelf);
        }

        public bool IsActive()
        {
            return View.itemsParent.activeSelf;
        }
    }
}