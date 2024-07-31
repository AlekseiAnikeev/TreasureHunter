using Characters;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Patrolling), typeof(Chasing), typeof(Combat))]
    [RequireComponent(typeof(PolygonCollider2D), typeof(Animator))]
    public class EnemyAI : Entity
    {
        private const string IsTakeHit = "TakeHit";
        private const string IsIsDie = "IsDie";

        [SerializeField] private State _startingState;
        [SerializeField] private float _speed;
        [SerializeField] private float _chasingDistance = 4f;
        [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private Player _player;

        public float Speed { get; private set; }
        public bool IsRunning => _currentState is State.Patrol or State.Chasing;

        private PolygonCollider2D _polygonCollider;
        private Patrolling _patroling;
        private Chasing _chasing;
        private Combat _combat;
        private Animator _animator;
        private State _currentState;

        private void Awake()
        {
            _patroling = GetComponentInChildren<Patrolling>();
            _chasing = GetComponentInChildren<Chasing>();
            _combat = GetComponentInChildren<Combat>();
            _animator = GetComponent<Animator>();
            _polygonCollider = GetComponent<PolygonCollider2D>();

            _currentState = _startingState;
            Speed = _speed;
            _currentHealth = _maxHealth;
            CanSpellUse = true;
        }

        private void Update()
        {
            StateHandler();
        }

        public void ChangeSpriteDirection(Vector3 target)
        {
            transform.rotation = Quaternion.Euler(0, transform.position.x > target.x ? 0 : 180, 0);
        }

        public void PolygonColliderTurnOn()
        {
            _polygonCollider.enabled = true;
        }

        public void PolygonColliderTurnOff()
        {
            _polygonCollider.enabled = false;
        }

        private void OnEnable()
        {
            base.TakeHit += TakeHitAnimation;
        }

        private void OnDisable()
        {
            base.TakeHit -= TakeHitAnimation;
        }

        private void TakeHitAnimation()
        {
            _animator.SetTrigger(IsTakeHit);
        }

        private void StateHandler()
        {
            switch (_currentState)
            {
                case State.Patrol:
                    _patroling.Patrol();
                    break;

                case State.Chasing:
                    _chasing.ChasingTarget();
                    break;

                case State.Attack:
                    _combat.Attack();
                    break;

                case State.Death:
                    IsDie();
                    break;

                default:
                case State.Idle:
                    Idle();
                    break;
            }

            GetCurrentState();
        }

        private void Idle()
        {
            GetCurrentState();
        }

        private void IsDie()
        {
            _animator.SetBool(IsIsDie, IsAlive);
        }

        private void GetCurrentState()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            if (IsAlive)
            {
                _currentState = State.Death;

                return;
            }

            if (transform.position == _patroling.TargetPosition && _patroling.CurrentPatrolTime > 0)
                _currentState = State.Idle;
            else
                _currentState = State.Patrol;

            if (distanceToPlayer <= _chasingDistance && distanceToPlayer > _attackDistance)
                _currentState = State.Chasing;

            if (distanceToPlayer <= _attackDistance)
                _currentState = State.Attack;
        }
    }
}