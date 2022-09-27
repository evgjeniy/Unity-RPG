using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item item;
    
    protected override void Interact() => PickUp();

    private void PickUp()
    {
        if (Entities.Player.Player.Instance.Inventory.Add(item))
        {
            Destroy(gameObject);
            ClearDescription();
        }
        else
        {
            Entities.Player.Player.Instance.Gui.descriptionHud.SetText("Inventory is full");
            Invoke("ClearDescription", 1.0f);
        }
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(0.0f, 2.5f, 0.0f, Space.World);
    }

    protected override void ShowDescription()
    {
        base.ShowDescription();
        Entities.Player.Player.Instance.Gui.descriptionHud.SetText(
            $"Press {System.Enum.GetName(typeof(KeyCode), inputKey)} - pick up the {item.name}");
    }

    protected override void ClearDescription()
    {
        base.ClearDescription();
        Entities.Player.Player.Instance.Gui.descriptionHud.SetText("");
    }
}
