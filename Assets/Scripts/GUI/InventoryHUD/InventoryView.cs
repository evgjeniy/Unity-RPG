using UnityEngine;

namespace GUI.InventoryHUD
{
    public class InventoryView : MonoBehaviour
    {
        public GameObject itemsParent;
        public GameObject title;
        
        [HideInInspector] 
        public InventoryViewSlot[] slots;

        private void Start()
        {
            slots = itemsParent.GetComponentsInChildren<InventoryViewSlot>();
        }
    }
}