using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;
    [SerializeField] TextMeshProUGUI coinCollectedText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject panel;

    void Start()
    {
        uIManager = this;
        panel.SetActive(false);
        coinCollectedText.text = "Coin x " + GameSession.gameSession.coinsCollected.ToString();
    }

    public void UpdateCoinText()
    {
        coinCollectedText.text = "Coin x " + GameSession.gameSession.coinsCollected.ToString();
    }
    public void OpenGameOverPanel()
    {
        panel.SetActive(true);
        gameOverText.text = "You Died \nCoin Collected " + GameSession.gameSession.coinsCollected.ToString(); 
    }
}
