using Characters;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator), typeof(EnemyAI))]
    public class Combat : MonoBehaviour
    {
        private const string IsAttack = "IsAttack";
        private const string IsRun = "IsRunning";

        [SerializeField] private int _damage;
        [SerializeField] private Player _player;

        private Animator _animator;
        private EnemyAI _enemyAi;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _enemyAi = GetComponent<EnemyAI>();
        }

        public void Attack()
        {
            if (_enemyAi.CanSpellUse)
            {
                _animator.SetBool(IsRun, false);

                if (_player.IsAlive == false)
                    _animator.SetTrigger(IsAttack);

                _enemyAi.StartAttackCooldown();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
                player.TakeDamage(_damage);
        }
    }
}