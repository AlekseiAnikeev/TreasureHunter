using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ability
{
    public class EnemyDetector : MonoBehaviour
    {
        private List<Entity> _enemy = new();

        public Entity FindClosestEnemy()
        {
            return _enemy.Count > 0
                ? _enemy.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).First()
                : null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Entity enemy))
                _enemy.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Entity enemy))
                _enemy.Remove(enemy);
        }
    }
}