using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameTrigger : MonoBehaviour
{
    public GameObject canvas;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canvas.SetActive(false);
    }
}
