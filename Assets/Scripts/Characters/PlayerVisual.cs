using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerVisual : MonoBehaviour
    {
        private const string IsRunning = "IsRunning";
        private const string IsGrounded = "IsGrounded";
        private const string IsJump = "Jump";
        private const string IsAttack = "IsAttack";
        private const string IsTakeHit = "TakeHit";
        private const string IsDie = "IsDie";

        [SerializeField] private float _damage;
        [SerializeField] private Player _player;

        private Animator _animator;
        private PolygonCollider2D _polygonCollider;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _polygonCollider = GetComponent<PolygonCollider2D>();
        }

        private void Update()
        {
            _animator.SetBool(IsRunning, _player.IsRunning);
            _animator.SetBool(IsGrounded, _player.IsGrounded);

            transform.rotation = Quaternion.Euler(0, _player.Direction.x > 0f ? 0 : 180, 0);
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
            Destroy(_player.gameObject);
        }

        private void OnEnable()
        {
            _player.TakeHit += TakeHit;
            _player.Jumped += Jump;
            _player.Attacked += Attack;
            _player.Died += Death;
        }

        private void OnDisable()
        {
            _player.TakeHit -= TakeHit;
            _player.Jumped -= Jump;
            _player.Attacked -= Attack;
            _player.Died -= Death;
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
            _animator.SetTrigger(IsAttack);
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