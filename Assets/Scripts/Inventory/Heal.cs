using Entities.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal", menuName = "Inventory/Heal")]
public class Heal : Item
{
    [SerializeField] private int healEffect;
    
    public override void Use()
    {
        Player.Instance.State.Heal(healEffect);
        Player.Instance.Inventory.Remove(this);
    }
}
