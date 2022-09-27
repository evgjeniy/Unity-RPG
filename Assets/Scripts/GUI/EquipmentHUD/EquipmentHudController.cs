using Equipments;

namespace GUI.EquipmentHUD
{
    [System.Serializable]
    public class EquipmentHudController
    {
        public EquipmentView View;
    
        public void Update(Equipment[] currentEquipment)
        {
            ClearAllSlots();
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                switch (currentEquipment[i]?.EquipmentSlot)
                {
                    case EquipmentSlot.Head: 
                        View.helmetSlot.AddItem(currentEquipment[i]);
                        break;
                    case EquipmentSlot.Chest: 
                        View.chestSlot.AddItem(currentEquipment[i]);
                        break;
                    case EquipmentSlot.Hands:
                        View.glovesSlot.AddItem(currentEquipment[i]);
                        break;
                    case EquipmentSlot.Legs:
                        View.pantsSlot.AddItem(currentEquipment[i]);
                        break;
                    case EquipmentSlot.Feet:
                        View.bootsSlot.AddItem(currentEquipment[i]);
                        break;
                    case EquipmentSlot.Weapon:
                        View.weaponSlot.AddItem(currentEquipment[i]);
                        break;
                    default: break;
                }
            }
        }
    
        public void OnEquipmentButtonClick()
        {
            View.itemsParent.SetActive(!View.itemsParent.activeSelf);
            View.playerImage.SetActive(!View.playerImage.activeSelf);
            View.title.SetActive(!View.title.activeSelf);
        }

        public void ClearAllSlots()
        {
            View.helmetSlot.ClearSlot();
            View.chestSlot.ClearSlot();
            View.glovesSlot.ClearSlot();
            View.pantsSlot.ClearSlot();
            View.bootsSlot.ClearSlot();
            View.weaponSlot.ClearSlot();
        }
        
        public bool IsActive()
        {
            return View.itemsParent.activeSelf;
        }
    }
}