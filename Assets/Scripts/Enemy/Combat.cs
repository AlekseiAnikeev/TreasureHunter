using Characters;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class Combat : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _attackRate = 2f;

        private const string IsAttack = "IsAttack";
        private const string IsRun = "IsRunning";

        private Animator _animator;
        private float _nextAttackTime = 0f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            if (Time.time > _nextAttackTime)
            {
                _animator.SetBool(IsRun, false);

                if (Player.Instance.IsAlive == false)
                    _animator.SetTrigger(IsAttack);

                _nextAttackTime = Time.time + _attackRate;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
                player.TakeDamage(_damage);
        }
    }
}