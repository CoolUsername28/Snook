using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descripitonText;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Configure(UpgradeSO upgradeSO)
    {
        image.sprite = upgradeSO.image;
        nameText.text = upgradeSO.upgradeName;
        descripitonText.text = upgradeSO.description;
        priceText.text = upgradeSO.price.ToString();
    }
}
