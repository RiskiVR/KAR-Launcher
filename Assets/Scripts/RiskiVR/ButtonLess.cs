using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonLess : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    private Button button;
    private void Awake() => button = GetComponent<Button>();
    public void OnSelect(BaseEventData eventData)
    {
        if (InitializeUI.usingController) OnUIHandle(true);
    }
    public void OnPointerEnter(PointerEventData eventData) => OnUIHandle(true);
    public void OnPointerExit(PointerEventData eventData) => OnUIHandle(false);
    public void OnUIHandle(bool enter)
    {
        if (!button.interactable) return;
        if (enter)
        {
            InitializeUI.instance.sfx.PlayOneShot(InitializeUI.instance.menu[3]);
        }
    }
}