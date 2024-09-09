using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class RingSpin : MonoBehaviour
{
    private Animator animator;
    private CanvasGroup canvasGroup;
    [Header("Parameters")]
    public float startSpeed;
    public float endSpeed;
    public float decreaseAmt;
    [Header("Debug (READ ONLY)")]
    [SerializeField] private float tSpin; // lmaoo t-spin x3
    [SerializeField] private float tFade;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private bool hidden;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        tSpin += decreaseAmt * Time.deltaTime;
        spinSpeed = Mathf.Lerp(startSpeed, endSpeed, tSpin);
        animator.speed = spinSpeed;

        if (hidden) canvasGroup.alpha = 0;
        else
        {
            tFade += 5 * Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 0.8f, tFade);
        }
    }
    public void Spin() => tSpin = 0;
    public void Fade(bool hide)
    {
        tFade = 0;
        hidden = hide;
    }
}