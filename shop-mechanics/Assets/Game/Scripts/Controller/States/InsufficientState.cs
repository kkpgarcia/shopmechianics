using UnityEngine;
using System.Collections;

public class InsufficientState : GameState {
    public override void Enter() {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.75f);
        MessageController.Initialize(
            "You don't have enough money for this purchase.",
            OnCancel,
            OnCancel
        );
        
        MessageController.Show();
    }

    private void OnCancel() {
        MessageController.ClearOverlay();
        MessageController.Clean();
        this.Owner.ChangeState<ShopState>();
    }
}