using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField]
    private AudioSource footstep;
    [SerializeField]
    private AudioClip[] steps;

    void FootStep()
    {
        footstep.clip = steps[Random.Range(0, steps.Length)];
        footstep.Play();
    }
}
