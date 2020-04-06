using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Common.ObjectPool;
using System.Linq;

public class ItemGrid : MonoBehaviour {
    //[SerializeField]
    //private ItemEntry m_Prefab;
    [SerializeField]
    private string m_Prefab = "BTN_ItemEntry";

    [SerializeField]
    private Transform m_Parent;
    
    private List<ItemEntry> m_ItemEntries = new List<ItemEntry>();
    
    public void AddItem(Item item, UnityAction<Item> callback, bool showPrice = true) {
        ItemEntry newEntry = CreateItemEntry();
        newEntry.Initialize(item, callback, showPrice);
        newEntry.transform.SetParent(m_Parent);
        newEntry.transform.localScale = Vector3.one;
        this.m_ItemEntries.Add(newEntry);
    }

    public void Remove(Item item) {
        ItemEntry entry = m_ItemEntries.FirstOrDefault(x => x.Compare(item));

        if(entry == null)
            return;

        m_ItemEntries.Remove(entry);
        ObjectPool.Instance.PoolObject(entry.gameObject);
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
        return ObjectPool.Instance.GetObjectForType(m_Prefab, false).GetComponent<ItemEntry>();
    }
}