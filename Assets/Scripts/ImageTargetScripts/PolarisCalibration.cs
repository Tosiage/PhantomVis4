using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarisCalibration : MonoBehaviour
{
    // Sizes: 0.525 , 0.18 , 0.345
    TargetData td;
    List<TargetData> targetDatas;
    //PM = Polaris Marker, Box = Modell, Target = Vuforia Image Target
    Matrix4x4 PMtoBox;
    Matrix4x4 PMtoTarget;
    Matrix4x4 TargetToBox;
    Vector3 posPMBold;
    Vector3 posPMTold;
    Vector3 posPMB;
    Vector3 posTPM;
    Vector3 posTB;
    Quaternion rotPMBold;
    Quaternion rotPMTold;
    Quaternion rotPMB;
    Quaternion rotTPM;
    Quaternion rotTB;
    Vector3 scale;
    // Use this for initialization
    void Start()
    {
        targetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;
        foreach (TargetData td in targetDatas)
        {


            scale = Vector3.one;
            posPMBold = new Vector3(-109.965297f, -88.147864f, -84.339106f); //PolarisMarker zu Box
            posPMTold = td.posPMTold; //PolarisMarker to Target
            rotPMBold = new Quaternion(0.482567f, -0.495161f, 0.506550f, 0.515123f); //PolarisMarker zu Box
            rotPMTold = td.rotPMTold; //PolarisMarker to Target
                                      //rotPMB = rightCoordToUnityCord(rotPMBold);
                                      //rotTPM = rightCoordToUnityCord(rotTPMold);
                                      /*rotPMB = rotPMBold;
                                      rotTPM = rotPMTold;
                                      posPMB = posPMBold;
                                      posTPM = posPMTold;*/
                                      //posPMB = ConvertRightHandedToLeftHandedVector(posPMBold);
                                      //posTPM = ConvertRightHandedToLeftHandedVector(posTPMold);

            PMtoBox = Matrix4x4.TRS(posPMBold, rotPMBold, scale);
            PMtoTarget = Matrix4x4.TRS(posPMTold, rotPMTold, scale);

            TargetToBox = PMtoBox * PMtoTarget.inverse;

            /*qw= √(1 + m00 + m11 + m22) /2
    qx = (m21 - m12)/( 4 *qw)
    qy = (m02 - m20)/( 4 *qw)
    qz = (m10 - m01)/( 4 *qw)*/
            /*var qw = Mathf.Sqrt(1 + TargetToBox[0, 0] + TargetToBox[1, 1] + TargetToBox[2, 2]) / 2;
            var qx = (TargetToBox[2, 1] - TargetToBox[1, 2]) / (4 * qw);
            var qy = (TargetToBox[0, 2] - TargetToBox[2, 0]) / (4 * qw);
            var qz = (TargetToBox[1, 0] - TargetToBox[0, 1]) / (4 * qw);*/

            //rotTB = Quaternion.LookRotation(TargetToBox.GetColumn(2), TargetToBox.GetColumn(1));
            //rotTB = new Quaternion(qx, qy, qz, qw);
            var scale2 = new Vector3(1, -1, 1);
            var pos = new Vector3(2.337769f, 1.090332f, -0.214233f);
            var Polaris = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(180, 0, 0), scale);
            var MirrorY = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale2);

            var TargetToBoxFinal = MirrorY * Polaris * TargetToBox;
            rotTB = rotMatToQuad(TargetToBox);
            posTB = TargetToBox.GetColumn(3);

            /* var rotPMBnew = Quaternion.LookRotation(PMtoBox.GetColumn(2), PMtoBox.GetColumn(1));
             Debug.Log(rotPMBnew);
             var posPMBnew = PMtoBox.GetColumn(3);
             Debug.Log(posPMBnew);*/
           // td.relativePosTemp = Vector3.zero;
            //td.relativeRotTemp = Quaternion.identity;
            td.relativePos = posTB * 0.001f;
            td.relativeRot = rotTB;
        }


    }

   

    private Quaternion rightCoordToUnityCord(Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }

    private Vector3 ConvertRightHandedToLeftHandedVector(Vector3 rightHandedVector)
    {
        return new Vector3(rightHandedVector.x, rightHandedVector.z, rightHandedVector.y);
    }

    public static Quaternion rotMatToQuad(Matrix4x4 rot)
    {
        float qw, qx, qy, qz;
        // qw = 0.5*sqrt(1.0 + rot.r1 + rot.r5 + rot.r9);
        // qx = (rot.r8 - rot.r6)/(4.0*qw);
        // qy = (rot.r3 - rot.r7)/(4.0*qw);
        // qz = (rot.r4 - rot.r2)/(4.0*qw);
        float r1 = rot[0, 0];
        float r2 = rot[0, 1];
        float r3 = rot[0, 2];
        float r4 = rot[1, 0];
        float r5 = rot[1, 1];
        float r6 = rot[1, 2];
        float r7 = rot[2, 0];
        float r8 = rot[2, 1];
        float r9 = rot[2, 2];

        float tr = r1 + r5 + r9;

        if (tr > 0)
        {
            float S = Mathf.Sqrt(tr + 1.0f) * 2; // S=4*qw
            qw = 0.25f * S;
            qx = (r8 - r6) / S;
            qy = (r3 - r7) / S;
            qz = (r4 - r2) / S;
        }
        else if ((r1 > r5) & (r1 > r9))
        {
            float S = Mathf.Sqrt(1.0f + r1 - r5 - r9) * 2; // S=4*qx
            qw = (r8 - r6) / S;
            qx = 0.25f * S;
            qy = (r2 + r4) / S;
            qz = (r3 + r7) / S;
        }
        else if (r5 > r9)
        {
            float S = Mathf.Sqrt(1.0f + r5 - r1 - r9) * 2; // S=4*qy
            qw = (r3 - r7) / S;
            qx = (r2 + r4) / S;
            qy = 0.25f * S;
            qz = (r6 + r8) / S;
        }
        else
        {
            float S = Mathf.Sqrt(1.0f + r9 - r1 - r5) * 2; // S=4*qz
            qw = (r4 - r2) / S;
            qx = (r3 + r7) / S;
            qy = (r6 + r8) / S;
            qz = 0.25f * S;
        }
        Quaternion q = new Quaternion(qx, qy, qz, qw);
        return q;
    }


}
