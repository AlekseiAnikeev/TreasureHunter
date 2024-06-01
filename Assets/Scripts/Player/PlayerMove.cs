using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private const string CommandIsRunning = "isRunning";

        private SpriteRenderer _renderer;
        private PlayerInputAction _playerInput;
        private Rigidbody2D _rigidBody;
        private Animator _animator;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
        
            _playerInput = new PlayerInputAction();
            _playerInput.Enable();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 inputVector = GetMovementVector();
            
            bool isRunning;
            float minMoveSpeed = 0.1f;
        
            isRunning = Mathf.Abs(inputVector.x) > minMoveSpeed;

            if (inputVector.x < 0)
                _renderer.flipX = true;
            else
                _renderer.flipX = false;
        
            _rigidBody.MovePosition(_rigidBody.position + inputVector * (_moveSpeed * Time.fixedDeltaTime));
        
            _animator.SetBool(CommandIsRunning, isRunning);
        }

        private Vector2 GetMovementVector()
        {
            return _playerInput.Player.Move.ReadValue<Vector2>();
        }
    }
}
