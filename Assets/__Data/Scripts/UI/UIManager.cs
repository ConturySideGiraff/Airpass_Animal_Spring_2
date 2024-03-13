using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<UIPage> _pages = new List<UIPage>();

    public void OffAll()
    {
        foreach (UIPage page in _pages)
        {
            if (page.GetType() == typeof(UIFade))
            {
                continue;
            }

            page.Off();
        }
    }

    public T Get<T>() where T : UIPage
    {
        return _pages.FirstOrDefault(p => p.GetType() == typeof(T)) as T;
    }
    
    public T On<T>() where T : UIPage
    {
        return _pages.FirstOrDefault(p => p.GetType() == typeof(T))?.On() as T;
    }
    
    public T Off<T>() where T : UIPage
    {
        return _pages.FirstOrDefault(p => p.GetType() == typeof(T))?.Off() as T;
    }
}
