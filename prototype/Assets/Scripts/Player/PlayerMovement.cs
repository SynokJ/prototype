public class PlayerMovement
{
    private UnityEngine.CharacterController _characterController;
    private UnityEngine.Transform _playerTr;

    public PlayerMovement(UnityEngine.CharacterController characterController, UnityEngine.Transform playerTr)
    {
        _characterController = characterController;
        _playerTr = playerTr;
    }

    public void MoveByDirection(UnityEngine.Vector3 dir)
    {
        float speed = Utils.PlayerConstants.PLAYER_MOVEMENT_SPEED * UnityEngine.Time.deltaTime;
        _characterController.Move((_playerTr.forward * dir.z + _playerTr.right * dir.x) * speed);
    }
}
