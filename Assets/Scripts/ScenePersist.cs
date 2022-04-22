using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist instance;
    int numberOfScenePersist;
    private void Awake()
    {
        instance = this;
        numberOfScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if(numberOfScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
