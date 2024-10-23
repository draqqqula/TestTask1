using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class MatchManager : MonoBehaviour
{
    public UnityEvent OnVictory;
    [SerializeField] public int RequiredCount;
    public void UpdateCounter(int value)
    {
        if (value >= RequiredCount)
        {
            OnVictory?.Invoke();
        }
    }
}