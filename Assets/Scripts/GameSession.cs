using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession gameSession { get; private set; }
    [SerializeField] public int coinsCollected;
    int currentSceneIndex;
    [SerializeField] GameObject saveGameObject;

    void Awake()
    {
        if(gameSession != null && gameSession != this)
        {
            Destroy(this);
        }
        else
        {
            gameSession = this;
        }
        coinsCollected = 0;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToCoin(int coinToAdd)
    {
        coinsCollected += coinToAdd;
    }
    public void ReplayGameSession()
    {
        DontDestroyOnLoad(saveGameObject);
        SceneManager.LoadScene(currentSceneIndex);
    }
}
