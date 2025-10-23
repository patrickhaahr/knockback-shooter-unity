using UnityEngine;
using System;

public enum PickupType
{
    Health,
    FireRate,
    Damage,
    Ammo
}

public class Pickups : MonoBehaviour
{
    public static event Action<string> OnPickupCollected;

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
        PickupType randomBoost = (PickupType)UnityEngine.Random.Range(0, 4);

        switch (randomBoost)
        {
            case PickupType.Health:
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(20);
                    OnPickupCollected?.Invoke("+20 Health");
                }
                break;

            case PickupType.FireRate:
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.IncreaseFireRate(0.1f);
                    OnPickupCollected?.Invoke("+Fire Rate");
                }
                break;

            case PickupType.Damage:
                PlayerController controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    controller.IncreaseDamage(5);
                    OnPickupCollected?.Invoke("+5 Damage");
                }
                break;

            case PickupType.Ammo:
                PlayerController ammoController = player.GetComponent<PlayerController>();
                if (ammoController != null)
                {
                    ammoController.RefillAmmo();
                    ammoController.IncreaseMaxAmmo(5);
                    OnPickupCollected?.Invoke("Ammo Refilled +5 Capacity");
                }
                break;
        }
    }
}
