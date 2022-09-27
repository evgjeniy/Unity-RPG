using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon;
    public bool isDefaultItem;
    public GameObject dropPrefab;
    
    public abstract void Use();
}
