using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IGameStateListener
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private Transform buttonsParent;
    [SerializeField] private ShopButton shopbuttonPrefab;
  

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.SHOP:
                SetUp();
                break;
        }
    }

    private void SetUp()
    {
        List<GameObject> toDestroy = new List<GameObject>();

        for (int i = 0; i < buttonsParent.childCount; i++)
        {
            ShopButton container = buttonsParent.GetChild(i).GetComponent<ShopButton>();

            toDestroy.Add(container.gameObject);
        }
        while (toDestroy.Count > 0)
        {
            Transform t = toDestroy[0].transform;
            t.SetParent(null);

            Destroy(t.gameObject);
            toDestroy.RemoveAt(0);
        }
        int buttonsToAdd = 3;
        for (int i = 0; i < buttonsToAdd; i++)
        {
            ShopButton shopButtonInstance = Instantiate(shopbuttonPrefab, buttonsParent);

            UpgradeSO upgradeSO = RecourcesManager.GetRandomUpgrade();

            shopButtonInstance.Configure(upgradeSO);
        }
    }

 

    private void Awake()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() => GameManager.Instance.SetGameState(GameState.GAME));
        
    }
    private void Update()
    {
        moneyText.text = GameManager.Instance.money.ToString();
    }
}
