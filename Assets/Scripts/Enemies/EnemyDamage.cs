using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int missileDamage = 10;
    
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            Debug.Log("Enemy hit by missile!");
            
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(missileDamage);
            }
            else
            {
                Debug.LogWarning("Enemy missing EnemyHealth component!");
            }
        }
    }
}
