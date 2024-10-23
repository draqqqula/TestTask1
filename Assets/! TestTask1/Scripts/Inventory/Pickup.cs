using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Draggable), typeof(Interactable))]
internal class Pickup : MonoBehaviour
{
    [SerializeField] private Draggable _draggable;
    [SerializeField] private Interactable _interactable;
    [SerializeField] private Transform _holdArea;
    [SerializeField] private string _layerName = "Items";
    [SerializeField] private string _tag = "Item";

    private void Reset()
    {
        _draggable = GetComponent<Draggable>();
        _interactable = GetComponent<Interactable>();
        gameObject.layer = LayerMask.NameToLayer(_layerName);
        gameObject.tag = _tag;
    }

    private void Start()
    {
        _interactable.OnInteraction.AddListener(HandleInteraction);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Drop();
        }
    }

    private void HandleInteraction()
    {
        _draggable.Drag(_holdArea);
        _interactable.InteractionsEnabled = false;
        enabled = true;
    }

    private void Drop()
    {
        _draggable.Drop();
        _interactable.InteractionsEnabled = true;
        enabled = false;
    }
}