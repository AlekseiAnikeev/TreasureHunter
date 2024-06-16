using System;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Inventory))]
    public class Player : Entity
    {
        public static Player Instance { get; private set; }

        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _jumpForce = 11.2f;

        private const string CommandHorizontal = "Horizontal";
        private const string CommandJump = "Jump";

        public event Action OnJump;
        public event Action OnAttack;
        public event Action OnDie;

        public bool IsRunning { get; private set; }
        public bool IsGrounded { get; private set; }
        public Vector3 Direction { get; private set; }

        private Rigidbody2D _rigidBody;
        private Inventory _inventory;

        private void Awake()
        {
            Instance = this;
            _rigidBody = GetComponent<Rigidbody2D>();
            _inventory = GetComponent<Inventory>();

            _currentHealth = _maxHealth;
        }

        private void Update()
        {
            if (IsAlive)
                OnDie?.Invoke();

            IsRunning = Input.GetAxis(CommandHorizontal) != 0;

            if (Input.GetButton(CommandHorizontal))
                Run();

            if (Input.GetButtonDown(CommandJump) && IsGrounded)
                Jump();

            if (Input.GetMouseButtonDown(0))
                OnAttack?.Invoke();

            Debug.Log(_inventory.CoinCount);
        }

        public void Healing(float healingRate)
        {
            if (_currentHealth + healingRate <= _maxHealth)
                _currentHealth += healingRate;
            else
                _currentHealth = _maxHealth;
        }

        private void Run()
        {
            Direction = transform.right * Input.GetAxis(CommandHorizontal);

            transform.position =
                Vector3.MoveTowards(transform.position, transform.position + Direction, _moveSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            _rigidBody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);

            IsGrounded = false;

            OnJump?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Ground _))
            {
                IsGrounded = true;
            }
        }
    }
}