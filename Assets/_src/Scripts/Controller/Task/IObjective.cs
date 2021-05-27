namespace _src.Scripts.Controller.Task
{
    public interface IObjective
    {
        delegate void Complete(IObjective sender, object param = default);
        event Complete OnCompleteEvent;
        
        void CompleteObjective(object param = default);
    }
}