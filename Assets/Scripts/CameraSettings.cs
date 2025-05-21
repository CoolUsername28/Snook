using BrewedInk.CRT;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private CRTCameraBehaviour crt;
    [SerializeField] private Image checkmark;
    private bool crtOn = true;
    private string crtKey = "CRTKey";

    private void Awake()
    {
        int i = PlayerPrefs.GetInt(crtKey, 0);
        if (i == 0)
        {
            crtOn = true;
            checkmark.enabled = true;
        }
        else
        { 
            crtOn = false;
            checkmark.enabled = false;
        }
        crt.enabled = crtOn;
    }

    public void ToggleCRT()
    {
        crtOn = !crtOn;
        crt.enabled = crtOn;
        if(crtOn == true)
        {
            checkmark.enabled = true;
            PlayerPrefs.SetInt(crtKey, 0);
            PlayerPrefs.Save();
        }
        else
        {
            checkmark.enabled = false;
            PlayerPrefs.SetInt(crtKey, 1);
            PlayerPrefs.Save();
        }
    }
}
