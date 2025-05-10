using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.GAME));

        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.MENU));
    }
}
