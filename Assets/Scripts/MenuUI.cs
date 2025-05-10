using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => StartGame());

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(() => ExitGame());
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
