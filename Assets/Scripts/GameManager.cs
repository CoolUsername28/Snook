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
        money += (score - reqScore);
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAMEOVER:
                score = 0;
                reqScore = 0;
                money = 0;
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