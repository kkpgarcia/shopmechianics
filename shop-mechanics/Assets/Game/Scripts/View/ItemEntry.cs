using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemEntry : MonoBehaviour {
    [SerializeField]
    private Image m_Icon;

    [SerializeField]
    private Text m_ItemName;

    [SerializeField]
    private Text m_Cost;
    [SerializeField]
    private Button m_Button;

    private Item m_Item;
    
    public void Initialize(Item item, UnityAction<Item> callback, bool showPrice = true) {
        m_Item = item;

        this.m_Icon.sprite = m_Item.Icon;
        this.m_ItemName.text = m_Item.Name;
        this.m_Cost.text = showPrice ? "$" + m_Item.Price : "";

        this.m_Button.onClick.AddListener(() => {
            if(callback != null)
                callback(m_Item);
        });
    }

    public bool Compare(Item item) {
        return m_Item == item;
    }

    private void OnDisable() {
        this.m_Button.onClick.RemoveAllListeners();
    }
}