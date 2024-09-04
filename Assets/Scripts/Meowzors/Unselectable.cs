using UnityEngine;
using UnityEngine.EventSystems;

public class Unselectable : MonoBehaviour, ISelectHandler
{

    private bool _suspectedSelected;

    public void OnSelect(BaseEventData eventData)
    {
        if (MainUI.usingController)
        {
            return;
        }

        _suspectedSelected = true;
    }

    private void Update()
    {

        if (!_suspectedSelected)
            return;

        if(MainUI.usingController)
        {
            return;
        }

        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {

            EventSystem.current.SetSelectedGameObject(null);

        }

        _suspectedSelected = false;

    }

}