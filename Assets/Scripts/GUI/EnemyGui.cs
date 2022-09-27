using GUI.HealthBarHUD;
using UnityEngine;

public class EnemyGui : MonoBehaviour
{
    public HealthBarHudController healthBar;
    
    public void LookAtPlayerCamera()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}
