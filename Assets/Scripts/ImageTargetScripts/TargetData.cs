using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetData : MonoBehaviour {
    public Vector3 relativePos;
    public float angleX;
    public float angleY;
    public float angleZ;
    [HideInInspector] public bool isVisible;
    public bool calibrated;

    //Durch Kalibrierung mit der Polaris werden die Werte für die relative Position und relative Rotation gesetzt
    public TargetData(Vector3 relativePos, int angleX, int angleY, int angleZ, bool isVisible, bool calibrated)
    {
        this.relativePos = relativePos;
        this.isVisible = isVisible;
        this.calibrated = calibrated;
        this.angleX = angleX;
        this.angleY = angleY;
        this.angleZ = angleZ;
    }
	
}
