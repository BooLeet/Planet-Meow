using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public class MenuPanel
    {
        public GameObject panel;
        public Button[] accessorButtons;
    }

    public MenuPanel[] menuPanels;
    public bool isShown
    {
        get;
        private set;
    } = true;

    void Awake()
    {
        foreach (MenuPanel menuPanel in menuPanels)
        {
            foreach (Button button in menuPanel.accessorButtons)
            {
                MenuPanelAccessorButton accessorButton = button.gameObject.AddComponent<MenuPanelAccessorButton>();
                accessorButton.Setup(this, menuPanel, button);
            }
        }

        ShowPanel(0);
    }

    public void ShowPanel(MenuPanel targetPanel)
    {
        foreach (MenuPanel menuPanel in menuPanels)
        {
            menuPanel.panel.SetActive(menuPanel == targetPanel);
        }
    }

    public void ShowPanel(int index)
    {
        ShowPanel(menuPanels[index]);
    }

    public void Show()
    {
        ShowHide(true);
    }

    public void Hide()
    {
        ShowHide(false);
    }

    public void ShowHide(bool show)
    {
        if (isShown == show)
        {
            return;
        }

        isShown = show;
        gameObject.SetActive(isShown);
    }
}
