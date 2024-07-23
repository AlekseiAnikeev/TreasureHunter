using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    public class EnemyDetected : MonoBehaviour
    {
        private List<Entity> _enemy = new();

        public List<Entity> Enemy => _enemy;

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