using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ClefType
{
    Treble,
    Bass
}
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] int numberOfNotes = 5;
    [SerializeField] List<Clef> usedClef = new List<Clef>();
    [SerializeField] Transform noteParent;
    [SerializeField] GameObject notePrefab;
    private List<Note> spawnedNotes = new List<Note>();
    private int currentNote;

    public static Clef CurrentClef;
    void Start()
    {
        SetCurrentClef();
        SpawnNotes();
    }

    private void SpawnNotes()
    {
        for (int i = 0; i < numberOfNotes; i++)
        {
            Note note = Instantiate(notePrefab, noteParent).GetComponent<Note>();
            note.SetNote(CurrentClef.notes[Random.Range(0, CurrentClef.notes.Count)]);
            spawnedNotes.Add(note);
        }
    }

    private void SetCurrentClef()
    {
        if (CurrentClef.clefGameObject) CurrentClef.clefGameObject.SetActive(false);
        CurrentClef = usedClef[Random.Range(0, usedClef.Count)];
        CurrentClef.clefGameObject.SetActive(true);
    }
}
[System.Serializable]
public struct Clef
{
    public ClefType type;
    public List<KeyNote> notes;
    public GameObject clefGameObject;
}
