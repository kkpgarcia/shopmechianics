using UnityEngine;

public class GameBoostrap : StateMachine {
    public void Start() {
        //Any system implementations are done here

        //Finally we initialize the game
        this.ChangeState<InitializeState>();
    }
}