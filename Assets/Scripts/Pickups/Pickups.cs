using UnityEngine;
using System;

public enum PickupType
{
    Health,
    Damage,
    Ammo,
    KnockbackPower
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
        
        PlayerController playerController = player.GetComponent<PlayerController>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        switch (randomBoost)
        {
            case PickupType.Health:
                if (playerHealth != null)
                {
                    playerHealth.IncreaseMaxHealth(20);
                    OnPickupCollected?.Invoke("+20 Max Health");
                }
                break;

            case PickupType.Damage:
                if (playerController != null)
                {
                    playerController.IncreaseDamage(5);
                    OnPickupCollected?.Invoke("+5 Damage");
                }
                break;

            case PickupType.Ammo:
                if (playerController != null)
                {
                    playerController.RefillAmmo();
                    playerController.IncreaseMaxAmmo(5);
                    OnPickupCollected?.Invoke("Ammo Refilled +5 Capacity");
                }
                break;

            case PickupType.KnockbackPower:
                if (playerController != null)
                {
                    playerController.IncreaseKnockback(0.5f, 0.3f);
                    OnPickupCollected?.Invoke("+Knockback Power");
                }
                break;
        }
    }
}
