using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sourceA;
    public AudioSource sourceB;

    private AudioSource activeSource;
    private AudioSource inactiveSource;

    [Header("Settings")]
    public float fadeDuration = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ensure we have both sources
        if (sourceA == null)
            sourceA = gameObject.AddComponent<AudioSource>();
        if (sourceB == null)
            sourceB = gameObject.AddComponent<AudioSource>();

        sourceA.loop = true;
        sourceB.loop = true;

        activeSource = sourceA;
        inactiveSource = sourceB;
    }

    /// <summary>
    /// Plays or crossfades to a new background music clip.
    /// </summary>
    public void PlayMusic(AudioClip newClip)
    {
        if (newClip == null) return;

        // If same clip, ignore
        if (activeSource.clip == newClip && activeSource.isPlaying)
            return;

        // Swap sources
        inactiveSource.clip = newClip;
        inactiveSource.volume = 0f;
        inactiveSource.Play();

        StartCoroutine(FadeMusic());
    }

    private IEnumerator FadeMusic()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            activeSource.volume = Mathf.Lerp(1f, 0f, t);
            inactiveSource.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        activeSource.Stop();

        // Swap references
        var temp = activeSource;
        activeSource = inactiveSource;
        inactiveSource = temp;
    }
}
