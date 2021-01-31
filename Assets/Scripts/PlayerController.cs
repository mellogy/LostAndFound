using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private LayerMask solid;

    private float xSpeed, ySpeed;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        xSpeed = Input.GetAxisRaw("Horizontal") * walkSpeed * Time.deltaTime;
        ySpeed = Input.GetAxisRaw("Vertical") * walkSpeed * Time.deltaTime;

        if (!Physics2D.OverlapCircle(transform.position + new Vector3(xSpeed, ySpeed, 0), .1f, solid)) {
            transform.position += new Vector3(xSpeed, ySpeed, 0);
        }

        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));
    }
}
