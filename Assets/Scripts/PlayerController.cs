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
    [SerializeField]
    private Transform carryPoint;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject magnifier;

    private float xSpeed, ySpeed;
    private Rigidbody2D rb;
    public GameObject carrying;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (carrying)
        {
            LostItem item = carrying.GetComponent<LostItem>();
            Vector3 target = item.owner.transform.position;
            Vector3 currentPos = transform.position;

            NPCController owner = item.owner.GetComponent<NPCController>();
            if (owner.hasTalked)
            {
                arrow.SetActive(true);
            }

            target.z = 0f;
            target.x = target.x - currentPos.x;
            target.y = target.y - currentPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            arrow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            magnifier.active = !magnifier.active;
        }

        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPoint.z = 25f;

        magnifier.transform.position = screenPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LostItem"))
        {
            carrying = collision.gameObject;
            LostItem itemCarried = carrying.GetComponent<LostItem>();
            itemCarried.follow = carryPoint;
        }
    }
}
