using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientInteract : MonoBehaviour
{
    private Animator anim;
    public int behindPlayer = -5;
    public int beforePlayer = 8;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            anim.ResetTrigger("Bop");
            anim.SetTrigger("Bop");
        }
    }
}
