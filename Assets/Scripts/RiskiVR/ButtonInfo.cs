using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] string info;
    public void OnPointerEnter(PointerEventData eventData)
    {
        MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[3]);
        MainUI.instance.discAnim.SetBool("anim", true);
        if (text) text.text = info;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MainUI.instance.discAnim.SetBool("anim", false);
        text.text = String.Empty;
    }
    void Start() => text.text = String.Empty;
}