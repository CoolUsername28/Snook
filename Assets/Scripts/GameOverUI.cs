using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour, IGameStateListener
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI splashText;
    public string[] plashTexts;

    private void plashText()
    {
        splashText.text = plashTexts[Random.Range(0, plashTexts.Length)];
    }
    private void Awake()
    {
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
}
