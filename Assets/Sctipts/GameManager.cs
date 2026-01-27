using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] InGameManager inGameManager;
    [Header("สถานะผู้เล่น")]
    public bool isdead = false;
    public bool gravity = false;

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
    public IEnumerator GravityObject(float countTime)
    {
        gravity = true;
        inGameManager.HideTap(false);
        yield return new WaitForSeconds(countTime);
        gravity = false;
        inGameManager.HideTap(true);

    }
}
