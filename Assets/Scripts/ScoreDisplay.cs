using System;
using System.Collections;
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
    [SerializeField] private int numberOfNotes = 5;
    [SerializeField] private List<Clef> usedClef = new List<Clef>();
    [SerializeField] private Transform noteParent;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Color correctNote = Color.green;
    [SerializeField] private Color wrongNote = Color.red;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private List<NoteAccidental> accidentals = new List<NoteAccidental>();
    private List<Note> spawnedNotes = new List<Note>();
    private int currentNoteIndex = 0;
    public static Clef CurrentClef;
    public static ScoreDisplay Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(InitGame());
    }

    private void SpawnNotes()
    {
        for (int i = 0; i < numberOfNotes; i++)
        {
            var note = Instantiate(notePrefab, noteParent).GetComponent<Note>();
            var randomNote = CurrentClef.notes[Random.Range(0, CurrentClef.notes.Count)];
            NoteAccidental randomAccidental = accidentals[Random.Range(0, accidentals.Count)];
            note.SetNote(randomNote, randomAccidental);
            spawnedNotes.Add(note);
        }
    }

    private void SetCurrentClef()
    {
        if (CurrentClef.clefGameObject) CurrentClef.clefGameObject.SetActive(false);
        CurrentClef = usedClef[Random.Range(0, usedClef.Count)];
        CurrentClef.clefGameObject.SetActive(true);
    }
    
    private IEnumerator InitGame()
    {
        yield return new WaitForSeconds(startDelay);
        currentNoteIndex = 0;
        foreach (Note note in spawnedNotes)
        {
            Destroy(note.gameObject);
        }
        spawnedNotes.Clear();
        SetCurrentClef();
        SpawnNotes();
    }
    
    public void OnPianoKeyPressed(KeyNote note)
    {
        if (currentNoteIndex >= spawnedNotes.Count) return;
        if (spawnedNotes[currentNoteIndex].Key == note)
        {
            spawnedNotes[currentNoteIndex].SetNoteColor(correctNote);
        }
        else
        {
            spawnedNotes[currentNoteIndex].SetNoteColor(wrongNote);
        }
        currentNoteIndex++;
        if (currentNoteIndex >= spawnedNotes.Count)
        {
            StartCoroutine(InitGame());
        }
    }
}
[System.Serializable]
public struct Clef
{
    public ClefType type;
    public List<KeyNote> notes;
    public GameObject clefGameObject;
}
