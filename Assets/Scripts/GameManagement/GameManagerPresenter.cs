using UnityEngine;
using UnityEngine.UI;

public class GameManagerPresenter : MonoBehaviour
{
    public GameManager model;
    public PlayerCameraPositioner cameraPositioner;
    public PlayerInput playerInput;

    [Header("Menu")]
    public MenuManager menuManager;
    public int mainMenuIndex = 0;
    public int resultsMenuIndex = 1;
    public MenuPlayButtonText playButton;

    [Header("Player UI")]
    public PlayerUIPresenter playerUI;
    public BubbleText pointsText;
    public GameRecordsResultPresenter recordsPresenter;

    [Header("Buttons")]
    public Button[] playButtons;
    public Button quitButton;

    [Header("Audio")]
    public SmoothParameterChanger audioCrossFadeParameterChanger;

    void Start()
    {
        model.OnPlayStateChange += HandlePlayStateChange;
        model.OnPauseChange += HandlePlayStateChange;
        model.OnKillCountChanged += HandleKillCountChange;

        foreach(Button playButton in playButtons)
        {
            playButton.onClick.AddListener(PlayOrUnpause);
        }

        quitButton.onClick.AddListener(model.QuitGame);

        playerInput.OnPause += PauseGame;

        HandlePlayStateChange();
    }

    private void OnDestroy()
    {
        model.OnPlayStateChange -= HandlePlayStateChange;
        model.OnPauseChange -= HandlePlayStateChange;
        model.OnKillCountChanged -= HandleKillCountChange;

        foreach (Button playButton in playButtons)
        {
            playButton.onClick.RemoveListener(PlayOrUnpause);
        }
        quitButton.onClick.RemoveListener(model.QuitGame);

        playerInput.OnPause -= PauseGame;
    }

    private void PlayOrUnpause()
    {
        if (model.isPlaying)
        {
            model.UnpauseGame();
            return;
        }

        model.StartGame();
    }

    private void HandlePlayStateChange()
    {
        UpdateCamera();
        UpdateMainPanel();
        UpdatePlayerUI();
        UpdatePlayerInput();
        UpdateMusic();

        playButton.UpdateRecord(model.gameRecords.points);
    }

    private void UpdateCamera()
    {
        if (model.isPlaying)
        {
            cameraPositioner.ReturnToOrigin();
        }
        else
        {
            cameraPositioner.FocusOnPlayer();
        }
    }

    private void UpdateMainPanel()
    {
        menuManager.ShowHide(!model.isPlaying || model.isPaused);
        if (model.player.isDead)
        {
            menuManager.ShowPanel(resultsMenuIndex);
            recordsPresenter.ShowRecords(model.killCount, model.gameRecords);
        }
    }

    private void UpdateMusic()
    {
        if (model.isPlaying && !model.isPaused)
        {
            audioCrossFadeParameterChanger.ChangeToSecondValue();
        }
        else
        {
            audioCrossFadeParameterChanger.ChangeToFirstValue();
        }
    }

    private void UpdatePlayerUI()
    {
        playerUI.ShowHide(model.isPlaying && !model.isPaused);
    }

    private void UpdatePlayerInput()
    {
        playerInput.SetEnable(model.isPlaying && !model.isPaused);
    }

    private void HandleKillCountChange()
    {
        pointsText.SetText(model.killCount.ToString());
    }

    private void PauseGame()
    {
        model.PauseGame();
        menuManager.ShowPanel(mainMenuIndex);
    }
}
