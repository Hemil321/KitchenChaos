using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the animation script
//The animation will be invoked, when a certain event is fired off
//In this case, when the player picks an object from the container counter
public class ContainerCounterVisual : MonoBehaviour
{
    //This is the trigger parameter for the animation
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
