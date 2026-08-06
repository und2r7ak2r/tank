using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class Bullet : ShootingEditor2DController
    {
        private Rigidbody2D mRigidbody2D;
        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 5);
        }

        private void Start()
        {

            mRigidbody2D.velocity = Vector2.right * 10f*Mathf.Sign(transform.localScale.x);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                this.SendCommand<KillEnemyCommand>();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        
    }
}