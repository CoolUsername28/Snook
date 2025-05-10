using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IGameStateListener
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Portal portal;

    public int score = 0;
    public int reqScore;
    public int money;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        SetGameState(GameState.MENU);
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();
        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.GameStateChangedCallback(gameState);

    }

    public void FoodCollectedCallback()
    {
        score++;
        if(score >= reqScore)
        {
            portal.Spawn();
        }
    }



    public void NextLevelCallback()
    {
        SetGameState(GameState.SHOP);
        if(UpgradeManager.Instance.hasUpgrade("Money Bag"))
        {
            money += (score - reqScore) * 2;
        }
        else money += (score - reqScore);
        if (UpgradeManager.Instance.hasUpgrade("Piggy Bank")) money += 5;

    }

    public bool TrySpendMoney(int ammount)
    {
        return money >= ammount;
    }
    public void SpendMoney(int ammount)
    {
        money -= ammount;
    }

    private void ResetValues()
    {
        score = 0;
        reqScore = 0;
        money = 0;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAMEOVER:
                ResetValues();
                UpgradeManager.Instance.ResetUpgrades();
                break;
            case GameState.GAME:
                score = 0;
                reqScore += 5;
                break;
        }
    }
}
public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}