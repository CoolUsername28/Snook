using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour, IGameStateListener
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Portal portal;
    [SerializeField] private Foods secondFood;
    [SerializeField] private Spikes[] spikes;
    [SerializeField] private AudioClip eatSfx;
    [SerializeField] private AudioClip gameOverSfx;
     
    float spawnTimer;
    private float spawnTime = 5f;
    string highScoreKey = "HighScore";
    private int matchScore;
    private int highScore;
    private int spikeCount;

    public int score = 0;
    public int reqScore;
    public int money;
    private bool gameIsRuning = false;

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
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        UpgradeManager.upgradeAdded += UpgradeAddedCallback;
    }
    private void OnDestroy()
    {
        UpgradeManager.upgradeAdded -= UpgradeAddedCallback;
    }

    private void UpgradeAddedCallback(UpgradeSO so)
    {
         if(so.upgradeName == "Orchard")
        {
            secondFood.gameObject.SetActive(true);
            secondFood.RandomizePosition();
        }
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();
        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.GameStateChangedCallback(gameState);

    }
    private void SpawnSpikes()
    {
     
        
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnTime)
        {
            spawnTimer = 0f;
            for (int i = 0; i < spikes.Length; i++)
            {
                if (i == spikeCount)
                {
                    spikes[i].Spawn();
                    break;
                }
                
            }
            spikeCount++;
        }
    }
    private void DespawnSpikes()
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].DeSpawn();
        }
        spikeCount = 0;
    }
    public void FoodCollectedCallback()
    {
        AudioManager.Instance.PlaySoundClip(eatSfx, transform, 1f);
        score++;
        matchScore++;
        if(score >= reqScore)
        {
            portal.Spawn();
        }
    }

    private void Update()
    {
        if (!gameIsRuning) return;
        SpawnSpikes();
    }

    public void NextLevelCallback()
    {
        gameIsRuning = false;
        DespawnSpikes();

       
        SetGameState(GameState.SHOP);
        if (UpgradeManager.Instance.hasUpgrade("Money Bag"))
        {
            money += reqScore * 2;
            money += (score - reqScore) * 3;
        }
        else 
        {
            money += reqScore;
            money += (score - reqScore) * 2;

        }
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
        matchScore = 0;
        gameIsRuning = false;
        spawnTime = 5f;
        DespawnSpikes();
        portal.Hide();
        secondFood.gameObject.SetActive(false);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAMEOVER:
                AudioManager.Instance.PlaySoundClip(gameOverSfx, transform, 1f);
                GameOverUI.Instance.DisplayScoreText(matchScore);
                GameOverUI.Instance.newHighScore(matchScore>highScore);
                if (matchScore > highScore)
                {
                    PlayerPrefs.SetInt(highScoreKey, matchScore);
                    PlayerPrefs.Save();
                    highScore = PlayerPrefs.GetInt(highScoreKey, 0);
                }
                ResetValues();
                UpgradeManager.Instance.ResetUpgrades();
                break;
            case GameState.GAME:
                gameIsRuning = true;
                if (UpgradeManager.Instance.hasUpgrade("Shield")) spawnTime = 8f;
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