using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float radius = 1f;
    public Transform interactionTransform;
    float distance;
    void Start()
    {
        distance = Vector3.Distance(player.position, interactionTransform.position);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
    void Update()
    {
        if (distance <= radius)
        {
            Debug.Log("check");
        }
    }
}
