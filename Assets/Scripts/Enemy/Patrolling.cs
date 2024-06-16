using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAI))]
    [RequireComponent(typeof(Animator))]
    public class Patrolling : MonoBehaviour
    {
        [SerializeField] private float _minPatrolDistance = 3f;
        [SerializeField] private float _maxPatrolDistance = 7f;
        [SerializeField] private float _maxPatrolTime = 2f;

        private const string IsRun = "IsRunning";
        
        public Vector3 TargetPosition { get; private set; }
        public float CurrentPatrolTime { get; private set; }

        private Vector3 _startingPosition;
        private EnemyAI _enemyAI;
        private Animator _animator;

        private float _accuracy = 0.00001f;

        private void Start()
        {
            _enemyAI = GetComponent<EnemyAI>();
            _animator = GetComponent<Animator>();

            CurrentPatrolTime = _maxPatrolTime;
            _startingPosition = transform.position;

            ChangeNextPlace();
        }

        private void Update()
        {
            if (Math.Abs(transform.position.x - TargetPosition.x) < _accuracy)
                CurrentPatrolTime -= Time.deltaTime;
        }

        public void Patrol()
        {
            _animator.SetBool(IsRun, Math.Abs(transform.position.x - TargetPosition.x) > _accuracy && _enemyAI.IsRunning);

            transform.position =
                Vector3.MoveTowards(transform.position, TargetPosition, _enemyAI.Speed * Time.deltaTime);

            CurrentPatrolTime -= Time.deltaTime;

            if (CurrentPatrolTime < 0)
            {
                ChangeNextPlace();

                CurrentPatrolTime = _maxPatrolTime;
            }

            _enemyAI.ChangeSpriteDirection(TargetPosition);
        }

        private void ChangeNextPlace()
        {
            TargetPosition = _startingPosition +
                             GetRandomDirection() * Random.Range(_minPatrolDistance, _maxPatrolDistance);
        }

        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0).normalized;
        }
    }
}