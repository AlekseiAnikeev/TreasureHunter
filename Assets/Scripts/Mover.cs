using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private PlayerInputAction _playerInput;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        
        _playerInput = new PlayerInputAction();
        _playerInput.Enable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidBody.MovePosition(_rigidBody.position + GetMovementVector() * (_moveSpeed * Time.fixedDeltaTime));
    }

    private Vector2 GetMovementVector()
    {
        return _playerInput.Player.Move.ReadValue<Vector2>();
    }
}
