using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SmoothMoving = 4f;
    public float yOffset = 200f;

    private PlayerInputActions _playerInputActions;
    private Animator _animator;
    private Vector2 TouchPosition;
    private Camera _camera;
    private Vector2 OldTouchPosition = new Vector2(0, 0);
    private static readonly int Animation1 = Animator.StringToHash("Animation");


    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _camera = Camera.main;
    }

    private void Move()
    {
        Vector3 ScreenCoordinates = new Vector3(TouchPosition.x, TouchPosition.y + yOffset,
            Camera.main.nearClipPlane);
        Vector3 WorldCoordinates = _camera.ScreenToWorldPoint(ScreenCoordinates);
        WorldCoordinates.z = 0;

        Vector3 SmoothPosition = Vector3.Lerp(transform.position, WorldCoordinates,
            SmoothMoving * Time.fixedDeltaTime);
        transform.position = SmoothPosition;
    }

    void FixedUpdate()
    {
        TouchPosition = _playerInputActions.Player.Move.ReadValue<Vector2>();
        if (TouchPosition != Vector2.zero)
        {
            Move();
            SetAnimation();
        }
    }

    private void SetAnimation()
    {
        Vector2 Offset = TouchPosition - OldTouchPosition;
        if (Offset != Vector2.zero)
        {
            if (Offset.y > 0.1) _animator.SetInteger(Animation1, 0);
            if (Offset.y < -0.1) _animator.SetInteger(Animation1, 1);
            if (Offset.x > 5) _animator.SetInteger(Animation1, 2);
            if (Offset.x < -5) _animator.SetInteger(Animation1, 3);
        }
        else _animator.SetInteger(Animation1, 0);

        OldTouchPosition = TouchPosition;
    }
}