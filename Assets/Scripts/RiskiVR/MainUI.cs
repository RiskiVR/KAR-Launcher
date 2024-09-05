using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] menu;
    
    [Header("Internal Elements")]
    public TextMeshProUGUI headerText;
    public Animation headerAnim;
    public Animator tabAnim;
    public Animator discAnim;
    public Animation infoAnim;
    public TextMeshProUGUI infoText;
    public Animator menuBGAnim;
    public GameObject[] menuBG;
    public static int currentTab;
    public static bool usingController;
    [SerializeField] GameObject[] tabs;
    [SerializeField] string[] tabInfo;
    private void Start()
    {
        instance = this;
        InputSystem.onActionChange += InputSystem_onActionChange;
        UpdateTab(0);
        infoText.text = "Launcher by RiskiVR";
    }
    public void UpdateTab(int tab)
    {
        currentTab = tab;
        infoText.text = String.Empty;
        foreach (GameObject g in tabs) g.SetActive(false);
        tabs[tab].SetActive(true);
        headerText.text = tabInfo[currentTab];
        headerAnim.Play();
        if (usingController) SelectFirstButton();
        if (currentTab > 0)
        {
            tabAnim.SetBool("anim", true);
            menuBGAnim.SetBool("fade", true);
            foreach (GameObject g in menuBG) g.SetActive(false);
            menuBG[currentTab - 1].SetActive(true);
        }
        else
        {
            tabAnim.SetBool("anim", false);
            menuBGAnim.SetBool("fade", false);
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