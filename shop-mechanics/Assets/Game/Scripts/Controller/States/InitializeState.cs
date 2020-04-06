using UnityEngine;

public class InitializeState : GameState {
    public override void Enter() {
        //Any game specific initializations are done here
        PlayerData.Instance.Initialize();

        this.PostNotification(MenuHeader.OnHeaderUpdateNotification);

        MenuHeader.OnShopButtonClicked += OpenShop;
        MenuHeader.OnInventoryClicked += OpenInventory;

        MessageController.ClearOverlay();
    }

    public override void Exit() {
        MenuHeader.OnShopButtonClicked -= OpenShop;
        MenuHeader.OnInventoryClicked -= OpenInventory;
    }
}