using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SmoothMoving = 4f;
    public float yOffset = 1000f;

    private PlayerInputActions _playerInputActions;
    private Animator _animator;
    private Vector2 TouchPosition;
    private Camera _camera;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _camera = Camera.main;
    }

    private void Move()
    {
        Vector3 ScreenCoordinates = new Vector3(TouchPosition.x, TouchPosition.y+yOffset,
            Camera.main.nearClipPlane);
        Vector3 WorldCoordinates = _camera.ScreenToWorldPoint(ScreenCoordinates);
        WorldCoordinates.z = 0;

        Vector3 SmoothPosition = Vector3.Lerp(transform.position, WorldCoordinates,
            SmoothMoving * Time.fixedDeltaTime);
        transform.position = SmoothPosition;
    }
    void FixedUpdate()
    {
        TouchPosition=_playerInputActions.Player.Move.ReadValue<Vector2>(); 
        Move();
    }
}