using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedClient : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(transform.GetChild(Netplay.currentClient).gameObject);
    }
}