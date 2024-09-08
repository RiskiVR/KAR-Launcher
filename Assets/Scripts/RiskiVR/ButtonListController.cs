using UnityEngine;
using UnityEngine.UI;

public class ButtonListController : MonoBehaviour
{
    Button[] buttons;
    void Awake() => buttons = GetComponentsInChildren<Button>();
    public void SetInteractable(bool interactable)
    {
        foreach (Button b in buttons) b.interactable = interactable;
    }
}