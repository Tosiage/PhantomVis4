using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetData : MonoBehaviour {
    public Vector3 relativePos;
    [HideInInspector] public bool isVisible;
   

    public TargetData(Vector3 relativePos, bool isVisible)
    {
        this.relativePos = relativePos;
        this.isVisible = isVisible;
    }
	
}
