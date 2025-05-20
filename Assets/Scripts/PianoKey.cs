using System;
using System.Collections;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum KeyNote
{
    // From C2 to C7 (61 keys)
    C2,
    D2,
    E2,
    F2,
    G2,
    A2,
    B2,
    C3,
    D3,
    E3,
    F3,
    G3,
    A3,
    B3,
    C4,
    D4,
    E4,
    F4,
    G4,
    A4,
    B4,
    C5,
    D5,
    E5,
    F5,
    G5,
    A5,
    B5,
    C6,
    D6,
    E6,
    F6,
    G6,
    A6,
    B6,
    C7,
    C2Sharp,
    D2Flat,
    D2Sharp,
    E2Flat,
    F2Sharp,
    G2Flat,
    G2Sharp,
    A2Flat,
    A2Sharp,
    B2Flat,
    C3Sharp,
    D3Flat,
    D3Sharp,
    E3Flat,
    F3Sharp,
    G3Flat,
    G3Sharp,
    A3Flat,
    A3Sharp,
    B3Flat,
    C4Sharp,
    D4Flat,
    D4Sharp,
    E4Flat,
    F4Sharp,
    G4Flat,
    G4Sharp,
    A4Flat,
    A4Sharp,
    B4Flat,
    C5Sharp,
    D5Flat,
    D5Sharp,
    E5Flat,
    F5Sharp,
    G5Flat,
    G5Sharp,
    A5Flat,
    A5Sharp,
    B5Flat,
    C6Sharp,
    D6Flat,
    D6Sharp,
    E6Flat,
    F6Sharp,
    G6Flat,
    G6Sharp,
    A6Flat,
    A6Sharp,
    B6Flat,
}

public class PianoKey : MonoBehaviour
{
    [SerializeField] private KeyNote[] note;
    [SerializeField] private PianoSamples pianoSamples;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeDuration = 1.0f; // Time in seconds for the fade
    [SerializeField] private bool isSharpKey;

    private void Start()
    {
        if (isSharpKey)
        {
            audioSource.pitch = GetPitchOffset(1);
        }
    }

    public void PlayNote()
    {
		StopAllCoroutines();
        AudioClip clip = pianoSamples.GetClip(note);
        if (audioSource && clip)
        {
			audioSource.Stop();
            audioSource.clip = clip;
            audioSource.volume = 1.0f; // Reset volume to max before playing
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"No audio clip found for note: {note}");
        }
        ScoreDisplay.Instance.OnPianoKeyPressed(note);
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

    static float GetPitchOffset(int semitones)
    {
        return Mathf.Pow(2f, semitones / 12f); // 12 semitones = 1 octave
    }
}