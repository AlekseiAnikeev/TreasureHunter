using Characters;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.TryGetComponent(out Inventory inventory))
            {
                inventory.IncreaseCoinCount();
            }

            Destroy(gameObject);
        }
    }
}