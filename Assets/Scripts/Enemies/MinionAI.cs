using UnityEngine;

public class MinionAI : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 12f;
   [SerializeField] private float maxForce = 20f;
   
   private Transform playerTransform;
   private Rigidbody2D rb;

   private void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
      
      if (rb != null)
      {
         rb.linearDamping = 0.5f;
      }
   }

   private void Start()
   {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      if (player != null)
      {
         playerTransform = player.transform;
      }
      
      Collider2D minionCollider = GetComponent<Collider2D>();
      if (minionCollider != null)
      {
         GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
         foreach (GameObject enemy in enemies)
         {
            if (enemy != gameObject)
            {
               Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
               if (enemyCollider != null)
               {
                  Physics2D.IgnoreCollision(minionCollider, enemyCollider);
               }
            }
         }
      }
   }

   private void FixedUpdate()
   {
      if (playerTransform == null || rb == null)
         return;

      Vector2 toPlayer = (Vector2)(playerTransform.position - transform.position);
      Vector2 desiredVelocity = toPlayer.normalized * moveSpeed;
      Vector2 steering = Vector2.ClampMagnitude(desiredVelocity - rb.linearVelocity, maxForce);
      rb.AddForce(steering);
   }
}
