using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioClip gameStartSFX;
    private bool showingSettings;

    

    private void Awake()
    {
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => StartGame());

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(() => ExitGame());

        settingsButton.onClick.RemoveAllListeners();
        settingsButton.onClick.AddListener(() => ToggleSettingsFromMenu());

    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        scoreText.text = "High Score: " + GameManager.Instance.GetHighScore().ToString();
    }

    private void StartGame()
    {
        AudioManager.Instance.PlaySoundClip(gameStartSFX, transform, 1f);
        GameManager.Instance.SetGameState(GameState.GAME);
    }
    
    private void ExitGame()
    {
        Application.Quit();
    }

    private void ToggleSettingsFromMenu()
    {
        SettingsUI.Instance.ToggleSettings();
    }
}
