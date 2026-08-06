using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public class AddBulletCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gunSystem = this.GetSystem<IGunSystem>();
            var gunConfigModel = this.GetModel<IGunConfigModel>();
            AddBullet(gunSystem.CurrentGun, gunConfigModel);
            foreach(var gunInfo in gunSystem.GunInfos)
            {
                AddBullet(gunInfo, gunConfigModel);
            }
        }
        void AddBullet(GunInfo guninfo, IGunConfigModel gunConfigModel )
        {
            var gunConfigItem =gunConfigModel.GetItemByName(guninfo.Name.Value);
            if(!gunConfigItem.NeedBullet)
            {

            }else
            {
                guninfo.BulletCountOutGun.Value += gunConfigItem.BulletMaxCount;
            }
        }
    }
}