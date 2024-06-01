using Player;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMove player))
        {
            if (player.TryGetComponent(out Inventory inventory))
            {
                inventory.IncreaseCoinCount();
            }

            Destroy(gameObject);
        }
    }
}