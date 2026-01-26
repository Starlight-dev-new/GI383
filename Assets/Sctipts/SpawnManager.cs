using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distancePlayer = 8f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, player.position.y+distancePlayer ,0);
    }
}
