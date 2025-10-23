using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  [SerializeField] private GameObject missilePrefab;
  [SerializeField] private float knockbackForce = 5f;
  [SerializeField] private float maxKnockbackSpeed = 10f;
  [SerializeField] private float knockbackDecayRate = 5f;
  
  private Camera mainCamera;
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D rb;
  private PlayerHealth playerHealth;
  private float currentKnockbackSpeed = 0f;

   private void Start() {
     mainCamera = Camera.main;
     spriteRenderer = GetComponent<SpriteRenderer>();
     rb = GetComponent<Rigidbody2D>();
     playerHealth = GetComponent<PlayerHealth>();
   }

     private void Update() {
       RotateTowardsMouse();
       
       // Decay knockback speed over time
       if (currentKnockbackSpeed > 0f) {
         currentKnockbackSpeed -= knockbackDecayRate * Time.deltaTime;
         if (currentKnockbackSpeed < 0f) {
           currentKnockbackSpeed = 0f;
         }
       }
       
       if (Mouse.current.leftButton.wasPressedThisFrame) {
         Shoot();
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
      
      // Apply knockback to player in opposite direction of shooting
      if (rb != null) {
        Vector3 mousePosition3D = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 shootDirection = ((Vector2)mousePosition3D - (Vector2)transform.position).normalized;
        Vector2 knockbackDirection = -shootDirection;
        
        // Add to current knockback speed and cap at max
        currentKnockbackSpeed += knockbackForce;
        if (currentKnockbackSpeed > maxKnockbackSpeed) {
          currentKnockbackSpeed = maxKnockbackSpeed;
        }
        
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
      }
    }
}
