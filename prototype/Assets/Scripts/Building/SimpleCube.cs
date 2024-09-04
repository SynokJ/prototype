using UnityEngine;

public class SimpleCube : BuildingEntity
{
    private void OnTriggerExit(Collider other)
    {
        _canBeDropped = true;
        _isSnapped = false;
    }

    protected override void OnStainInContact(Collider other)
       => _canBeDropped = !_collider.bounds.Intersects(other.bounds);

    protected override bool CanBeDetached()
       => _canBeDropped && !_isSnapped;
}
