using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeTo : MonoBehaviour{
    public Transform target;

    private void Update()
    {
        Matrix4x4 M = target.transform.localToWorldMatrix;
        Vector3 pos = new Vector3(1f, 0f, 0f);
        this.transform.position = (Vector3)(M * new Vector4(pos.x, pos.y, pos.z, 1));
       // this.transform.position = target.transform.position;
    }
}
