using FrameworkDesign;
using System.Collections.Generic;
using System.Linq;


namespace ShootingEditor2D
{
    public interface IGunSystem :ISystem
    {
        public GunInfo CurrentGun { get; }
        Queue<GunInfo> GunInfos { get; }
        void PickGun(string name, int bulletCountInGun, int bulletCountOutGun);
        void ShiftGun();
    }
    public class OnCurrentGunChanged 
    {
        public string Name { get; set; }
    }


    public class GunSystem : AbstractSystem, IGunSystem
    {
        private Queue<GunInfo> mGunInfos = new Queue<GunInfo>();
        public Queue<GunInfo> GunInfos
        {
            get { return mGunInfos; }
        }
        public GunInfo CurrentGun { get;} =new GunInfo()
        {
            BulletCountInGun = new BindableProperty<int>()
            {
                Value=3
            },

            BulletCountOutGun=new BindableProperty<int>()
            {
                Value=1
            },
            Name = new BindableProperty<string>() 
            {
                Value = "手枪"
            },
            GunState = new BindableProperty<GunState>() 
            {
                Value = GunState.Idle
            }
        };

        

        public void PickGun(string name, int bulletCountInGun, int bulletCountOutGun)
        {
            if(CurrentGun.Name.Value==name)
            {
                CurrentGun.BulletCountOutGun.Value += bulletCountInGun;
                CurrentGun.BulletCountOutGun.Value += bulletCountOutGun;
            }else if(mGunInfos.Any(info => info.Name.Value==name))
            {
                var gunInfo = mGunInfos.First(info => info.Name.Value == name);
                gunInfo.BulletCountOutGun.Value += bulletCountInGun;
                gunInfo.BulletCountOutGun.Value += bulletCountOutGun;
            }else
            {
                EnqueueCurrentGun(name, bulletCountInGun, bulletCountOutGun);
            }
        }

        public void ShiftGun()
        {
            if (mGunInfos.Count > 0)
            {
                var nextGunInfo = mGunInfos.Dequeue();

                EnqueueCurrentGun(nextGunInfo.Name.Value, nextGunInfo.BulletCountInGun.Value, nextGunInfo.BulletCountOutGun.Value);

            }
        }

        void EnqueueCurrentGun(string nextGunName,int nextGunBulletCountInGun, int nextGunBulletCountOutGun)
        {
            var currentGunInfo = new GunInfo
            {
                Name = new BindableProperty<string>()
                {
                    Value = CurrentGun.Name.Value
                },
                BulletCountInGun = new BindableProperty<int>()
                {
                    Value = CurrentGun.BulletCountInGun.Value
                },
                BulletCountOutGun = new BindableProperty<int>()
                {
                    Value = CurrentGun.BulletCountOutGun.Value
                },
                GunState = new BindableProperty<GunState>()
                {
                    Value = CurrentGun.GunState.Value
                }
            };

            mGunInfos.Enqueue(currentGunInfo);

            CurrentGun.Name.Value = nextGunName;
            CurrentGun.BulletCountInGun.Value = nextGunBulletCountInGun;
            CurrentGun.BulletCountOutGun.Value = nextGunBulletCountOutGun;
            CurrentGun.GunState.Value = GunState.Idle;

            this.SendEvent(new OnCurrentGunChanged()
            {
                Name = nextGunName
            });
        }
        protected override void OnInit()
        {
            
        }
    }
}