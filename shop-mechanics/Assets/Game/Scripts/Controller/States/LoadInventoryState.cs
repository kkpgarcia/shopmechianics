using UnityEngine;

public class LoadInventoryState : GameState {
    public override void Enter() {
        CloseShop();

        MenuHeader.OnShopButtonClicked += OpenShop;

        Item[] items = PlayerData.Instance.Items.ToArray();

        foreach(Item i in items) {
            InventoryGrid.AddItem(i, OnSelectItem, false);
        }
    }

    public void OnSelectItem(Item item) {
        if(item == null)
            return;

        StoreManager.ItemSelected = item;

        this.Owner.ChangeState<SellSelectionState>();   
    }
}