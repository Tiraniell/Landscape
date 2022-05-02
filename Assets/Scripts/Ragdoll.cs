using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody[] Rbs;
    public Collider[] Colls;

    public float killForce = 5f;

    private Animator anim;
    public ThirdPersonUserControl controller;
    public ThirdPersonCharacter characterController;

    private void Kill()
    {
        SetRagdoll(true);
        SetMain(false);
    }

    private void Revive()
    {
        SetRagdoll(false);
        SetMain(true);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        Rbs = GetComponentsInChildren<Rigidbody>();
        Colls = GetComponentsInChildren<Collider>();

        controller = GetComponent<ThirdPersonUserControl>();
        characterController = GetComponent<ThirdPersonCharacter>();
        Revive();
    }

    
    void SetRagdoll(bool active)
    {
        for (int i = 0; i < Rbs.Length; i++)
        {
            Rbs[i].isKinematic = !active;
            if (active)
            {
                Rbs[i].AddForce(Vector3.forward * killForce, ForceMode.Impulse);
            }
        }
        for (int i = 0; i < Colls.Length; i++)
        {
            Colls[i].enabled = active;           
        }
    }

    void SetMain(bool active)
    {
        
        anim.enabled = active;
        controller.enabled = active;
        characterController.enabled = active;
        
        Rbs[0].isKinematic = !active;
        Colls[0].enabled = active;

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Revive();
        }
    }
}
