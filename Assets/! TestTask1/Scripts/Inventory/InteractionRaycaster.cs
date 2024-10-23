using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.UIElements;
using UnityEngine;

public class InteractionRaycaster : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private string _layerName;
    [SerializeField] private Camera _camera;
    private int _layerMask;
    private Interactable _currentTarget;
    private int _cachedInstanceId;

    private void Start()
    {
        _layerMask = 1 << LayerMask.NameToLayer(_layerName);
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, _distance, _layerMask))
        {
            if (_currentTarget != null && 
                _cachedInstanceId == hitInfo.colliderInstanceID && 
                _currentTarget.InteractionsEnabled)
            {
                return;
            }

            if (TrySetTarget(hitInfo.collider.gameObject))
            {
                _cachedInstanceId = hitInfo.colliderInstanceID;
            }
            return;
        }
        RemoveTarget();
    }

    private bool TrySetTarget(GameObject gameObject)
    {
        RemoveTarget();
        var interactable = gameObject.GetComponent<Interactable>();
        if (interactable == null || !interactable.InteractionsEnabled)
        {
            return false;
        }
        interactable.EnableHighlight();
        _currentTarget = interactable;
        return true;
    }

    private void RemoveTarget()
    {
        if (_currentTarget == null)
        {
            return;
        }
        else if (_currentTarget.IsDestroyed())
        {
            _currentTarget = null;
            return;
        }
        _currentTarget.DisableHighlight();
        _cachedInstanceId = 0;
        _currentTarget = null;
    }
}