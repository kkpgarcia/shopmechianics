using UnityEngine;

public class ItemSelectionState : GameState {
    public override void Enter() {
        Item item = StoreManager.ItemSelected;
        MessageController.Initialize(
            string.Format("Would you like to purchase {0} for ${1}?", item.Name, item.Price),
            OnConfirm,
            OnCancel
        );

        MessageController.Show();
    }

    private void OnConfirm() {
        Item currItem = StoreManager.ItemSelected;

        if(currItem == null)
            return;

        if(PlayerData.Instance.Currency - currItem.Price < 0) {
            //Insufficient funds
        } else {
            PlayerData.Instance.Currency -= currItem.Price;
            PlayerData.Instance.Items.Add(currItem);

            MessageController.ClearOverlay();
            this.PostNotification(MenuHeader.OnHeaderUpdateNotification);

            this.Owner.ChangeState<ShopState>();
        }

        MessageController.Clean();
    }

    private void OnCancel() {
        MessageController.ClearOverlay();
        MessageController.Clean();
        this.Owner.ChangeState<ShopState>();
    }
}