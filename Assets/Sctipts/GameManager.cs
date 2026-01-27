using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("สถานะผู้เล่น")]
    public bool isdead = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
