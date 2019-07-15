using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarisCalibration : MonoBehaviour {
    //PM = Polaris Marker, Box = Modell, Target = Vuforia Image Target
    Matrix4x4 PMtoBox;
    Matrix4x4 TargetToPM;
    Matrix4x4 TargetToBox;
    Vector3 posPMB;
    Vector3 posTPM;
    Vector3 posTB;
    Quaternion rotPMB;
    Quaternion rotTPM;
    Quaternion rotTB;
    Vector3 scale;
	// Use this for initialization
	void Start () {
        scale = Vector3.one;
        posPMB = new Vector3(-109.965297f, -88.147864f, -84.339106f);
        posTPM = new Vector3(-14.512811f, -214.783934f, 150.288691f);
        rotPMB = new Quaternion(0.482567f, -0.495161f, 0.506550f, 0.515123f);
        rotTPM = new Quaternion(0.703863f, 0.006356f, 0.709819f, 0.026352f);

        PMtoBox = Matrix4x4.TRS(posPMB, rotPMB, scale);
        TargetToPM = Matrix4x4.TRS(posTPM, rotTPM, scale);
        TargetToBox = PMtoBox * TargetToPM.inverse;
        rotTB = Quaternion.LookRotation(TargetToBox.GetColumn(2),TargetToBox.GetColumn(1));
        posTB = TargetToBox.GetColumn(3);
        Debug.Log("rotTB " + rotTB);
        Debug.Log("posTB " + posTB);
		
	}
	
	
}
