using UnityEngine;

public class InitializeState : GameState {
    public override void Enter() {
        //Any game specific initializations are done here
        PlayerData.Instance.Initialize();

        MenuHeader.OnShopButtonClicked += OpenShop;
        MenuHeader.OnInventoryClicked += OpenInventory;

        MessageController.ClearOverlay();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        MenuHeader.OnShopButtonClicked -= OpenShop;
        MenuHeader.OnInventoryClicked -= OpenInventory;
    }

    private void OpenShop(object sender, object args) {
        CloseInventory();

        StoreData data = ResourceManager.Instance.Load<StoreData>("Store/Some Random Store");
        StoreManager.Initialize(data);
        
        string[] categories = StoreManager.GetCategories();
        
        for(int i = 0; i < categories.Length; i++)
            CategorySelector.AddCategory(i, categories[i], OnSelectCategory);
    }

    private void CloseShop() {
        CategorySelector.ClearCategory();
        ShopGrid.ClearGrid();
        StoreManager.Clear();
        MessageController.Clean();
    }

    private void OpenInventory(object sender, object args) {
        CloseShop();

        Item[] items = PlayerData.Instance.Items.ToArray();

        foreach(Item i in items) {
            InventoryGrid.AddItem(i, (item) => {
                Debug.Log("Selected item: " + item.Name);
            }, false);
        }
    }

    private void CloseInventory() {
        InventoryGrid.ClearGrid();
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
        MessageController.Initialize(
            string.Format("Would you like to purchase {0} for ${1}?", item.Name, item.Price),
            OnConfirm,
            OnCancel
        );

        StoreManager.ItemSelected = item;

        MessageController.Show();
    }

    private void OnConfirm() {
        Item currItem = StoreManager.ItemSelected;
        if(PlayerData.Instance.Currency - currItem.Price < 0) {
            //Insufficient funds
        } else {
            PlayerData.Instance.Currency -= currItem.Price;
            PlayerData.Instance.Items.Add(currItem);
            MessageController.ClearOverlay();
        }

    }

    private void OnCancel() {
        MessageController.ClearOverlay();
    }
}