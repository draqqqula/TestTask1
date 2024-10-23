using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteraction;
    [SerializeField] public bool InteractionsEnabled = true;
    [SerializeField] private Outline _outline;

    public void EnableHighlight()
    {
        _outline.enabled = true;
        enabled = true;
    }

    public void DisableHighlight()
    {
        _outline.enabled = false;
        if (enabled)
        {
            enabled = false;
        }
    }

    private void Reset()
    {
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        enabled = false;
    }

    private void Update()
    {
        if (InteractionsEnabled && Input.GetKeyDown(KeyCode.E))
        {
            OnInteraction?.Invoke();
        }
    }

    private void OnDisable()
    {
        DisableHighlight();
    }
}
