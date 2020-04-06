using UnityEngine;

public class ShopState : GameState {
    public override void Enter() {
        this.PostNotification(CategorySelector.OnClearSelectionNotification); 
    }
}