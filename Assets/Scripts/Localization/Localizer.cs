using System;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public static Localizer instance;

    public LocalizationContainer[] localizationContainers;
    public LocalizationContainer defaultContainer;

    private LocalizationContainer _currentContainer;

    public LocalizationContainer currentContainer
    {
        get
        {
            if (_currentContainer == null)
            {
                currentContainer = defaultContainer;
            }
            return _currentContainer;
        }
        set
        {
            _currentContainer = value;
            OnLanguageChanged?.Invoke();
        }
    }

    public static event Action OnLanguageChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (currentContainer != null)
        {
            
        }
    }

    public static string Localize(string key)
    {
        if(instance == null)
        {
            return key;
        }

        return instance.currentContainer.GetWord(key);
    }
}
