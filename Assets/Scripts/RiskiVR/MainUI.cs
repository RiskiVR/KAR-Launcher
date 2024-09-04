using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Internal Elements")] public static MainUI instance;
    public TextMeshProUGUI headerText;
    public AudioSource audioSource;
    public AudioClip[] menu;
    public Animator tabAnim;
    public Animator discAnim;
    private Selectable[] allSelectables;
    public static int currentTab;
    private bool usingController;
    [SerializeField] GameObject[] tabs;
    [SerializeField] string[] tabInfo;
    void Awake()
    {
        instance = this;
        Screen.SetResolution(1280, 960, false);
    }
    private void Start()
    {
        InputSystem.onActionChange += InputSystem_onActionChange;
        allSelectables = FindObjectsOfType<Selectable>(true);
        UpdateTab(0);
        for (int i = 0; i < allSelectables.Length; i++)
        {
            var trigger = allSelectables[i].gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { audioSource.PlayOneShot(menu[3]); });
            trigger.triggers.Add(entry);
        }
    }
    public void SwitchScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
    
    public void UpdateTab(int tab)
    {
        currentTab = tab;
        foreach (GameObject g in tabs) g.SetActive(false);
        tabs[tab].SetActive(true);
        headerText.text = tabInfo[currentTab];
        if (usingController) SelectFirstButton();
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

        foreach (Button b in allSelectables)
        {
            Navigation n = new Navigation();
            n.mode = ctrl ? Navigation.Mode.Automatic : Navigation.Mode.None;
            b.navigation = n;
        }
    }

    public class MessageUI
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);
    }
}