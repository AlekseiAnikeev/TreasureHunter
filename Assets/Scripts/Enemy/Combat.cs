using Characters;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyAI))]
    public class Combat : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private const string IsAttack = "IsAttack";
        private const string IsRun = "IsRunning";

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
                
                if (Player.Instance.IsAlive == false)
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