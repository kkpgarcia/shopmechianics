using UnityEngine;

public class InitializeState : GameState {
    public override void Enter() {
        //Any game specific initializations are done here
        StoreData data = ResourceManager.Instance.Load<StoreData>("Store/Some Random Store");
    
        for(int i = 0; i < data.Categories.Length; i++) {
            CategorySelector.AddCategory(i, data.Categories[i].name, OnSelectCategory);
        }
    }

    public void OnSelectCategory(string s) {
        Debug.Log("Selected category: " + s);
    }

    public void LoadShop() {}
}