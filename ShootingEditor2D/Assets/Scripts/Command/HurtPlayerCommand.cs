using FrameworkDesign;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

namespace ShootingEditor2D
{
    public class HurtPlayerCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playmodel=this.GetModel<IPlayerModel>();
            playmodel.HP.Value--;
            if(playmodel.HP.Value <= 0 )
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

}