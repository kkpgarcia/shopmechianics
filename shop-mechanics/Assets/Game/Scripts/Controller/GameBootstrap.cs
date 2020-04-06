using UnityEngine;

public class GameBootstrap : StateMachine {
    public CategorySelector CategorySelector;
    public StoreManager StoreManager;
    public ItemGrid ItemGrid;

    public void Start() {
        //Any system implementations are done here

        //Finally we initialize the game
        this.ChangeState<InitializeState>();
    }
}