using UnityEngine;

public class LoadInventoryState : GameState {
    public override void Enter() {
        CloseShop();

        MenuHeader.OnShopButtonClicked += OpenShop;

        Item[] items = PlayerData.Instance.Items.ToArray();

        foreach(Item i in items) {
            InventoryGrid.AddItem(i, (item) => {
                Debug.Log("Selected item: " + item.Name);
            }, false);
        }
    }
}