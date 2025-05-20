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
    [SerializeField] private AudioClip audioClip;

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

        AudioManager.Instance.PlaySoundClip(audioClip, transform, 1f);
        GameManager.Instance.SpendMoney(price);
        UpgradeManager.Instance.AddUpgrade(currentUpgrade);
        transform.SetParent(null);
        Destroy(this.gameObject);
    }
}
