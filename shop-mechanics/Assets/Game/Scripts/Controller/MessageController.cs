using UnityEngine;
using UnityEngine.Events;

public class MessageController : MonoBehaviour {
    public MessageBox MessageBox;
    public Overlay Overlay;

    public void Initialize(string message, UnityAction onConfirm, UnityAction onCancel) {
        MessageBox.Initialize(message, onConfirm, onCancel);
    }

    public void Show() {
        Overlay.Show();
        MessageBox.Show();
    }

    public void Hide() {
        Overlay.Hide();
        MessageBox.Hide();
    }

    public void ClearOverlay() {
        Overlay.Hide();
    }
}