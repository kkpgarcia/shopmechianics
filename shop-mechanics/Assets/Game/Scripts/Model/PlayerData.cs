using System.Collections.Generic;
using Common.Singleton;

public class PlayerData : MonoSingleton<PlayerData> {
    
    public int Currency;
    public List<Item> Items;

    public void Initialize() {
        Items = new List<Item>();
        Currency = 200;
    }
}