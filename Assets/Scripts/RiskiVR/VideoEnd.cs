using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    [SerializeField] private float targetTime = 0f;
    [SerializeField] private bool useVideoLength;
    [SerializeField] private UnityEvent onVideoEnd;
    private VideoPlayer videoplayer;
    private float elapsedTime = 0f;
    void Awake() => videoplayer = GetComponent<VideoPlayer>();
    void OnEnable() => elapsedTime = 0;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (useVideoLength && videoplayer.clip) targetTime = (float)videoplayer.clip.length;
        if (elapsedTime >= targetTime) onVideoEnd.Invoke();
    }
}