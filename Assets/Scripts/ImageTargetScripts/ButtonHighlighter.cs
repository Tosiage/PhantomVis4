using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;

namespace HoloToolkit.Unity.InputModule
{
    public class ButtonHighlighter : MonoBehaviour, IFocusable
    {
        public Material highlightMat;
        public Material normalMat;
        public void OnFocusEnter()
        {
            this.gameObject.GetComponent<Renderer>().material = highlightMat;
        }

        public void OnFocusExit()
        {
            this.gameObject.GetComponent<Renderer>().material = normalMat;
        }


    }
}
