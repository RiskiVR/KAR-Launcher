using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class MenuInputManager : MonoBehaviour
{
    public UnityEvent onA;
    public UnityEvent onB;
    public UnityEvent onX;
    public UnityEvent onY;
    public UnityEvent onStart;
    [Header("Object Requirement")]
    [SerializeField] private bool requiresObject;
    [SerializeField] private GameObject requiredObject;
    [Header("Input Action References")]
    [SerializeField] private InputActionReference submit;
    [SerializeField] private InputActionReference cancel;
    [SerializeField] private InputActionReference north;
    [SerializeField] private InputActionReference west;
    [SerializeField] private InputActionReference start;
    private void Update()
    {
        if (requiresObject)
        {
            if (!requiredObject.activeSelf) return;
        }
        if (submit.action.triggered) onA.Invoke();
        if (cancel.action.triggered) onB.Invoke();
        if (west.action.triggered) onX.Invoke();
        if (north.action.triggered) onY.Invoke();
        if (start.action.triggered) onStart.Invoke();
    }
    
    public void InvokeA() => onA.Invoke();
    public void InvokeB() => onB.Invoke();
    public void InvokeX() => onX.Invoke();
    public void InvokeY() => onY.Invoke();
    public void InvokeStart() => onStart.Invoke();
}