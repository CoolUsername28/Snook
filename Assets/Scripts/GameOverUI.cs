using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour, IGameStateListener
{

    public static GameOverUI Instance { get; private set; }
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI splashText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    public string[] plashTexts;

    
    private void plashText()
    {
        splashText.text = plashTexts[Random.Range(0, plashTexts.Length)];
    }
    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.GAME));

        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.MENU));
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAMEOVER:
                plashText();
                break;
        }
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
