using UnityEngine;

public class GameState : State {
    protected GameBootstrap Owner;
    protected CategorySelector CategorySelector { get { return Owner.CategorySelector; } }
    protected StoreManager StoreManager { get { return Owner.StoreManager; } }
    protected ItemGrid ItemGrid { get { return Owner.ItemGrid; } }
    protected MessageController MessageController { get { return Owner.MessageController; } }

    private void Awake() {
        this.Owner = this.GetComponent<GameBootstrap>();
    }
}