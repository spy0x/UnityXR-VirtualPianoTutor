using UnityEngine;

[CreateAssetMenu(fileName = "PianoSamples", menuName = "Scriptable Objects/PianoSamples")]
public class PianoSamples : ScriptableObject
{
    public PianoSample[] samples;

    public AudioClip GetClip(KeyNote note)
    {
        foreach (var sample in samples)
        {
            if (sample.note == note)
            {
                return sample.clip;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct PianoSample
{
    public KeyNote note;
    public AudioClip clip;
    public float noteHeight;
    public float fNoteHeight;
}