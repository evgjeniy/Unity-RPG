using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private Player _player;
    [SerializeField] private int slots = 20;
    public List<Item> items = new List<Item>();

    public void Initialize(Player player)
    {
        _player = player;
    }
    
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= slots) return false; // not enough space
            items.Add(item);
            _player.Gui.inventory.Update(this);
        }
        
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        _player.Gui.inventory.Update(this);
    }
}
