public class PlayerLook
{
    private UnityEngine.Transform _cameraTr;
    private UnityEngine.Transform _playerTr;

    private float _currentRotation = 0.0f;

    public PlayerLook(UnityEngine.Transform cameraTr, UnityEngine.Transform playerTr)
    {
        _cameraTr = cameraTr;
        _playerTr = playerTr;
    }

    public void LookByDirection(UnityEngine.Vector3 rotateDir)
    {
        float speed = Utils.PlayerConstants.PLAYER_LOOK_SPEED;

        _currentRotation -= rotateDir.y;
        _currentRotation = UnityEngine.Mathf.Clamp(_currentRotation, -90.0f, 90.0f);
        _cameraTr.localRotation = UnityEngine.Quaternion.Euler(_currentRotation, 0.0f, 0.0f);

        UnityEngine.Vector3 playerRotateDir = new UnityEngine.Vector3(0.0f, rotateDir.x, 0.0f);
        _playerTr.Rotate(playerRotateDir * speed * UnityEngine.Time.deltaTime);
    }
}
