using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{
    public class AttackPlayer : ShootingEditor2DController
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.SendCommand<HurtPlayerCommand>();
            }
        }
        
    }
}