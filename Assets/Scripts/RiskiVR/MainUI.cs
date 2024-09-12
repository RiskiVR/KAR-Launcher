using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    public static int currentTab;
    public static bool usingController;
    
    [Header("Canvas")]
    [SerializeField] GraphicRaycaster graphicRaycaster;

    [Header("Audio")]
    public AudioSource sfx;
    public AudioSource bgm;
    public AudioClip[] menu;

    [Header("Internal Elements")]
    public TextMeshProUGUI headerText;
    public Animation headerAnim;
    public Animator tabAnim;
    public Animator discAnim;
    public Animation infoAnim;
    public Animation infoAnim2;
    public TextMeshProUGUI infoText;
    public Animator menuBGAnim;
    public GameObject[] menuBG;
    [SerializeField] GameObject[] tabs;
    [SerializeField] string[] tabInfo;
    [SerializeField] Color[] menuColors;
    [SerializeField] Image coloredRings;
    public RingSpin ringSpin;
    
    [Header("Confirmation Menu")]
    public Transform confirmTransform;
    public Button longButtonPrefab;
    private void Awake()
    {
        instance = this;
        InputSystem.onActionChange += InputSystem_onActionChange;
        QualitySettings.vSyncCount = 1;
        UpdateTab(0);
    }

    private void Update()
    {
        if (!usingController) graphicRaycaster.enabled = Application.isFocused;
        bgm.mute = !Application.isFocused;
    }
    public void UpdateTab(int tab)
    {
        currentTab = tab;
        infoText.text = String.Empty;
        foreach (GameObject g in tabs) g.SetActive(false);
        tabs[tab].SetActive(true);
        headerText.text = tabInfo[currentTab];
        headerAnim.Play();
        infoAnim2.Play();
        if (usingController) SelectFirstButton();
        if (currentTab > 0)
        {
            tabAnim.SetBool("anim", true);
            menuBGAnim.SetBool("fade", true);
            foreach (GameObject g in menuBG) g.SetActive(false);
            menuBG[currentTab - 1].SetActive(true);
            coloredRings.color = menuColors[tab];
            ringSpin.Fade(false);
        }
        else
        {
            tabAnim.SetBool("anim", false);
            menuBGAnim.SetBool("fade", false);
            ringSpin.Fade(true);
        }
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
    private void SelectFirstButton() => EventSystem.current.SetSelectedGameObject(tabs[currentTab].transform.GetChild(0).transform.GetChild(0).gameObject);
    private void UseController(bool ctrl)
    {
        Cursor.visible = !ctrl;
        usingController = ctrl;
        graphicRaycaster.enabled = !ctrl;
        if (ctrl)
        {
            if (EventSystem.current.currentSelectedGameObject == null) SelectFirstButton();
        }
        else EventSystem.current.SetSelectedGameObject(null);
    }
    public void ShowDisc(bool show)
    {
        discAnim.SetTrigger("anim2");
        discAnim.SetBool("anim", show); 
    }
    public class MessageUI
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);
    }
}