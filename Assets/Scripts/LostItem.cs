using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostItem : MonoBehaviour
{
    [SerializeField]
    public string myName = "item";
    public Transform follow;
    public GameObject owner;

    void Start()
    {
        follow = transform;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        owner.GetComponent<NPCController>().myObject = gameObject;
    }

    void Update()
    {
        transform.position = follow.position;
    }
}
