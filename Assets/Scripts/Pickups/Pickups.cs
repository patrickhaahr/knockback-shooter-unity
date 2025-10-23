using UnityEngine;

public enum PickupType
{
    Health,
    FireRate,
    Damage
}

public class Pickups : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyRandomBoost(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void ApplyRandomBoost(GameObject player)
    {
        PickupType randomBoost = (PickupType)Random.Range(0, 3);

        switch (randomBoost)
        {
            case PickupType.Health:
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(20);
                }
                break;

            case PickupType.FireRate:
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.IncreaseFireRate(0.1f);
                }
                break;

            case PickupType.Damage:
                PlayerController controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    controller.IncreaseDamage(5);
                }
                break;
        }
    }
}
