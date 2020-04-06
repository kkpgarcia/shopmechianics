using UnityEngine;
using UnityEngine.UI;

using System;

public class MenuHeader : MonoBehaviour {
    [SerializeField]
    private Button m_ShopButton;
    [SerializeField]
    private Button m_InventoryButton;
    [SerializeField]
    private Text m_CurrencyText;
    
    public event EventHandler OnShopButtonClicked;
    public event EventHandler OnInventoryClicked;

    public const string OnHeaderUpdateNotification = "MenuHeader.OnHeaderUpdateNotification";
    
    private void Start() {
        m_ShopButton.onClick.AddListener(() => {
            if(OnShopButtonClicked != null)
                OnShopButtonClicked(this, null);
        });

        m_InventoryButton.onClick.AddListener(() => {
            if(OnInventoryClicked != null)
                OnInventoryClicked(this, null);
        });
    }

    private void OnEnable() {
        this.AddObserver(OnHeaderUpdate, OnHeaderUpdateNotification);
    }

    private void OnDisable() {
        this.AddObserver(OnHeaderUpdate, OnHeaderUpdateNotification);
    }

    private void OnHeaderUpdate(object sender, object args) {
        //Update Header
    }
}