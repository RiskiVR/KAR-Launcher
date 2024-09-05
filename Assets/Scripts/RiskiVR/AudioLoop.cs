using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLoop : MonoBehaviour
{
    [SerializeField] private float loopStartTime;
    private AudioSource audioSource;
    void Start() => audioSource = GetComponent<AudioSource>();
    void Update()
    {
        if (audioSource.time >= audioSource.clip.length || !audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.time = loopStartTime;
        }
    }
}