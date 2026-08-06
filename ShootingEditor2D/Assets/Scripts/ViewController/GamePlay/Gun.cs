using UnityEngine;
using FrameworkDesign;
using System;

namespace ShootingEditor2D
{

    public class Gun : ShootingEditor2DController
    {
        private Bullet mBullet;
        private GunInfo mGunInfo;
        private int mMaxBulletCount;
        private void Awake()
        {
            mBullet =transform.Find("Bullet").GetComponent<Bullet>();
            mGunInfo = this.GetSystem<IGunSystem>().CurrentGun;
            mMaxBulletCount = this.SendQuery(new MaxBulletCountQuery(mGunInfo.Name.Value));
        }
        public void Shoot()
        {
            if (mGunInfo.BulletCountInGun.Value > 0&&mGunInfo.GunState.Value==GunState.Idle)
            {
                var bullet = Instantiate(mBullet.transform, mBullet.transform.position, mBullet.transform.rotation);
                bullet.transform.localScale = mBullet.transform.lossyScale;
                bullet.gameObject.SetActive(true);
                this.SendCommand<ShootCommand>();
            }
        }

        private void OnDestroy()
        {
            mGunInfo = null;
        }

        internal void Reload()
        {
            if(mGunInfo.BulletCountInGun.Value<mMaxBulletCount&&
                mGunInfo.BulletCountOutGun.Value>0&&
                mGunInfo.GunState.Value==GunState.Idle)
            {
                this.SendCommand<ReloadCommand>();
            }
        }
    }
}