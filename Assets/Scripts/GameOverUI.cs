using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour, IGameStateListener
{

    public static GameOverUI Instance { get; private set; }
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI splashTextUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private AudioClip gameStartFx;
    public string[] splashTexts;

    
    private void splashText()
    {
        splashTextUI.text = splashTexts[Random.Range(0, splashTexts.Length)];
    }
    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => Retry());

        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.MENU));
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAMEOVER:
                splashText();
                break;
        }
    }

    private void Retry()
    {
        AudioManager.Instance.PlaySoundClip(gameStartFx, transform, 1f);
        GameManager.Instance.SetGameState(GameState.GAME);
    }
    public void DisplayScoreText(int score)
    {
        scoreText.text = score.ToString();
        
    }
    public void newHighScore(bool b)
    {
        highscoreText.gameObject.SetActive(b);
    }
}
