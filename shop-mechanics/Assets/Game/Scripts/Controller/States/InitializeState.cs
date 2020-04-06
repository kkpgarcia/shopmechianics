using UnityEngine;

public class InitializeState : GameState {
    public override void Enter() {
        //Any game specific initializations are done here
        StoreData data = ResourceManager.Instance.Load<StoreData>("Store/Some Random Store");
        
        StoreManager.Initialize(data);
        
        string[] categories = StoreManager.GetCategories();
        
        for(int i = 0; i < categories.Length; i++)
            CategorySelector.AddCategory(i, categories[i], OnSelectCategory);
    
        MessageController.ClearOverlay();
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
        MessageController.Initialize(
            string.Format("Would you like to purchase {0} for ${1}?", item.Name, item.Price),
            OnConfirm,
            OnCancel
        );

        MessageController.Show();
    }

    private void OnConfirm() {
        MessageController.ClearOverlay();
    }

    private void OnCancel() {
        MessageController.ClearOverlay();
    }

    public void LoadShop() {}
}