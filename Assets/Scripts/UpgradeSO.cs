using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "Scriptable Objects/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    [field: SerializeField] public string upgradeName { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public Sprite image { get; private set; }
    [field: SerializeField] public int price { get; private set; }




}
