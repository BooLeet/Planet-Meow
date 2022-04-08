using UnityEngine;
using UnityEngine.UI;

public class MenuPanelAccessorButton : MonoBehaviour
{
    private MenuManager.MenuPanel menuPanel;
    private MenuManager menuManager;
    private Button button;

    public void Setup(MenuManager menuManager, MenuManager.MenuPanel menuPanel, Button button)
    {
        this.menuManager = menuManager;
        this.menuPanel = menuPanel;
        this.button = button;

        button.onClick.AddListener(ShowPanel);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(ShowPanel);
    }

    private void ShowPanel()
    {
        menuManager.ShowPanel(menuPanel);
    }
}
