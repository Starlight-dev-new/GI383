using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] GameObject[] tapGravity;

    public void HideTap(bool trunState){
        for(int i = 0;  i < tapGravity.Length ; i++)
        {
            tapGravity[i].SetActive(false);
        }
    }

}
