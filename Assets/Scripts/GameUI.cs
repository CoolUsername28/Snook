using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Transform upgradeDisplay;
    [SerializeField] private UpgradeContainerUI upgradeContainer;
   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        UpdateHearts(1);
    }
    private void Update()
    {
        scoreText.text = GameManager.Instance.score.ToString() + " / " + GameManager.Instance.reqScore.ToString();
      
    }
    public void DisplayUpgrades(UpgradeSO upgrade)
    {
             UpgradeContainerUI upgrade1 = Instantiate(upgradeContainer, upgradeDisplay);
            upgrade1.Configure(upgrade.image);
    }
    public void ClearUpgradeDisplay()
    {
        upgradeDisplay.Clear();
    }
    public void UpdateHearts(int hearts)
    {
        heartContainer.Clear();
        for (int i = 0; i < hearts; i++)
        {
            Instantiate(heart, heartContainer);
        }
    }
}
