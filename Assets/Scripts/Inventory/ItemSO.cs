using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;

    public StatToChange statToChange = new();

    public int amountToChangeStat;

    public enum StatToChange
    {
        none,
        health,
        stamina
    };
}
