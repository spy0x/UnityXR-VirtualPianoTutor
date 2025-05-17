using UnityEngine;

[CreateAssetMenu(fileName = "PianoSamples", menuName = "Scriptable Objects/PianoSamples")]
public class PianoSamples : ScriptableObject
{
    public PianoSample[] Samples;

    public AudioClip GetClip(KeyNote note)
    {
        foreach (var sample in Samples)
        {
            if (sample.Note == note)
            {
                return sample.Clip;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct PianoSample
{
    public KeyNote Note;
    public AudioClip Clip;
}