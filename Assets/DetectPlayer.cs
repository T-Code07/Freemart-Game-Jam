using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("DETECTING>>>>>>>>>>>");
        print(collision.gameObject.name);
    }
}
