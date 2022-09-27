using System.Collections.Generic;
using System.Linq;
using Entities.Enemy;
using Entities.Player;
using UnityEngine;
using Zenject;

public class EnemyObjectPool : MonoBehaviour
{
    [Inject] private Player _player;
    [SerializeField] private int activeAmount;
    private List<GameObject> _enemies = new List<GameObject>();

    void Start()
    {
        foreach (var enemy in GetComponentsInChildren<Enemy>())
            _enemies.Add(enemy.gameObject);
    }

    void Update()
    {
        if (ActiveEnemiesAmount() < activeAmount)
        {
            SetActive(_enemies.FirstOrDefault(e => Vector3.Distance(
                e.transform.position, _player.transform.position) > 20.0f && !e.activeSelf)?.GetComponent<Enemy>());
        }
    }

    private void SetActive(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.State.CurrentHealth = enemy.State.MaxHealth;
            enemy.State.Gui.healthBar.UpdateHealth(enemy.State);
            enemy.Animator.SetBool("IsDead", false);
            enemy.GetComponent<CapsuleCollider>().enabled = true;
            enemy.gameObject.GetComponent<EnemyInteractable>().enabled = true;
            enemy.Controller.IsDead = false;
        }
    } 

    private int ActiveEnemiesAmount()
    {
        int amount = 0;
        _enemies.ForEach(e => amount += e.activeSelf ? 1 : 0);
        return amount;
    } 
}
