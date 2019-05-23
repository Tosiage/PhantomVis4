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
            var targetTransform = GameObject.Find("Empty").transform;
            var temp = targetTransform.localToWorldMatrix * new Vector4(2, 0, 0, 1);
          //  Vector3 temp = targetTransform.TransformPoint(2, 0, 0);
           // this.transform.position = temp;
          //  Vector3 hoi = new Vector3(2, 0, 0);
           // Vector3 thePos = targetTransform.TransformPointUnscaled(hoi);
            //this.transform.position = thePos;
            //this.transform.rotation = targetTransform.rotation;

            //Vector 4 (2,0,0,1)
            //obj.transform.localtoworld
            
        }
    }

}
