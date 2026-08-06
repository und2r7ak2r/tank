namespace FrameworkDesign
{
    public interface IController : IBelongToArchitecture, ICanGetSystem, ICanGetModel, ICanSendCommand,
        ICanSendEvent,ICanRegisterEvent,ICanSendQuery
    {

    }
}
