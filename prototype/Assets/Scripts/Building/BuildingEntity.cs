using UnityEngine;

public abstract class BuildingEntity : MonoBehaviour
{
    private Vector3 _normal;

    protected Rigidbody _rb;
    protected Renderer _renderer;
    protected Collider _collider;
    protected Transform _targetTr;
    protected bool _canBeDropped;
    protected bool _isSnapped;

    protected virtual void SetupVariables()
    {
        _canBeDropped = true;
        _isSnapped = false;
    }

    protected abstract void OnStainInContact(Collider other);
    protected abstract bool CanBeDetached();

    public void OnPicked(Transform snapPoint)
        => _targetTr = snapPoint;

    #region Collider not trigger
    //private void OnCollisionEnter(Collision collision)
    //{
    //    _normal = collision.contacts[0].normal;
    //    _isConnected = true;
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (!_collider.bounds.Intersects(collision.collider.bounds))
    //        _canBeDropped = true;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    _isConnected = false;
    //}
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        Vector3 colPoint = other.ClosestPoint(transform.position);
        _normal = transform.position - colPoint;

        _isSnapped = true;
    }

    private void OnTriggerStay(Collider other)
       => OnStainInContact(other);

    private void OnTriggerExit(Collider other)
        => _isSnapped = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        SetupVariables();
    }

    private void Update()
    {
        if (_targetTr == null) return;

        if (_collider.bounds.Contains(_targetTr.position) && _isSnapped)
        {
            Vector3 normalDir = GenerateNormalDirection();
            _renderer.material.color = CanBeDetached() ? Color.green : Color.red;
            MoveByNormal(normalDir);
        }
        else
        {
            float speed = Utils.BuildingConstants.MOVEMENT_SPEED * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, _targetTr.position, speed);
            _renderer.material.color = CanBeDetached() ? Color.green : Color.gray;
            _rb.velocity = Vector3.zero;
        }
    }

     public bool DetachFromTarget()
    {
        if (CanBeDetached())
        {
            _targetTr = null;
            _renderer.material.color = Color.white;
            return true;
        }

        return false;
    }
    
    private Vector3 GetDestinationPosByNormal(Vector3 dir)
        => dir - Vector3.Dot(dir, _normal) * _normal;

    private void MoveByNormal(Vector3 normalDir)
    {
        _rb.velocity = Vector3.zero;
        _rb.MovePosition(_rb.position + normalDir);
        transform.rotation = Quaternion.LookRotation(_normal * 3.0f);
    }

    private Vector3 GenerateNormalDirection()
    {
        Vector3 dir = (_targetTr.position - transform.position).normalized;
        float speed = Utils.BuildingConstants.MOVEMENT_SPEED * Time.deltaTime;
        return GetDestinationPosByNormal(dir) * speed;
    }

    public void RotateByWhellDelta(float delta)
    {
        float speed = Utils.BuildingConstants.ROTATION_SPEED * Time.deltaTime;
        transform.Rotate(Vector3.up * delta * speed);
    }
}
