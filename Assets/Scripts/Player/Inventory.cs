using UnityEngine;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        public int CoinCount { get; private set; }

        public void IncreaseCoinCount()
        {
            CoinCount += 1;
        }
    }
}