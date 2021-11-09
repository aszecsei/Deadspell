using Entitas;

namespace Deadspell
{
    public interface IEventListener
    {
        void RegisterListeners(IEntity entity);
    }
}
