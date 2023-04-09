namespace Interfaces
{
    public interface ILevelPanelSubject
    {
        void AddObserver(ILevelPanelObserver observer);
        void RemoveObserver(ILevelPanelObserver observer);
        void NotifyObservers(PlayerAction action);
    }
}