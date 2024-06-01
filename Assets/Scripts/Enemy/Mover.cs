using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform _pointContainer;
        [SerializeField] private float _speed;

        private const string CommandRun = "Run";

        private Transform[] _points;
        private SpriteRenderer _sprite;
        private Animator _animator;

        private int _currentPointIndex;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _points = new Transform[_pointContainer.childCount];

            for (int i = 0; i < _points.Length; i++)
                _points[i] = _pointContainer.GetChild(i);
        }

        private void Update()
        {
            Transform target = _points[_currentPointIndex];

            transform.position =
                Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            _sprite.flipX = transform.position.x > target.position.x;

            _animator.SetTrigger(CommandRun);

            if (transform.position == target.position)
                ChangeNextPlace();
        }

        private void ChangeNextPlace()
        {
            _currentPointIndex = ++_currentPointIndex % _points.Length;
        }
    }
}