using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Transform playerTransform;
    
    void Update()
    {
        if (GameManager.instance.isdead)return;
        scoreText.text = "Score: " + playerTransform.position.y.ToString("0");
    }

}
