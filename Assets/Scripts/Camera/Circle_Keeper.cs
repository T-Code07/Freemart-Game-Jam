using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freemart.Obstacles.Camera
{
    /// <summary>
    /// Use this script to make sure the Camera View of a camera is circular.
    /// </summary>
    [ExecuteInEditMode]
    public class Circle_Keeper : MonoBehaviour
    {
        void Update()
        {
            float x = transform.localScale.z;
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
    }
}
