using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StoreManager : MonoBehaviour {
    private Dictionary<string, Item[]> m_StoreMap = new Dictionary<string, Item[]>();
    private StoreData m_Model;
    public Item ItemSelected = null;
    
    public void Initialize(StoreData data) {
        m_Model = data;

        foreach(StoreCategoryData category in m_Model.Categories) {
            List<Item> itemData = new List<Item>();

            foreach(ItemData id in category.ItemList) {
                itemData.Add(id.Item);
            }

            m_StoreMap.Add(category.name, itemData.ToArray());
        }
    }

    public void Clear() {
        m_StoreMap.Clear();
        ItemSelected = null;
        m_Model = null;
    }

    public string[] GetCategories() {
        return m_StoreMap.Keys.ToArray();
    }

    public Item[] GetItemFromCategory(string category) {
        if(!m_StoreMap.ContainsKey(category))
            return null;
    
        return m_StoreMap[category].ToArray();
    }
}