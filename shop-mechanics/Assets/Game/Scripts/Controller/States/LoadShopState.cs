using UnityEngine;

public class LoadShopState : GameState {
    public override void Enter() {
        CloseInventory();

        MenuHeader.OnInventoryClicked += OpenInventory;

        StoreData data = ResourceManager.Instance.Load<StoreData>("Store/Some Random Store");
        StoreManager.Initialize(data);
        
        string[] categories = StoreManager.GetCategories();
        
        for(int i = 0; i < categories.Length; i++)
            CategorySelector.AddCategory(i, categories[i], OnSelectCategory);
    }

     public void OnSelectCategory(string s) {
        ShopGrid.ClearGrid();

        Item[] items = StoreManager.GetItemFromCategory(s);

        if(items == null)
            return;

        foreach(Item item in items)
            ShopGrid.AddItem(item, OnSelectItem);
    }

    public void OnSelectItem(Item item) {
        if(item == null)
            return;

        StoreManager.ItemSelected = item;

        this.Owner.ChangeState<ItemSelectionState>();   
    }
}