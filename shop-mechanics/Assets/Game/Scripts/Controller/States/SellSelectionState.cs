using UnityEngine;

public class SellSelectionState : GameState {
    public override void Enter() {
        Item item = StoreManager.ItemSelected;
        MessageController.Initialize(
            string.Format("Would you like to sell {0} for ${1}?", item.Name, item.Price),
            OnConfirm,
            OnCancel
        );

        MessageController.Show();
    }

    private void OnConfirm() {
        Item currItem = StoreManager.ItemSelected;

        if(currItem == null)
            return;
            
        PlayerData.Instance.Currency += currItem.Price;
        InventoryGrid.Remove(currItem);
        PlayerData.Instance.Items.Remove(currItem);

        MessageController.ClearOverlay();
        this.PostNotification(MenuHeader.OnHeaderUpdateNotification);

        this.Owner.ChangeState<InventoryState>();
        
        MessageController.Clean();
    }

    private void OnCancel() {
        MessageController.ClearOverlay();
        MessageController.Clean();
        this.Owner.ChangeState<InventoryState>();
    }
}