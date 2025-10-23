using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  [SerializeField] private GameObject missilePrefab;
  [SerializeField] private float knockbackDistance = 2f;
  [SerializeField] private float knockbackSpeed = 10f;
  [SerializeField] private float fireRate = 0.2f;
  [SerializeField] private int missileDamage = 10;
  [SerializeField] private int maxAmmo = 10;
  [SerializeField] private float ammoRegenTime = 5f;
  
  private Camera mainCamera;
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D rb;
  private PlayerHealth playerHealth;
  private float nextFireTime = 0f;
  private Vector2 knockbackVelocity = Vector2.zero;
  private float knockbackTimer = 0f;
  private int currentAmmo;
  private float nextAmmoRegenTime = 0f;

   private void Start() {
     mainCamera = Camera.main;
     spriteRenderer = GetComponent<SpriteRenderer>();
     rb = GetComponent<Rigidbody2D>();
     playerHealth = GetComponent<PlayerHealth>();
     currentAmmo = maxAmmo;
   }

   private void Update() {
     RotateTowardsMouse();
     
     // Apply knockback movement
     if (knockbackTimer > 0f) {
       knockbackTimer -= Time.deltaTime;
       if (knockbackTimer <= 0f) {
         knockbackVelocity = Vector2.zero;
         rb.linearVelocity = Vector2.zero;
       }
     }
     
     // Regenerate ammo
     if (currentAmmo < maxAmmo && Time.time >= nextAmmoRegenTime) {
       currentAmmo++;
       nextAmmoRegenTime = Time.time + ammoRegenTime;
     }
     
     if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime && currentAmmo > 0) {
       Shoot();
       currentAmmo--;
       nextFireTime = Time.time + fireRate;
     }
   }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collision with missiles
        if (collision.gameObject.CompareTag("Missile"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
        
        // Damage player on enemy collision
        if (collision.gameObject.CompareTag("Enemy") && playerHealth != null)
        {
            playerHealth.TakeDamage(10);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision with missiles
        if (other.CompareTag("Missile"))
        {
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
            return;
        }
        
        // Damage player on enemy collision
        if (other.CompareTag("Enemy") && playerHealth != null)
        {
            playerHealth.TakeDamage(10);
        }
    }



    private void RotateTowardsMouse() {
      Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
      Vector2 direction = mousePosition - transform.position;
       float angle = Vector2.SignedAngle(Vector2.up, direction);
      transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    private void Shoot() {
      if (missilePrefab == null) {
        Debug.LogWarning("Missile prefab not assigned!");
        return;
      }
      
      // Spawn missile at the top of the player (offset in transform.up direction)
      Vector3 spawnPosition = transform.position + transform.up * 0.5f;
      GameObject missile = Instantiate(missilePrefab, spawnPosition, transform.rotation);

      Missile missileScript = missile.GetComponent<Missile>();
      if (missileScript != null)
      {
          missileScript.SetDamage(missileDamage);
      }
      
      // Apply knockback to player in opposite direction of shooting
      if (rb != null) {
        Vector3 mousePosition3D = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 shootDirection = ((Vector2)mousePosition3D - (Vector2)transform.position).normalized;
        Vector2 knockbackDirection = -shootDirection;
        
        // Calculate velocity needed to travel knockbackDistance
        knockbackVelocity = knockbackDirection * knockbackSpeed;
        knockbackTimer = knockbackDistance / knockbackSpeed;
        
        rb.linearVelocity = knockbackVelocity;
      }
    }

    public void IncreaseFireRate(float decrease)
    {
        fireRate -= decrease;
        if (fireRate < 0.1f)
        {
            fireRate = 0.1f;
        }
    }

    public void IncreaseDamage(int amount)
    {
        missileDamage += amount;
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
    }

    public void IncreaseMaxAmmo(int amount)
    {
        maxAmmo += amount;
        currentAmmo = maxAmmo;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}
