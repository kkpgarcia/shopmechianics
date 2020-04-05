using UnityEngine;

[CreateAssetMenu(menuName = "Game Models/Item Data")]
public class ItemData : ScriptableObject {
    public string Name;
    public int Price;
    public ItemType Type;
}