using UnityEngine;
[RequireComponent(typeof(ButtonInfo))]
public class ClientInfo : MonoBehaviour
{
    [SerializeField] string info;
    private ButtonInfo buttonInfo;
    private void Awake() => buttonInfo = GetComponent<ButtonInfo>();
    private void OnEnable()
    {
        var infoFull = $"{info} ({Netplay.clientNames[Netplay.currentClient]})";
        MainUI.instance.infoText.text = infoFull;
        buttonInfo.info = infoFull;
    }
}