using UnityEngine;

namespace ShootingEditor2D
{

    public class Trigger2DCheck : MonoBehaviour
    {
        private int EnterCount = 0;
        public LayerMask layerMask;
        public bool Triggered
        {
            get
            {
                return EnterCount > 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject, layerMask))
            {
                EnterCount++;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject, layerMask))
            {
                EnterCount--;
            }
        }
        bool IsInLayerMask(GameObject obj, LayerMask layerMask) 
        {
            // 根据Layer数值进行移位获得用于运算的Mask值
            var objLayerMask = 1 << obj.layer;
            return (layerMask.value & objLayerMask) > 0;
        }

    }
}