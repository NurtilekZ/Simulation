namespace _src.Scripts.Controller.Services
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}