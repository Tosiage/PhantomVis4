using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TorsoPosition : MonoBehaviour {
    public bool targetStatus;
    private void Start()
    {
        
    }

    private void Update()
    {
        targetTracking(targetStatus);
        
    }

    private void targetTracking(bool Bool)
    {
       /* if (Bool == true)
        {
            //Debug.Log("moin");
            this.transform.position = GameObject.Find("Sphere_Astronaut").transform.position;
            Vector3 temp = new Vector3(0.25f, 0, 0);
            this.transform.position = this.transform.position + temp;
        }*/
    }

}
