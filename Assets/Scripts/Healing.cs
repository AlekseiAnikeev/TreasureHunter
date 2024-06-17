using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private float _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Characters.Player player))
        {
            Destroy(gameObject);
            
            Debug.Log($"Восстановлено {_health} hp");

            player.Heal(_health);
        }
    }
}