using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraTr;
    [SerializeField] private Transform _playerTr;
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private GameObject _aim;

    private PlayerInteraction _interaction;
    private PlayerMovement _movement;
    private PlayerInput _input;
    private PlayerLook _look;

    void Awake()
    {
        _input = new PlayerInput();
        _look = new PlayerLook(_cameraTr, _playerTr);
        _movement = new PlayerMovement(_characterController, _playerTr);
        _interaction = new PlayerInteraction(_camera, _layer, _snapPoint, _aim);
    }

    void Update()
    {
        if (_input.TryGetMovementInput(out Vector3 movementDir))
            _movement.MoveByDirection(movementDir);

        if (_input.TryGetLookInput(out Vector3 lookDir))
            _look.LookByDirection(lookDir);

        if (_input.TryGetMousePickUpAction())
            _interaction.TryToInterract();

        if (_input.TryGetMouseDropAction())
            _interaction.DropInteraction();

        if (_input.TryGetMouseRotateAction(out float mouseWheelDelta))
            _interaction.RotateInteractionByMouseWheel(mouseWheelDelta * 10.0f);
    }
}
