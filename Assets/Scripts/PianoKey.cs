using System.Collections;
using Oculus.Interaction;
using UnityEngine;
public enum KeyNote
{
    // From C2 to C7 (61 keys)
    C2,
    CSharp2,
    D2,
    DSharp2,
    E2,
    F2,
    FSharp2,
    G2,
    GSharp2,
    A2,
    ASharp2,
    B2,
    C3,
    CSharp3,
    D3,
    DSharp3,
    E3,
    F3,
    FSharp3,
    G3,
    GSharp3,
    A3,
    ASharp3,
    B3,
    C4,
    CSharp4,
    D4,
    DSharp4,
    E4,
    F4,
    FSharp4,
    G4,
    GSharp4,
    A4,
    ASharp4,
    B4,
    C5,
    CSharp5,
    D5,
    DSharp5,
    E5,
    F5,
    FSharp5,
    G5,
    GSharp5,
    A5,
    ASharp5,
    B5,
    C6,
    CSharp6,
    D6,
    DSharp6,
    E6,
    F6,
    FSharp6,
    G6,
    GSharp6,
    A6,
    ASharp6,
    B6,
    C7
}
public class PianoKey : MonoBehaviour
{
    [SerializeField] private KeyNote note;
    [SerializeField] private PianoSamples pianoSamples;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeDuration = 1.0f; // Time in seconds for the fade

    
    public void PlayNote()
    {
        AudioClip clip = pianoSamples.GetClip(note);
        if (audioSource && clip)
        {
            audioSource.clip = clip;
            audioSource.volume = 1.0f; // Reset volume to max before playing
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"No audio clip found for note: {note}");
        }
    }
    
    public void StopNote()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        // Gradually decrease volume over fadeDuration
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null; // Wait for next frame
        }

        audioSource.volume = 0f;
        audioSource.Stop(); // Stop the audio after fading
    }
}

