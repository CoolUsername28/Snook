using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descripitonText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button button;

    public UpgradeSO currentUpgrade;

    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => BuyUpgrade());
    }
    public void Configure(UpgradeSO upgradeSO)
    {
        image.sprite = upgradeSO.image;
        nameText.text = upgradeSO.upgradeName;
        descripitonText.text = upgradeSO.description;
        priceText.text = upgradeSO.price.ToString();

        currentUpgrade = upgradeSO;
    }

    private void BuyUpgrade()
    {
        int price = currentUpgrade.price;
        if (!GameManager.Instance.TrySpendMoney(price)) return;

        GameManager.Instance.SpendMoney(price);
        UpgradeManager.Instance.AddUpgrade(currentUpgrade);
        transform.SetParent(null);
        Destroy(this.gameObject);
    }
}
