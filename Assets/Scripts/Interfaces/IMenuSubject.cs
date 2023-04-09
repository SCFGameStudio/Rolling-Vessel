using UnityEngine;

namespace Interfaces
{
    public interface IMenuSubject
    {
        void AddObserver(IMenuObserver observer);
        void RemoveObserver(IMenuObserver observer);
        void NotifyObservers(MenuOption option);
    }
}