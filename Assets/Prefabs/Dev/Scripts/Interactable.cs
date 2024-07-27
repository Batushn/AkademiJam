using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
using System;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;
    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();

    }
    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;

    }
    public void enableOutline()
    {
        outline.enabled = true;
    }



}