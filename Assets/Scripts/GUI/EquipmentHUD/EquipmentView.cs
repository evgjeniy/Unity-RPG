using UnityEngine;

namespace GUI.EquipmentHUD
{
    public class EquipmentView : MonoBehaviour
    {
        public GameObject itemsParent;
        public GameObject playerImage;
        public GameObject title;
    
        [Space]
        public EquipmentViewSlot helmetSlot;
        public EquipmentViewSlot chestSlot;
        public EquipmentViewSlot glovesSlot;
        public EquipmentViewSlot pantsSlot;
        public EquipmentViewSlot bootsSlot;
        public EquipmentViewSlot weaponSlot;
    }
}