using UnityEngine;

public class PlayerInteraction
{
    private Camera _camera;
    private LayerMask _layerMask;
    private Transform _snapPoint;
    private BuildingEntity _buildingItem;
    private GameObject _aim;

    public PlayerInteraction(Camera camera, LayerMask layerMask, Transform snapPoint, GameObject aim)
    {
        _camera = camera;
        _layerMask = layerMask;
        _snapPoint = snapPoint;
        _aim = aim;
    }

    public void TryToInterract()
    {
        if (_buildingItem != null)
            return;

        Ray ray = _camera.ScreenPointToRay(_aim.transform.position);
        float dist = Utils.PlayerConstants.PLAYER_MAX_INTERRACTION_DISTANCE;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist, _layerMask))
        {
            BuildingEntity tempBuildingItem = hit.transform.gameObject.GetComponent<BuildingEntity>();
            if (tempBuildingItem != null)
            {
                tempBuildingItem.OnPicked(_snapPoint);
                _buildingItem = tempBuildingItem;
            }
        }
    }

    public void DropInteraction()
    {
        if (_buildingItem == null) return;

        bool isDropSucceded = _buildingItem.DetachFromTarget();
        if (isDropSucceded) _buildingItem = null;
    }

    public void RotateInteractionByMouseWheel(float mouseDelta)
    {
        if (_buildingItem == null) return;
        _buildingItem.RotateByWhellDelta(mouseDelta);
    }
}
