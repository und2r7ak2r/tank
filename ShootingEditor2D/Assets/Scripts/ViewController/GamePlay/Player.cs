using FrameworkDesign;
using UnityEngine;

namespace ShootingEditor2D
{

    public class Player : ShootingEditor2DController
    {
        private Rigidbody2D mRigidbody2D;
        private Animator  mAnimator;
        private bool mJumpPressed;

        private Gun mGun;
        private Trigger2DCheck mGroundCheck;
        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mGroundCheck=transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();
            mGun=transform.Find("Gun").GetComponent<Gun>();
            mAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                mJumpPressed = true;
            }
            if(Input.GetKeyDown(KeyCode.J))
            {
                mGun.Shoot();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                mGun.Reload();
            }
            if(Input.GetKeyDown (KeyCode.Q))
            {
                this.SendCommand<ShiftGunCommand>();
            }
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");
            if (horizontalMovement != 0)
            {
                mAnimator.SetBool("IsRun", true);
                if (horizontalMovement > 0 && transform.localScale.x < 0
                    || horizontalMovement < 0 && transform.localScale.x > 0)
                {
                    var localScale = transform.localScale;
                    localScale.x = -localScale.x;
                    transform.localScale = localScale;
                }
                mRigidbody2D.velocity = new Vector2(horizontalMovement * 5f, mRigidbody2D.velocity.y);
            }
            else 
            {
                mAnimator.SetBool("IsRun", false);
            }
           
            var grounded = mGroundCheck.Triggered;
            if (mJumpPressed && grounded)
            {
                mAnimator.SetBool("IsRun", false);
                mAnimator.SetBool("IsJump", true);
                mJumpPressed = false;
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, 5f);
            }
            else
            {
                mAnimator.SetBool("IsJump", false);
            }
        }

    }
}