using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ItemCounter : MonoBehaviour
{
    public UnityEvent<int> OnValueChanged;
    [SerializeField] private string _requiredTag;
    private int _counter = 0;

    private void TryChangeValue(Collider other, int amount)
    {
        if (other.tag == _requiredTag)
        {
            _counter += amount;
            OnValueChanged?.Invoke(_counter);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TryChangeValue(other, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        TryChangeValue(other, -1);
    }
}