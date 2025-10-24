using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  [SerializeField] private GameObject missilePrefab;
  [SerializeField] private float knockbackDistance = 3f;
  [SerializeField] private float knockbackSpeed = 10f;
  [SerializeField] private float fireRate = 0.1f;
  [SerializeField] private int missileDamage = 25;
  [SerializeField] private int maxAmmo = 15;
  [SerializeField] private float ammoRegenTime = 2f;

  
  private Camera mainCamera;
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D rb;
  private PlayerHealth playerHealth;
  private float nextFireTime = 0f;
  private Vector2 knockbackVelocity = Vector2.zero;
  private float knockbackTimer = 0f;
  private int currentAmmo;
  private float nextAmmoRegenTime = 0f;
  private Vector3 mouseWorldPosition;

   private void Start() {
     mainCamera = Camera.main;
     spriteRenderer = GetComponent<SpriteRenderer>();
     rb = GetComponent<Rigidbody2D>();
     playerHealth = GetComponent<PlayerHealth>();
     currentAmmo = maxAmmo;
   }

private void Update() {
      mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
      RotateTowardsMouse();
      
      HandleMovement();
      
      if (knockbackTimer > 0f) {
        knockbackTimer -= Time.deltaTime;
        if (knockbackTimer <= 0f) {
          knockbackVelocity = Vector2.zero;
          rb.linearVelocity = Vector2.zero;
        }
      }
     
      // Ammo 
      if (currentAmmo < maxAmmo && Time.time >= nextAmmoRegenTime) {
        currentAmmo += 2;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
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
        if (collision.gameObject.CompareTag("Missile"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
        
        if (collision.gameObject.CompareTag("Enemy") && playerHealth != null)
        {
            playerHealth.TakeDamage(10);
        }
    }



    private void HandleMovement() {
      if (knockbackTimer > 0f) return;
      
      if (rb != null) {
        rb.linearVelocity = Vector2.zero;
      }
    }
    
    private void RotateTowardsMouse() {
      Vector2 direction = mouseWorldPosition - transform.position;
       float angle = Vector2.SignedAngle(Vector2.up, direction);
      transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    private void Shoot() {
      if (missilePrefab == null) {
        Debug.LogWarning("Missile prefab not assigned!");
        return;
      }
     
      // follow cursor position
      Vector3 spawnPosition = transform.position + transform.up * 0.5f;
      GameObject missile = Instantiate(missilePrefab, spawnPosition, transform.rotation);

      Missile missileScript = missile.GetComponent<Missile>();
      if (missileScript != null)
      {
          missileScript.SetDamage(missileDamage);
      }
     
      // knockback 
      if (rb != null) {
        Vector2 shootDirection = ((Vector2)mouseWorldPosition - (Vector2)transform.position).normalized;
        Vector2 knockbackDirection = -shootDirection;
        
        knockbackVelocity = knockbackDirection * knockbackSpeed;
        knockbackTimer = knockbackDistance / knockbackSpeed;
        
        rb.linearVelocity = knockbackVelocity;
      }
    }

    public void IncreaseDamage(int amount)
    {
        missileDamage += amount;
    }

    public void IncreaseKnockback(float distanceIncrease, float speedIncrease)
    {
        knockbackDistance += distanceIncrease;
        knockbackSpeed += speedIncrease;
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

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
    }
}
