using UnityEngine;

public class GameState : State {
    protected GameBootstrap Owner;
    protected CategorySelector CategorySelector { get { return Owner.CategorySelector; } }

    private void Awake() {
        this.Owner = this.GetComponent<GameBootstrap>();
    }
}