using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Draggable : MonoBehaviour
{
    readonly struct RigidBodyState
    {
        public readonly float Drag;
        public readonly RigidbodyConstraints Constraints;
        public readonly Transform Parent;
        public readonly bool UseGravity;

        public RigidBodyState(float drag, RigidbodyConstraints constraints, Transform parent, bool useGravity)
        {
            Drag = drag;
            Constraints = constraints;
            Parent = parent;
            UseGravity = useGravity;
        }

        public static RigidBodyState FromRigidBody(Rigidbody rigidbody)
        {
            return new RigidBodyState(rigidbody.drag, rigidbody.constraints, rigidbody.transform.parent, rigidbody.useGravity);
        }
        
        public void Apply(Rigidbody rigidbody)
        {
            rigidbody.drag = Drag;
            rigidbody.constraints = Constraints;
            rigidbody.transform.parent = Parent;
            rigidbody.useGravity = UseGravity;
        }
    }

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _holdingDrag = 10f;
    [SerializeField] private float _maxDistance = 0.1f;
    [SerializeField] private float _pickupForce = 4f;

    private RigidBodyState _cachedState;

    private Transform Area => transform.parent;

    public void Drag(Transform target)
    {
        transform.parent = target;
        _rigidBody.useGravity = false;
        _rigidBody.drag = _holdingDrag;
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;

        enabled = true;
    }

    public void Drop()
    {
        _cachedState.Apply(_rigidBody);

        enabled = false;
    }

    private void Awake()
    {
        _cachedState = RigidBodyState.FromRigidBody(_rigidBody);
        enabled = false;
    }

    private void Reset()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Area == null)
        {
            Drop();
            return;
        }
        var delta = Area.position - transform.position;
        if (delta.magnitude > _maxDistance)
        {
            _rigidBody.velocity = delta * _pickupForce;
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }
}