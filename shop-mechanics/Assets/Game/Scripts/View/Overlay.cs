using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour {
    [SerializeField]
    private Image m_Image;

    public void Show() {
        m_Image.CrossFadeAlpha(0.5f, 1f, false);
        m_Image.raycastTarget = true;
    }

    public void Hide() {
        m_Image.CrossFadeAlpha(0.01f, 1f, false);
        m_Image.raycastTarget = false;
    }
}