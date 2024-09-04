using UnityEngine;

public class SimpleSphere : BuildingEntity
{

    protected override void SetupVariables()
    {
        base.SetupVariables();
        _canBeDropped = false;
    }

    protected override void OnStainInContact(Collider other)
        => _canBeDropped = other.gameObject.tag.Equals(Utils.BuildingConstants.WALL_TAG);

    protected override bool CanBeDetached()
        => _canBeDropped && _isSnapped;
}
