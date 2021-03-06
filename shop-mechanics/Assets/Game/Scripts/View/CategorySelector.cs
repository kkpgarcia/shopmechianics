using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

using Common.ObjectPool;

public class CategorySelector : MonoBehaviour {
    
    [SerializeField]
    private string m_Prefab = "BTN_Category";
    [SerializeField]
    private Transform m_CategoryParent;

    private List<Category> m_Categories = new List<Category>();

    public const string OnSelectNotification = "CategorySelector.OnSelect";
    public const string OnClearSelectionNotification = "CategorySelector.OnClearSelection";

    private int m_CurrentSelected = -1;

    private void OnEnable() {
        this.AddObserver(OnSelect, OnSelectNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(OnSelect, OnSelectNotification);
    }

    public void AddCategory(int index, string category, UnityAction<string> callback) {
        Category newCategory = CreateCategory();
        newCategory.Initialize(index, category, callback);
        newCategory.transform.SetParent(m_CategoryParent);
        m_Categories.Add(newCategory);
    }

    public void ClearCategory() {
        foreach(Category category in m_Categories)
            ObjectPool.Instance.PoolObject(category.gameObject);

        m_Categories.Clear();
    }

    private Category CreateCategory() {
        return ObjectPool.Instance.GetObjectForType(m_Prefab, false).GetComponent<Category>();
    }

    private void OnSelect(object sender, object args) {
        InfoEventArgs<int> info = (InfoEventArgs<int>)args;
        if(m_CurrentSelected != -1)
            m_Categories[m_CurrentSelected].UnselectCategory();
            
        m_CurrentSelected = info.Arg0;
    }

    private void OnClearSelection(object sender, object args) {
        if(m_CurrentSelected == -1)
            return;

        m_Categories[m_CurrentSelected].UnselectCategory(false);
        m_CurrentSelected = -1;
    }
}