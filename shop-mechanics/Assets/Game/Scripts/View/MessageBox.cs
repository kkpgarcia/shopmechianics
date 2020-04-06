using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MessageBox : MonoBehaviour {
    [SerializeField]
    private Text m_Message;
    
    [SerializeField]
    private Button m_Cancel;

    [SerializeField]
    private Button m_Confirm;

    [SerializeField]
    private Panel m_Panel;

    private void Start() {
        m_Panel.SetPosition("Hide", false);
    }

    public void Initialize(string message, UnityAction onConfirm, UnityAction onCancel) {
        m_Message.text = message;

        m_Cancel.onClick.AddListener(() => {
            if(onCancel != null)
                onCancel();
            Hide();
        });

        m_Confirm.onClick.AddListener(() => {
            if(onConfirm != null)
                onConfirm();
            Hide();
        });
    }

    public void Show() {
        m_Panel.SetPosition("Show", true);
    }

    public void Hide() {
        m_Panel.SetPosition("Hide", true);
    }

    public void Clean() {
        m_Cancel.onClick.RemoveAllListeners();
        m_Confirm.onClick.RemoveAllListeners();
    }
}