using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is used on the player to check if it is touching an objstacle or something like it. 
public class Obstacle_Check : MonoBehaviour
{
    [SerializeField] LayerMask m_ObjstacleMask;
    [SerializeField] LayerMask m_GroundMask;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == LayerMask.NameToLayer("Obstacle")) 
        {
            print("NAME OF COLLISION: " + hit.transform.name);
            print("OBJSTACLE!!!!!!");
        }
    }
}
