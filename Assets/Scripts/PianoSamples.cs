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
    public float GetNoteHeight(KeyNote note, ClefType clef)
    {
        foreach (var sample in samples)
        {
            if (sample.note == note)
            {
                if (clef == ClefType.Treble)
                {
                    return sample.noteHeight;
                }
                else if (clef == ClefType.Bass)
                {
                    return sample.fNoteHeight;
                }
            }
        }
        return 0;
    }
    
    public NoteOverPosition GetNoteOverPosition(KeyNote note, ClefType clef)
    {
        foreach (var sample in samples)
        {
            if (sample.note == note)
            {
                if (clef == ClefType.Treble)
                {
                    return sample.noteOverPosition;
                }
                else if (clef == ClefType.Bass)
                {
                    return sample.fNoteOverPosition;
                }
            }
        }
        return NoteOverPosition.None;
    }
}

[System.Serializable]
public struct PianoSample
{
    public KeyNote note;
    public AudioClip clip;
    public float noteHeight;
    public float fNoteHeight;
    public NoteOverPosition noteOverPosition;
    public NoteOverPosition fNoteOverPosition;
}