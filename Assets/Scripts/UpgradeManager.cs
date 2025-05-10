using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance { get; private set; }
    [field: SerializeField] public List<UpgradeSO> avilableUpgrades { get; private set; }
    [field: SerializeField] public List<string> activeUpgrades { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        avilableUpgrades = new List<UpgradeSO>();
        avilableUpgrades = RecourcesManager.upgrades.ToList<UpgradeSO>();

        activeUpgrades = new List<string>();
    }

    public void AddUpgrade(UpgradeSO upgrade)
    {
        avilableUpgrades.Remove(upgrade);
        activeUpgrades.Add(upgrade.upgradeName);
    }

    public UpgradeSO AddRandomUpgradeToShop()
    {
        UpgradeSO upgrade = avilableUpgrades.ElementAt(Random.Range(0, avilableUpgrades.Count));
        avilableUpgrades.Remove(upgrade);
        return upgrade;
    }
    
    public void ReAddUpgradeToList(UpgradeSO upgrade)
    {
        avilableUpgrades.Add(upgrade);
    }

    public bool hasUpgrade(string upgradeName)
    {
        foreach(string name in activeUpgrades)
        {
            if (name == upgradeName) return true;
        }
        return false;
    }

    public void ResetUpgrades()
    {
        avilableUpgrades.Clear();
        activeUpgrades.Clear();

        avilableUpgrades = RecourcesManager.upgrades.ToList<UpgradeSO>();
    }


}
