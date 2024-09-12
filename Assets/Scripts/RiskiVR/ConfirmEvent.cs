using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ConfirmEvent : MonoBehaviour
{
    [TextArea]
    [SerializeField] string headerInfo;
    [SerializeField] UnityEvent onConfirm;
    [SerializeField] UnityEvent onCancel;
    [SerializeField] UnityEvent onEither;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            MainUI.instance.confirmTransform.gameObject.SetActive(true);
            MainUI.instance.confirmTransform.GetChild(0).GetComponent<TextMeshProUGUI>().text = headerInfo;
            var cancelButton = Instantiate(MainUI.instance.longButtonPrefab, MainUI.instance.confirmTransform.GetChild(1));
            var confirmButton = Instantiate(MainUI.instance.longButtonPrefab, MainUI.instance.confirmTransform.GetChild(1));
            EventSystem.current.SetSelectedGameObject(MainUI.instance.confirmTransform.GetChild(1).GetChild(0).gameObject);
            cancelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";
            confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = "Confirm";
            cancelButton.onClick.AddListener(() =>
            {
                MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[0]);
                onCancel.Invoke(); onEither.Invoke();
                foreach (Transform btn in MainUI.instance.confirmTransform.GetChild(1)) Destroy(btn.gameObject);
                MainUI.instance.confirmTransform.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(gameObject);
            });
            confirmButton.onClick.AddListener(() =>
            {
                MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[1]);
                onConfirm.Invoke(); onEither.Invoke();
                MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[2]);
                MainUI.instance.sfx.PlayOneShot(MainUI.instance.menu[6]);
                foreach (Transform btn in MainUI.instance.confirmTransform.GetChild(1)) Destroy(btn.gameObject);
                MainUI.instance.confirmTransform.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(gameObject);
            });
        });
    }
}