using UnityEngine;
using System;
using System.Collections;

public class RelativeTo : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public Transform target5;
    
    //The averaged rotational value
    private Quaternion averageRotation;

    private void Update()
    {
        //Matrix4x4 M = target.transform.localToWorldMatrix;
        Matrix4x4 Mrot1 = Matrix4x4.Rotate(target1.rotation);
        Matrix4x4 Mtra1 = Matrix4x4.Translate(target1.position);
        Matrix4x4 Mrot2 = Matrix4x4.Rotate(target2.rotation);
        Matrix4x4 Mtra2 = Matrix4x4.Translate(target2.position);
        Matrix4x4 Mrot3 = Matrix4x4.Rotate(target3.rotation);
        Matrix4x4 Mtra3 = Matrix4x4.Translate(target3.position);
        Matrix4x4 Mrot4 = Matrix4x4.Rotate(target4.rotation);
        Matrix4x4 Mtra4 = Matrix4x4.Translate(target4.position);
        Matrix4x4 Mrot5 = Matrix4x4.Rotate(target5.rotation);
        Matrix4x4 Mtra5 = Matrix4x4.Translate(target5.position);
        
        Vector3 pos1 = new Vector3(-0.2367f, -0.093f, 0.144f);
        Vector3 pos2 = new Vector3(-0.2367f, -0.093f, -0.144f);
        Vector3 pos3 = new Vector3(0.2367f, -0.093f, -0.144f);
        Vector3 pos4 = new Vector3(0.2367f, -0.093f, 0.144f);
        Vector3 pos5 = new Vector3(0f, -0.093f, 0f);
        
        //this.transform.position = (Vector3)(Mtra1 * Mrot1 * new Vector4(pos1.x, pos1.y, pos1.z, 1));
        //this.transform.position = (Vector3)(Mtra2 * Mrot2 * new Vector4(pos2.x, pos2.y, pos2.z, 1));
        //this.transform.rotation = target1.transform.rotation;
        //this.transform.rotation = target2.transform.rotation;

        Vector3 pos1final = (Vector3)(Mtra1 * Mrot1 * new Vector4(pos1.x, pos1.y, pos1.z, 1));
        Vector3 pos2final = (Vector3)(Mtra2 * Mrot2 * new Vector4(pos2.x, pos2.y, pos2.z, 1));
        Vector3 pos3final = (Vector3)(Mtra3 * Mrot3 * new Vector4(pos3.x, pos3.y, pos3.z, 1));
        Vector3 pos4final = (Vector3)(Mtra4 * Mrot4 * new Vector4(pos4.x, pos4.y, pos4.z, 1));
        Vector3 pos5final = (Vector3)(Mtra5 * Mrot5 * new Vector4(pos5.x, pos5.y, pos5.z, 1));
        
        Vector3 avgPosition = new Vector3((pos1final.x + pos2final.x + pos3final.x + pos4final.x + pos5final.x) / 5, 
                        (pos1final.y + pos2final.y + pos3final.y  + pos4final.y + pos5final.y) / 5,
                        (pos1final.z + pos2final.z  + pos3final.z + pos4final.z + pos5final.z) / 5);


        //Global variable which holds the amount of rotations which
        //need to be averaged.
        int addAmount = 0;

        //https://forum.unity.com/threads/average-quaternions.86898/
        //http://wiki.unity3d.com/index.php/Averaging_Quaternions_and_Vectors
        //multipleRotations is an array which holds all the quaternions
        //which need to be averaged.
        Quaternion[] multipleRotations = { target1.transform.rotation, target2.transform.rotation, target3.transform.rotation,
            target4.transform.rotation, target5.transform.rotation };

        //Global variable which represents the additive quaternion
        Quaternion addedRotation = multipleRotations[0];

        //Loop through all the rotational values.
        foreach (Quaternion singleRotation in multipleRotations)
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





        this.transform.position = avgPosition;
        this.transform.rotation = averageRotation;



    }

    //Get an average (mean) from more then two quaternions (with two, slerp would be used).
    //Note: this only works if all the quaternions are relatively close together.
    //Usage: 
    //-Cumulative is an external Vector4 which holds all the added x y z and w components.
    //-newRotation is the next rotation to be added to the average pool
    //-firstRotation is the first quaternion of the array to be averaged
    //-addAmount holds the total amount of quaternions which are currently added
    //This function returns the current average quaternion
    /*  public static Quaternion AverageQuaternion(ref Vector4 cumulative, Quaternion newRotation, Quaternion firstRotation, int addAmount)
      {

          float w = 0.0f;
          float x = 0.0f;
          float y = 0.0f;
          float z = 0.0f;

          //Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
          //q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
          if (!AreQuaternionsClose(newRotation, firstRotation))
          {

              newRotation = InverseSignQuaternion(newRotation);
          }

          //Average the values
          float addDet = 1f / (float)addAmount;
          cumulative.w += newRotation.w;
          w = cumulative.w * addDet;
          cumulative.x += newRotation.x;
          x = cumulative.x * addDet;
          cumulative.y += newRotation.y;
          y = cumulative.y * addDet;
          cumulative.z += newRotation.z;
          z = cumulative.z * addDet;

          //note: if speed is an issue, you can skip the normalization step
          return NormalizeQuaternion(x, y, z, w);
      }*/

    public static Quaternion NormalizeQuaternion(float x, float y, float z, float w)
    {

        float lengthD = 1.0f / (w * w + x * x + y * y + z * z);
        w *= lengthD;
        x *= lengthD;
        y *= lengthD;
        z *= lengthD;

        return new Quaternion(x, y, z, w);
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
