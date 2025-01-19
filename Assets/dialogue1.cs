using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueTrigger.Instance.CanPlayDialogue2 = true;
            Destroy(gameObject);
        }
    }
}
