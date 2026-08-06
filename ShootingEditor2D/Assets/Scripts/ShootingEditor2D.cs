using FrameworkDesign;
namespace ShootingEditor2D
{

    public class ShootingEditor2D : Architecture<ShootingEditor2D>
    {
        protected override void init()
        {
            this.RegisterModel<IGunConfigModel>(new GunConfigModel()); 
            this.RegisterModel<IPlayerModel>(new PlayerModel());
            this.RegisterSystem<IStatSystem>(new StatSystem());
            this.RegisterSystem<IGunSystem>(new GunSystem());
            this.RegisterSystem<ITimeSystem>(new TimeSystem());

        }
    }
}