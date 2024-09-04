using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] string info;
    void Start() => text.text = String.Empty;
    public void OnSelect(BaseEventData eventData)
    {
        if (MainUI.usingController) OnUIHandle(true);
    }
    public void OnPointerEnter(PointerEventData eventData) => OnUIHandle(true);
    public void OnPointerExit(PointerEventData eventData) => OnUIHandle(false);
    public void OnUIHandle(bool enter)
    {
        if (enter)
        {
            if (MainUI.currentTab == 0)
            {
                MainUI.instance.discAnim.SetTrigger("anim2");
                MainUI.instance.discAnim.SetBool("anim", true);
            } else MainUI.instance.discAnim.SetBool("anim", false);
            MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[3]);
            MainUI.instance.infoAnim.Play();
            text.text = info;
        }
        else
        {
            if (MainUI.currentTab == 0) MainUI.instance.discAnim.SetBool("anim", false);
            text.text = String.Empty;
        }
    }
}