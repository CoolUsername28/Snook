using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainerUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Configure(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
