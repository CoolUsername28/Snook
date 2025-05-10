using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.GAME));
    }
}
