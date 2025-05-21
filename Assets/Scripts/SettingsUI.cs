using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    private bool isActive = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ToggleSettings();
    }
    public void ToggleSettings()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }

}
