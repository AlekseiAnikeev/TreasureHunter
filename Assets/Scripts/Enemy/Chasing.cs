using Characters;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    [RequireComponent(typeof(Animator))]
    public class Chasing : MonoBehaviour
    {
        [SerializeField] private float _chasingSpeed = 1.5f;

        private const string IsRun = "IsRunning";

        private EnemyAI _enemyAI;
        private Animator _animator;

        private void Start()
        {
            _enemyAI = GetComponent<EnemyAI>();
            _animator = GetComponent<Animator>();
        }

        public void ChasingTarget()
        {
            _animator.SetBool(IsRun, _enemyAI.IsRunning);

            transform.position =
                Vector3.MoveTowards(transform.position, Player.Instance.transform.position,
                    (_enemyAI.Speed + _chasingSpeed) * Time.deltaTime);

            _enemyAI.ChangeSpriteDirection(Player.Instance.transform.position);
        }
    }
}