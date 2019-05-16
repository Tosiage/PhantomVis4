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
        if (Bool == true)
        {
            //Debug.Log("moin");
            var cube_Astronaut = GameObject.Find("Cube_Astronaut").transform;
            Vector3 temp = cube_Astronaut.TransformPoint(2, 0, 0);
            this.transform.position = temp;
            this.transform.rotation = cube_Astronaut.rotation;
            
        }
    }

}
