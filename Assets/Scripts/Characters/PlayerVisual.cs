using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private float _spellCooldown = 2f;
        [SerializeField] private float _damage;

        private const string IsRunning = "IsRunning";
        private const string IsGrounded = "IsGrounded";
        private const string IsJump = "Jump";
        private const string IsAttack = "IsAttack";
        private const string IsTakeHit = "TakeHit";
        private const string IsDie = "IsDie";

        private Animator _animator;
        private PolygonCollider2D _polygonCollider;

        private float _nextAttackTime;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _polygonCollider = GetComponent<PolygonCollider2D>();
        }

        private void Update()
        {
            _animator.SetBool(IsRunning, Player.Instance.IsRunning);
            _animator.SetBool(IsGrounded, Player.Instance.IsGrounded);

            transform.rotation = Quaternion.Euler(0, Player.Instance.Direction.x > 0f ? 0 : 180, 0);
        }

        public void PolygonColliderTurnOn()
        {
            _polygonCollider.enabled = true;
        }

        public void PolygonColliderTurnOff()
        {
            _polygonCollider.enabled = false;
        }

        public void DestroyGameObject()
        {
            Destroy(Player.Instance.gameObject);
        }

        private void OnEnable()
        {
            Player.Instance.OnTakeHit += TakeHit;
            Player.Instance.OnJump += Jump;
            Player.Instance.OnAttack += Attack;
            Player.Instance.OnDie += Death;
        }

        private void OnDisable()
        {
            Player.Instance.OnTakeHit -= TakeHit;
            Player.Instance.OnJump -= Jump;
            Player.Instance.OnAttack -= Attack;
            Player.Instance.OnDie -= Death;
        }

        private void TakeHit()
        {
            _animator.SetTrigger(IsTakeHit);
        }

        private void Jump()
        {
            _animator.SetTrigger(IsJump);
        }

        private void Death()
        {
            _animator.SetBool(IsDie, true);
        }

        private void Attack()
        {
            if (Time.time > _nextAttackTime)
            {
                _animator.SetTrigger(IsAttack);

                _nextAttackTime = Time.time + _spellCooldown;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Entity enemyEntity))
            {
                enemyEntity.TakeDamage(_damage);
            }
        }
    }
}