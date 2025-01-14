using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public BoxCollider2D platformCollider;
    public GameObject player;

    private void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(player != null)
        {
            float playerY = player.transform.position.y;
            float platformY = transform.position.y;

            if(playerY > platformY)
            {
                platformCollider.enabled = true;
            }
            else
            {
                platformCollider.enabled = false;
            }
        }
    }
}
