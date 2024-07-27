
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;

    void Update()
    {
        Debug.Log("update 1");
        CheckInteraction();
        Debug.Log("update 2");
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
            Debug.Log("u1");
        }
        Debug.Log("u4");

    }
    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.Log(Camera.main.transform.position);
        Debug.Log(Camera.main.transform.forward);
        Debug.Log(ray);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            Debug.Log("1");
            if (hit.collider.tag == "Interactable")
            {
                Debug.Log("2");
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    Debug.Log("3");
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    Debug.Log("4");
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    Debug.Log("e1");
                    DisableCurrentInteractable();
                }
            }
            else
            {
                Debug.Log("e2");
                Debug.Log(hit.collider.name);
                Debug.Log(hit.collider.gameObject);
                DisableCurrentInteractable();

            }
        }
        else
        {
            Debug.Log("e3");
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.enableOutline();

    }
    void DisableCurrentInteractable()
    {



        currentInteractable.DisableOutline();
        currentInteractable = null;


    }
}