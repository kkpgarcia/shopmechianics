using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Common.ObjectPool;

public class ItemGrid : MonoBehaviour {
    //[SerializeField]
    //private ItemEntry m_Prefab;
    [SerializeField]
    private string m_Prefab = "BTN_ItemEntry";

    [SerializeField]
    private Transform m_Parent;
    
    private List<ItemEntry> m_ItemEntries = new List<ItemEntry>();
    
    public void AddItem(Item item, UnityAction<Item> callback) {
        ItemEntry newEntry = CreateItemEntry();
        newEntry.Initialize(item, callback);
        newEntry.transform.SetParent(m_Parent);
        this.m_ItemEntries.Add(newEntry);
    }

    public void ClearGrid() {
        if(m_ItemEntries.Count == 0)
            return;
            
        foreach(ItemEntry entry in m_ItemEntries) {
            ObjectPool.Instance.PoolObject(entry.gameObject);
        }

        m_ItemEntries.Clear();
    }

    private ItemEntry CreateItemEntry() {
        return ObjectPool.Instance.GetObjectForType(m_Prefab, true).GetComponent<ItemEntry>();
    }
}