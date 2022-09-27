using Entities.Enemy;
using Entities.Player;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyInteractable : Interactable
{
    private Enemy _enemy;
    private void Start() => _enemy = GetComponent<Enemy>();
    
    protected override void Interact()
    {
        base.Interact();
        if (!Player.Instance.Gui.inventory.IsActive())
            Player.Instance.State.Attack(_enemy);
    }
}
