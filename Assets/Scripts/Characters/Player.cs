using System;
using Ability;
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
        [SerializeField] private Vampiric _vamiricAbility;

        private const string CommandHorizontal = "Horizontal";
        private const string CommandJump = "Jump";

        public event Action Jumped;
        public event Action Attacked;
        public event Action Died;

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
            CanSpellUse = true;
        }

        private void Update()
        {
            if (IsAlive)
                Died?.Invoke();
            else
            {
                IsRunning = Input.GetAxis(CommandHorizontal) != 0;

                if (Input.GetButton(CommandHorizontal))
                    Run();

                if (Input.GetButtonDown(CommandJump) && IsGrounded)
                    Jump();

                if (Input.GetMouseButtonDown(0) && CanSpellUse)
                {
                    StartAttackCooldown();

                    Attacked?.Invoke();
                }

                if (Input.GetKeyDown(KeyCode.Y))
                    _vamiricAbility.ActivationAbility();

                Debug.Log(_inventory.CoinCount);
            }
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

            Jumped?.Invoke();
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