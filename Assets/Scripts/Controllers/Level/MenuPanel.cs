using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Level
{
    public class MenuPanel : MonoBehaviour, IMenuSubject
    {
        private List<IMenuObserver> observers = new List<IMenuObserver>();

        public void AddObserver(IMenuObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IMenuObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(MenuOption option)
        {
            foreach (IMenuObserver observer in observers)
            {
                observer.OnMenuOptionSelected(option);
            }
        }

        public void OnPlayButtonClicked()
        {
            NotifyObservers(MenuOption.Play);
            SceneManager.LoadScene("MainScene");
        }

        public void OnOptionsButtonClicked()
        {
            NotifyObservers(MenuOption.Options);
        }

        public void OnCreditsButtonClicked()
        {
            NotifyObservers(MenuOption.Credits);
        }
    }
}