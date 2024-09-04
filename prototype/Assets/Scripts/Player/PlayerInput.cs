using UnityEngine;

public class PlayerInput
{
    public PlayerInput()
    {
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }

    public bool TryGetMovementInput(out UnityEngine.Vector3 movementDirection)
    {
        float horInput = UnityEngine.Input.GetAxis("Horizontal");
        float verInput = UnityEngine.Input.GetAxis("Vertical");
        movementDirection = new UnityEngine.Vector3(horInput, 0.0f, verInput);
        return !(horInput == 0 && verInput == 0);
    }

    public bool TryGetLookInput(out UnityEngine.Vector3 lookDirection)
    {
        float horMouse = UnityEngine.Input.GetAxis("Mouse X");
        float verMouse = UnityEngine.Input.GetAxis("Mouse Y");
        lookDirection = new UnityEngine.Vector3(horMouse, verMouse, 0.0f);
        return !(horMouse == 0 && verMouse == 0);
    }

    public bool TryGetMousePickUpAction()
        => UnityEngine.Input.GetMouseButtonUp(0);

    public bool TryGetMouseDropAction()
        => UnityEngine.Input.GetMouseButtonUp(1);
    
    public bool TryGetMouseRotateAction(out float scrollDelta)
    {
        scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        return !(scrollDelta == 0);
    }
}
