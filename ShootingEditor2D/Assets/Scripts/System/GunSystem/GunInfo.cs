using FrameworkDesign;

namespace ShootingEditor2D
{
    public enum GunState
    {
        Idle,
        Shooting,
        ReLoad,
        EmptyBullet,
        CoolDown
    }
    public class GunInfo
    {

        public BindableProperty<int> BulletCountInGun;

        public BindableProperty<string> Name;

        public BindableProperty<GunState> GunState;

        public BindableProperty<int> BulletCountOutGun;


    }
}