using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    public string info;
    private Button button;
    private void Awake() => button = GetComponent<Button>();
    private void OnEnable()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject) MainUI.instance.infoText.text = info;
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (MainUI.usingController) OnUIHandle(true);
    }
    public void OnPointerEnter(PointerEventData eventData) => OnUIHandle(true);
    public void OnPointerExit(PointerEventData eventData) => OnUIHandle(false);
    public void OnUIHandle(bool enter)
    {
        if (!button.interactable) return;
        if (enter)
        {
            if (MainUI.currentTab == 0)
            {
                MainUI.instance.discAnim.SetTrigger("anim2");
                MainUI.instance.discAnim.SetBool("anim", true);
            }
            else
            {
                MainUI.instance.discAnim.SetBool("anim", false);
                MainUI.instance.ringSpin.Spin();
            }
            MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[3]);
            if (info == String.Empty) return;
            MainUI.instance.infoAnim.Play();
            MainUI.instance.infoText.text = info;
        }
        else
        {
            if (MainUI.currentTab == 0) MainUI.instance.discAnim.SetBool("anim", false);
            if (info == String.Empty) return;
            MainUI.instance.infoText.text = String.Empty;
        }
    }
}