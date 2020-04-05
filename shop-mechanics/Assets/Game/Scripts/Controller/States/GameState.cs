using UnityEngine;

public class GameState : State {
    protected GameBoostrap Owner;

    private void Awake() {
        this.Owner = this.GetComponent<GameBoostrap>();
    }
}