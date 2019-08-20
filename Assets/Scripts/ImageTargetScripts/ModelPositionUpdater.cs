using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPositionUpdater : MonoBehaviour
{

    [HideInInspector] public Vector3 pos1;
    [HideInInspector] public Vector3 pos2;

    [HideInInspector] public List<Vector3> positions;
    [HideInInspector] public List<Quaternion> rotations;
    [HideInInspector] public Vector3 addedUpVectors;
    [HideInInspector] public Vector3 averagePos;
    public List<TargetData> targetDatas;
    public GameObject[] targetManagerTargets;

    //The averaged rotational value
    private Quaternion averageRotation;


    // Use this for initialization
    void Start()
    {


        positions = new List<Vector3>();
        rotations = new List<Quaternion>();
        targetManagerTargets = GameObject.Find("TargetManager").GetComponent<TargetManager>().targets;
        targetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;


    }

    // Update is called once per frame
    void Update()
    {
        //Zu Beginn jedes Updates Leeren der Listen, um zu vermeiden, dass Daten verfälscht werden
        //Liste würde sonst immer länger werden
        positions.Clear();
        rotations.Clear();
        addedUpVectors = Vector3.zero;

        //Wenn Target gerade sichtbar ist, wird Position des Targets relativ zum Phantom in position Liste abgespeichert
        //Rotation des Targets relativ zum Phantom wird in rotations Liste abgespeichert
        //Anschließend werden Mittelwerte beider Listen gebildet und dem GameObjekt des Phantoms zugewiesen
        foreach (TargetData td in targetDatas)
        {
            if (td.isVisible && td.initialCalibration || td.isVisible && td.calibrated)
            {
                var currentTransform = td.transform;
                // Quaternion offsetRot = Quaternion.Euler(td.angleX, td.angleY, td.angleZ);
                //Quaternion offsetRot = td.relativeRot;

                //wenn man die TargetData per hand eingibt, muss die offsetRot noch draufmultipliziert werden,
                //wenn man die TargetData mit Calibrate kalibriert, nicht.
               // Matrix4x4 Mrot = Matrix4x4.Rotate(currentTransform.rotation * td.relativeRot);
                Matrix4x4 Mrot = Matrix4x4.Rotate(currentTransform.rotation);
                Debug.Log("Mrot\n" + Mrot);
                Matrix4x4 Mtra = Matrix4x4.Translate(currentTransform.position);
                Debug.Log("Mtra\n" + Mtra);
                Vector3 posfinal = (Vector3)(Mtra * Mrot * new Vector4(td.relativePos.x, td.relativePos.y, td.relativePos.z, 1));
                positions.Add(posfinal);
                rotations.Add(currentTransform.rotation * td.relativeRot);

            }
        }

        foreach (Vector3 pos in positions)
        {
            addedUpVectors += pos;
        }
        averagePos = addedUpVectors / positions.Count;

        //https://forum.unity.com/threads/average-quaternions.86898/
        //http://wiki.unity3d.com/index.php/Averaging_Quaternions_and_Vectors
        //multipleRotations is an array which holds all the quaternions
        //which need to be averaged.

        //Global variable which holds the amount of rotations which
        //need to be averaged.
        int addAmount = 0;

        if (rotations.Count < 1)
        {
            // this.gameObject.transform.GetComponent<Renderer>().enabled = false;
            return;
        }

        //Global variable which represents the additive quaternion
        Quaternion addedRotation = rotations[0];

        //Loop through all the rotational values.
        foreach (Quaternion singleRotation in rotations)
        {

            //Temporary values
            float w;
            float x;
            float y;
            float z;

            var newRotation = singleRotation;
            //Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
            //q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
            if (!AreQuaternionsClose(newRotation, addedRotation))
            {

                newRotation = InverseSignQuaternion(newRotation);
            }

            //Amount of separate rotational values so far
            addAmount++;

            float addDet = 1.0f / (float)addAmount;
            addedRotation.w += newRotation.w;
            w = addedRotation.w * addDet;
            addedRotation.x += newRotation.x;
            x = addedRotation.x * addDet;
            addedRotation.y += newRotation.y;
            y = addedRotation.y * addDet;
            addedRotation.z += newRotation.z;
            z = addedRotation.z * addDet;

            //Normalize. Note: experiment to see whether you
            //can skip this step.
            float D = 1.0f / (w * w + x * x + y * y + z * z);
            w *= D;
            x *= D;
            y *= D;
            z *= D;

            //The result is valid right away, without
            //first going through the entire array.
            averageRotation = new Quaternion(x, y, z, w);
        }

        this.transform.position = averagePos;
        this.transform.rotation = averageRotation;

    }



    //Changes the sign of the quaternion components. This is not the same as the inverse.
    public static Quaternion InverseSignQuaternion(Quaternion q)
    {

        return new Quaternion(-q.x, -q.y, -q.z, -q.w);
    }

    //Returns true if the two input quaternions are close to each other. This can
    //be used to check whether or not one of two quaternions which are supposed to
    //be very similar but has its component signs reversed (q has the same rotation as
    //-q)
    public static bool AreQuaternionsClose(Quaternion q1, Quaternion q2)
    {

        float dot = Quaternion.Dot(q1, q2);

        if (dot < 0.0f)
        {

            return false;
        }

        else
        {

            return true;
        }
    }
}

