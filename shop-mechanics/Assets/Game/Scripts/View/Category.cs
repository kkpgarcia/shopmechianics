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

    private void Start() {
        this.m_Panel.SetPosition("Unselected", false);
    }

    public void Initialize(int index, string title, UnityAction<string> action) {
        this.Title.text = title;
        this.m_Button.onClick.AddListener(() => {
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
    public void UnselectCategory() {
        this.m_Panel.SetPosition("Unselected", true)
            .SetDuration(0.5f)
            .SetEquation(EasingEquations.EaseInOutQuint);
    }
}