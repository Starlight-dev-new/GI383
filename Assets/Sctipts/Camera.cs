using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform player;

    void LateUpdate()
    {
        if (GameManager.instance.isdead) return;
        transform.position = new Vector3(0, player.position.y , transform.position.z);
    }
}
    
