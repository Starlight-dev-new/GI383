using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
    public void GravityObject()
    {
        gravity = !gravity;
        StartCoroutine(StartCountDown(10f));
        gravity = !gravity;
    }
    IEnumerator StartCountDown(float countTime)
    {
        yield return new WaitForSeconds(countTime);
    }
}
