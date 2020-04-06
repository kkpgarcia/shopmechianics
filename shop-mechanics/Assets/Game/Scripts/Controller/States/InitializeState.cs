using UnityEngine;

public class InitializeState : GameState {
    public override void Enter() {
        //Any game specific initializations are done here
        StoreData data = ResourceManager.Instance.Load<StoreData>("Store/Some Random Store");
        
        StoreManager.Initialize(data);
        
        string[] categories = StoreManager.GetCategories();
        
        for(int i = 0; i < categories.Length; i++)
            CategorySelector.AddCategory(i, categories[i], OnSelectCategory);
    }

    public void OnSelectCategory(string s) {
        ItemGrid.ClearGrid();

        Item[] items = StoreManager.GetItemFromCategory(s);

        if(items == null)
            return;

        foreach(Item item in items)
            ItemGrid.AddItem(item, OnSelectItem);
    }

    public void OnSelectItem(Item item) {
        Debug.Log("Selected Item: " + item.Name);
    }

    public void LoadShop() {}
}