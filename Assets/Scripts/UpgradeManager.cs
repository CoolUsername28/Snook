using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance { get; private set; }

    public static Action<UpgradeSO> upgradeAdded;
    [field: SerializeField] public List<UpgradeSO> avilableUpgrades { get; private set; }
    [field: SerializeField] public List<UpgradeSO> activeUpgrades { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        avilableUpgrades = new List<UpgradeSO>();
        avilableUpgrades = RecourcesManager.upgrades.ToList<UpgradeSO>();

        activeUpgrades = new List<UpgradeSO>();
    }

    public void AddUpgrade(UpgradeSO upgrade)
    {
        avilableUpgrades.Remove(upgrade);
        activeUpgrades.Add(upgrade);
        upgradeAdded?.Invoke(upgrade);
        GameUI.Instance.DisplayUpgrades(upgrade);
    }

    public UpgradeSO AddRandomUpgradeToShop()
    {
        UpgradeSO upgrade = avilableUpgrades.ElementAt(UnityEngine.Random.Range(0, avilableUpgrades.Count));
        avilableUpgrades.Remove(upgrade);
        return upgrade;
    }
    
    public void ReAddUpgradeToList(UpgradeSO upgrade)
    {
        avilableUpgrades.Add(upgrade);
    }

    public bool hasUpgrade(string upgradeName)
    {
        foreach(UpgradeSO upgrade in activeUpgrades)
        {
            if (upgrade.upgradeName == upgradeName) return true;
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
