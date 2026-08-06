using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans;
        private Vector3 TargetPos;

        private float mMinX = -5;
        private float mMaxX = 5;
        private float mMinY = -5;
        private float mMaxY = 5;


        private void LateUpdate()
        {
            if(!mPlayerTrans)
            {
                var playerGameObj= GameObject.FindWithTag("Player");
                if (playerGameObj)
                {
                    mPlayerTrans = playerGameObj.transform;
                }
                else
                {
                    return;
                }
            }

            var playerPos = mPlayerTrans.position;

            TargetPos.x = playerPos.x+3*Mathf.Sign(mPlayerTrans.localScale.x);
            TargetPos.y = playerPos.y + 2;
            TargetPos.z = -10;
            var smoothSpeed = 5;
            var position = transform.position;
            position = Vector3.Lerp(position, new Vector3(TargetPos.x, TargetPos.y, position.z), 
                smoothSpeed * Time.deltaTime);

            transform.position = new Vector3(Mathf.Clamp(position.x, mMinX, mMaxX),
                Mathf.Clamp(position.y, mMinY, mMaxY),
                position.z);

        }
    }
}