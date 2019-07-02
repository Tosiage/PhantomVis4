using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPositionUpdater : MonoBehaviour {
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public Transform target5;
    public Transform target6;
    public Transform target7;
    public Transform target8;
    public Transform target9;
    public Transform target10;
    public Transform target11;
    public Transform target12;
    public Transform target13;
    public Transform target14;
    public Transform target15;
    public Transform target16;

    [HideInInspector] public TargetData targetData1;
    [HideInInspector] public TargetData targetData2;
    [HideInInspector] public TargetData targetData3;
    [HideInInspector] public TargetData targetData4;
    [HideInInspector] public TargetData targetData5;
    [HideInInspector] public TargetData targetData6;
    [HideInInspector] public TargetData targetData7;
    [HideInInspector] public TargetData targetData8;
    [HideInInspector] public TargetData targetData9;
    [HideInInspector] public TargetData targetData10;
    [HideInInspector] public TargetData targetData11;
    [HideInInspector] public TargetData targetData12;
    [HideInInspector] public TargetData targetData13;
    [HideInInspector] public TargetData targetData14;
    [HideInInspector] public TargetData targetData15;
    [HideInInspector] public TargetData targetData16;

    [HideInInspector] public Vector3 pos1;
    [HideInInspector] public Vector3 pos2;

    [HideInInspector] public List<TargetData> targetDatas;
    [HideInInspector] public List<Vector3> positions;
    [HideInInspector] public List<Quaternion> rotations;
    [HideInInspector] public Vector3 addedUpVectors;
    [HideInInspector] public Vector3 averagePos;

    public GameObject[] targetManagerTargets;
    public List<TargetData> targetManagerTargetDatas;

    //The averaged rotational value
    private Quaternion averageRotation;


    // Use this for initialization
    void Start () {
        
        //Die Daten aller Targets die in der Szene existieren.
        //Daten sind relative Position und relative Rotation
        targetData1 = target1.GetComponent<TargetData>();
        targetData2 = target2.GetComponent<TargetData>();
        targetData3 = target3.GetComponent<TargetData>();
        targetData4 = target4.GetComponent<TargetData>();
        targetData5 = target5.GetComponent<TargetData>();
        targetData6 = target6.GetComponent<TargetData>();
        targetData7 = target7.GetComponent<TargetData>();
        targetData8 = target8.GetComponent<TargetData>();
        targetData9 = target9.GetComponent<TargetData>();
        targetData10 = target10.GetComponent<TargetData>();
        targetData11 = target11.GetComponent<TargetData>();
        targetData12 = target12.GetComponent<TargetData>();
        targetData13 = target13.GetComponent<TargetData>();
        targetData14 = target14.GetComponent<TargetData>();
        targetData15 = target15.GetComponent<TargetData>();
        targetData16 = target16.GetComponent<TargetData>();


        //Liste, um über alle targetDatas iterieren zu können
        targetDatas = new List<TargetData>
        {
            targetData1,
            targetData2,
            targetData3,
            targetData4,
            targetData5,
            targetData6,
            targetData7,
            targetData8,
            targetData9,
            targetData10,
            targetData11,
            targetData12,
            targetData13,
            targetData14,
            targetData15,
            targetData16
        };
        positions = new List<Vector3>();
        rotations = new List<Quaternion>();

        targetManagerTargets = GameObject.Find("TargetManager").GetComponent<TargetManager>().targets;
        targetManagerTargetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;

    }
	
	// Update is called once per frame
	void Update () {
        //Zu Beginn jedes Updates Leeren der Listen, um zu vermeiden, dass Daten verfälscht werden
        //Liste würde sonst immer länger werden
        positions.Clear();
        rotations.Clear();
        addedUpVectors = Vector3.zero;

        //Wenn Target gerade sichtbar ist, wird Position des Targets relativ zum Phantom in position Liste abgespeichert
        //Rotation des Targets relativ zum Phantom wird in rotations Liste abgespeichert
        //Anschließend werden Mittelwerte beider Listen gebildet und dem GameObjekt des Phantoms zugewiesen
        foreach(TargetData td in targetDatas)
        {
            if (td.isVisible)
            {
                var currentTransform = td.transform;
                Quaternion targetRot = Quaternion.Euler(td.angleX, td.angleY, td.angleZ);

                Matrix4x4 Mrot = Matrix4x4.Rotate(currentTransform.rotation * targetRot);
                Matrix4x4 Mtra = Matrix4x4.Translate(currentTransform.position);
                Vector3 posfinal = (Vector3)(Mtra * Mrot * new Vector4(td.relativePos.x, td.relativePos.y, td.relativePos.z, 1));
                positions.Add(posfinal);
                rotations.Add(currentTransform.rotation * targetRot);
 
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

        if(rotations.Count < 1)
        {
           
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
