using Characters;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAI), typeof(Animator))]
    public class Chasing : MonoBehaviour
    {
        private const string IsRun = "IsRunning";

        [SerializeField] private float _chasingSpeed = 1.5f;
        [SerializeField] private Player _player;

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
                Vector3.MoveTowards(transform.position, _player.transform.position,
                    (_enemyAI.Speed + _chasingSpeed) * Time.deltaTime);

            _enemyAI.ChangeSpriteDirection(_player.transform.position);
        }
    }
}