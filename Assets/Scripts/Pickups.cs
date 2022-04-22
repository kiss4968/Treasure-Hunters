using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] int valuePerCoin = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            GameSession.gameSession.AddToCoin(valuePerCoin);
            UIManager.uIManager.UpdateCoinText();
        }
    }
}
