using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class RecourcesManager 
{
    const string upgradesDataPath = "Data/Upgrades";

   private static UpgradeSO[] upgradeSOs;
  public static UpgradeSO[] upgrades
    {
        get
        {
            if(upgradeSOs == null)
            {
                upgradeSOs = Resources.LoadAll<UpgradeSO>(upgradesDataPath);
            }
            return upgradeSOs;
        }
        private set { }
    }

    public static UpgradeSO GetRandomUpgrade()
    {
        return upgrades[Random.Range(0, upgrades.Length)];
    }
}
