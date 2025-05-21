using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI scoreTextFeedback;
    [SerializeField] private float feedbackDuration = 2f;
    private List<Note> spawnedNotes = new List<Note>();
    private int currentNoteIndex = 0;
    public static Clef CurrentClef;
    public static ScoreDisplay Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scoreTextFeedback.text = "";
        StartCoroutine(InitGame());
    }

    private void SpawnNotes()
    {
        for (int i = 0; i < numberOfNotes; i++)
        {
            var note = Instantiate(notePrefab, noteParent).GetComponent<Note>();
            var randomNote = CurrentClef.notes[Random.Range(0, CurrentClef.notes.Count)];
            note.SetNote(randomNote);
            spawnedNotes.Add(note);
        }

        spawnedNotes[0].SetAnimationState(true);
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
        scoreTextFeedback.text = "";
        currentNoteIndex = 0;
        foreach (Note note in spawnedNotes)
        {
            Destroy(note.gameObject);
        }

        spawnedNotes.Clear();
        SetCurrentClef();
        SpawnNotes();
    }

    public void OnPianoKeyPressed(KeyNote[] note)
    {
        if (currentNoteIndex >= spawnedNotes.Count) return;
        
        Color noteColor = GetNoteColor(note);

        spawnedNotes[currentNoteIndex].SetNoteColor(noteColor);
        spawnedNotes[currentNoteIndex].SetAnimationState(false);
        
		StopAllCoroutines();
        if (noteColor == correctNote)
        {
            StartCoroutine(SetFeedBackText("Correct! It's " + spawnedNotes[currentNoteIndex].Key));
        }
        else
        {
            StartCoroutine(SetFeedBackText("Wrong! It's " + spawnedNotes[currentNoteIndex].Key));
        }
        currentNoteIndex++;

        if (currentNoteIndex >= spawnedNotes.Count)
        {
            StartCoroutine(InitGame());
        }
        else
        {
            spawnedNotes[currentNoteIndex].SetAnimationState(true);
        }
    }

    public Color GetNoteColor(KeyNote[] note)
    {
        Color noteColor = wrongNote;
        foreach (var n in note)
        {
            if (spawnedNotes[currentNoteIndex].Key == n)
            {
                noteColor = correctNote;
                break;
            }
        }
        return noteColor;
    }
    
    public bool IsCorrectNote(KeyNote[] note)
    {
        foreach (var n in note)
        {
            if (spawnedNotes[currentNoteIndex].Key == n)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator SetFeedBackText(string text)
    {
        scoreTextFeedback.text = text;
        yield return new WaitForSeconds(feedbackDuration);
        scoreTextFeedback.text = "";
    }
}

[System.Serializable]
public struct Clef
{
    public ClefType type;
    public List<KeyNote> notes;
    public GameObject clefGameObject;
}