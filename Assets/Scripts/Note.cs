using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public enum NoteOverPosition
{
    None,
    LineUp,
    LineDown,
    LineCenter
}

public enum NoteAccidental
{
    None,
    Sharp,
    Flat
}
public class Note : MonoBehaviour
{
    [SerializeField] private KeyNote key;
    [SerializeField] private RectTransform gap;
    [SerializeField] private Image noteFillImage;
    [SerializeField] private PianoSamples pianoSamples;
    [SerializeField] private GameObject lineOverUp;
    [SerializeField] private GameObject lineOverDown;
    [SerializeField] private GameObject lineOverCenter;
    [SerializeField] private Transform sharpKey;
    [SerializeField] private Transform flatKey;

    private NoteAccidental accidental = NoteAccidental.None;
    public NoteAccidental Accidental => accidental;
    public KeyNote Key => key;
    public void SetNote(KeyNote note, NoteAccidental accidental = NoteAccidental.None)
    {
        key = note;
        SetGapHeight(pianoSamples.GetNoteHeight(note, ScoreDisplay.CurrentClef.type));
        SetOverLines(pianoSamples.GetNoteOverPosition(note, ScoreDisplay.CurrentClef.type));
        SetAccidental(accidental);
    }

    private void SetAccidental(NoteAccidental noteAccidental)
    {
        accidental = noteAccidental;
        if (noteAccidental == NoteAccidental.Sharp)
        {
            sharpKey.gameObject.SetActive(true);
        }
        else if (noteAccidental == NoteAccidental.Flat)
        {
            flatKey.gameObject.SetActive(true);
        }
    }

    private void SetGapHeight(float height)
    {
        gap.sizeDelta = new Vector2(gap.sizeDelta.x, height);
    }

    private void SetOverLines(NoteOverPosition position)
    {
        if (position == NoteOverPosition.None) return;
        if (position == NoteOverPosition.LineUp)
        {
            lineOverUp.SetActive(true);
        }
        else if (position == NoteOverPosition.LineDown)
        {
            lineOverDown.SetActive(true);
        }
        else if (position == NoteOverPosition.LineCenter)
        {
            lineOverCenter.SetActive(true);
        }
    }
    
    public void SetNoteColor(Color color)
    {
        noteFillImage.color = color;
        noteFillImage.enabled = true;
    }
}
