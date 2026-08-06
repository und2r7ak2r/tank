using UnityEngine;

namespace ShootingEditor2D
{
    public class Enemy : MonoBehaviour
    {
        private Trigger2DCheck mWallCheck;
        private Trigger2DCheck mFallCheck;
        private Trigger2DCheck mGroundCheck;
        private Animator mAnimator;

        private Rigidbody2D mRigidbody2D;

        private void Awake()
        {
            mWallCheck = transform.Find("WallCheck").GetComponent<Trigger2DCheck>();
            mFallCheck = transform.Find("FallCheck").GetComponent<Trigger2DCheck>();
            mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();

            mRigidbody2D = GetComponent<Rigidbody2D>();
            mAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            var scaleX = transform.localScale.x;
            if (mGroundCheck.Triggered && mFallCheck.Triggered && !mWallCheck.Triggered)
            {
                mAnimator.SetBool("IsEnRun", true);
                mRigidbody2D.velocity = new Vector2(scaleX * 1, mRigidbody2D.velocity.y);
            }
            else 
            {
                if (mGroundCheck.Triggered)
                {
                    var localScale = transform.localScale;
                    localScale.x = -scaleX;
                    transform.localScale = localScale;
                }
            }
        }
    }
}
