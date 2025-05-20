using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => StartGame());

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(() => ExitGame());
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        scoreText.text = "High Score: " + GameManager.Instance.GetHighScore().ToString();
    }

    private void StartGame()
    {
        GameManager.Instance.SetGameState(GameState.GAME);
    }
    
    private void ExitGame()
    {
        Application.Quit();
    }
}
