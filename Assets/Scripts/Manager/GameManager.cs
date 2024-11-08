using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }
void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        LevelManager = GetComponentInChildren<LevelManager>();


        DontDestroyOnLoad(GameObject.FindWithTag("Player"));
        DontDestroyOnLoad(GameObject.FindWithTag("MainCamera"));
    }
}