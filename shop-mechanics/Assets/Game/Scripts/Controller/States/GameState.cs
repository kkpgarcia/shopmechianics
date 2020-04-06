using UnityEngine;

public class GameState : State {
    protected GameBootstrap Owner;
    protected CategorySelector CategorySelector { get { return Owner.CategorySelector; } }
    protected StoreManager StoreManager { get { return Owner.StoreManager; } }
    protected ItemGrid ShopGrid { get { return Owner.ShopGrid; } }
    protected ItemGrid InventoryGrid { get { return Owner.InventoryGrid; } }
    protected MessageController MessageController { get { return Owner.MessageController; } }
    protected MenuHeader MenuHeader { get { return Owner.MenuHeader; } }

    private void Awake() {
        this.Owner = this.GetComponent<GameBootstrap>();
    }

    protected void OpenShop(object sender, object args) {
        CloseInventory();
        
        this.Owner.ChangeState<LoadShopState>();
    }

     protected void OpenInventory(object sender, object args) {
        CloseShop();
        
        this.Owner.ChangeState<LoadInventoryState>();
    }

    protected void CloseShop() {
        MenuHeader.OnInventoryClicked -= OpenInventory;
        CategorySelector.ClearCategory();
        ShopGrid.ClearGrid();
        StoreManager.Clear();
        MessageController.Clean();
    }

    protected void CloseInventory() {
        MenuHeader.OnShopButtonClicked -= OpenShop;
        InventoryGrid.ClearGrid();
    }
}