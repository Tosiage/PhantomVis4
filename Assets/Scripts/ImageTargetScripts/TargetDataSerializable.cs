﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetDataSerializable
{

    public Vector3 relativePos;
    public Quaternion relativeRot;
    public string id;
    public bool calibrated;
    public string time;

    public TargetDataSerializable(Vector3 relativePos, Quaternion relativeRot, string id, bool calibrated, string time)
    {
        this.relativePos = relativePos;
        this.relativeRot = relativeRot;
        this.id = id;
        this.calibrated = calibrated;
        this.time = time;
    }
}
