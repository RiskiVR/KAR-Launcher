using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InitializeUI : MonoBehaviour
{
    public static InitializeUI instance;
    public static bool usingController;
    
    [Header("Canvas")]
    [SerializeField] GraphicRaycaster graphicRaycaster;

    [Header("Audio")]
    public AudioSource sfx;
    public AudioClip[] menu;
    
    [Header("Internal Elements")]
    [SerializeField] GameObject firstSelected;
    
    private void Awake()
    {
        instance = this;
        InputSystem.onActionChange += InputSystem_onActionChange;
        QualitySettings.vSyncCount = 1;
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!usingController) graphicRaycaster.enabled = hasFocus;
    }
    
    private void InputSystem_onActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            if (obj.ToString().Contains("Navigate")) UseController(true);
            if (obj.ToString().Contains("Point")) UseController(false);
        }
    }
    private void OnDestroy() => InputSystem.onActionChange -= InputSystem_onActionChange;
    private void SelectFirstButton() => EventSystem.current.SetSelectedGameObject(firstSelected);
    private void UseController(bool ctrl)
    {
        if (!Application.isFocused) return;
        
        Cursor.visible = !ctrl;
        usingController = ctrl;
        graphicRaycaster.enabled = !ctrl;
        if (ctrl)
        {
            if (EventSystem.current.currentSelectedGameObject == null) SelectFirstButton();
        }
        else EventSystem.current.SetSelectedGameObject(null);
    }
}