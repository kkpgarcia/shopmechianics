using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Category : MonoBehaviour {
    [SerializeField]
    private Text Title;

    [SerializeField]
    private Button m_Button;

    [SerializeField]
    private Panel m_Panel;
    private int m_Index;
    private bool m_IsSelected = false;

    private void Start() {
        this.m_Panel.SetPosition("Unselected", false);
    }

    public void Initialize(int index, string title, UnityAction<string> action) {
        this.Title.text = title;
        this.m_Button.onClick.AddListener(() => {
             if(m_IsSelected)
                return;

            m_IsSelected = true;

            if(action != null)
                action(this.Title.text);

            SelectCategory();
        });
        this.m_Index = index;
    }
    
    public void SelectCategory() {
        this.m_Panel.SetPosition("Selected", true)
            .SetDuration(0.5f)
            .SetEquation(EasingEquations.EaseInOutQuint);
        this.PostNotification(CategorySelector.OnSelectNotification, 
            new InfoEventArgs<int>(this.m_Index));
    }

    // to remove
    public void UnselectCategory(bool animated = true) {
        m_IsSelected = false;
        this.m_Panel.SetPosition("Unselected", true)
            .SetDuration(0.5f)
            .SetEquation(EasingEquations.EaseInOutQuint);
    }

    private void OnDisable() {
        this.m_Button.onClick.RemoveAllListeners();
    }
}