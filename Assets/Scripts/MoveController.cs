using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    [SerializeField]private int speed;
    [SerializeField]private float turnspeed=20f;
    void Update()
    {
     horizontalInput=Input.GetAxis("Horizontal"); 
     verticalInput = Input.GetAxis("Vertical");
     
     this.transform.Translate(speed*Time.deltaTime*Vector3.right*verticalInput);
     this.transform.Translate(speed*Time.deltaTime* Vector3.forward*horizontalInput);
     //S=v*t (Vector)
    }
}
