using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CounterUIAgent : MonoBehaviour
{
    [SerializeField] private MatchManager _matchManager;
    [SerializeField] private TMP_Text _text;

    public void SetCounter(int value)
    {
        _text.SetText($"{value}/{_matchManager.RequiredCount}");
    }

    private void Reset()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        SetCounter(0);
    }
}
