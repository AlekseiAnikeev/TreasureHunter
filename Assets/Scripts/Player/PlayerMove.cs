using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Inventory))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _jumpForce = 11.2f;

        private const string CommandIsRunning = "Running";
        private const string CommandIsGrounded = "Grounded";
        private const string CommandHorizontal = "Horizontal";
        private const string CommandJump = "Jump";

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _rigidBody;
        private Inventory _inventory;

        private bool _isRunning;
        private bool _isGrounded;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            _isRunning = Input.GetAxis(CommandHorizontal) != 0;

            if (Input.GetButton(CommandHorizontal))
                Run();
            if (Input.GetButtonDown(CommandJump) && _isGrounded)
                Jump();

            _animator.SetBool(CommandIsRunning, _isRunning);
            _animator.SetBool(CommandIsGrounded, _isGrounded);

            Debug.Log(_inventory.CoinCount);
        }

        private void Run()
        {
            Vector3 direction = transform.right * Input.GetAxis(CommandHorizontal);

            transform.position =
                Vector3.MoveTowards(transform.position, transform.position + direction, _moveSpeed * Time.deltaTime);

            _spriteRenderer.flipX = direction.x < 0f;
        }

        private void Jump()
        {
            _rigidBody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger(CommandJump);
            _isGrounded = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Ground _))
            {
                _isGrounded = true;
            }
        }
    }
}