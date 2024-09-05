using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLoop : MonoBehaviour
{
    [SerializeField] private float loopStartTime;
    [SerializeField] private float loopEndTime;
    private AudioSource audioSource;
    void Start() => audioSource = GetComponent<AudioSource>();
    void Update()
    {
        if (audioSource.time >= loopEndTime || !audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.time = loopStartTime;
        }
    }
}