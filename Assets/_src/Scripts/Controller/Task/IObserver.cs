namespace _src.Scripts.Controller.Task
{
    public interface IObserver
    {
        delegate void Trigger(IObserver sender, object param = default);
        event Trigger OnTriggered;
        
        void NotifyObserver(object param = default);
    }
}