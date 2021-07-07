using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is used on the player to check if it is touching an objstacle or something like it. 
public class Obstacle_Check : MonoBehaviour
{
    [SerializeField] LayerMask m_obstacleMesh;
    [SerializeField] float m_obstacleDamage = 1f;
    [SerializeField] float m_damageDelay = 1f;
    private Health m_healthScript;
    static private bool m_isDelayRunning = false;
    private void Start()
    {
        m_healthScript = GetComponent<Health>();
    }

    //Runs when the character controller is moving
    //Becuase this is called only when the character controller is moved,
    //it won't run when the player is stopped.
    
    //Bug?- 
    //Doesn't do damage if the player doesn't move.
    //EX: If the player stands on an obstacle, it will only do damage once 
    //    until the player moves again. 
    //
    //To be honest, this isn't that bad of a problem. Stick with it for now?
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Checks the layer of the hit
        if(hit.gameObject.layer == LayerMask.NameToLayer("Obstacle")) 
        {
            //if the co-routine is running, return
            if (m_isDelayRunning) return;

            StartCoroutine(damageDelay());
        }
    }

    IEnumerator damageDelay() 
    {
        //Delay is running
        m_isDelayRunning = true;

        //Decrease health
        m_healthScript.DecreaseHealth(m_obstacleDamage);

        //Delay
        yield return new WaitForSeconds(m_damageDelay);

        //Delay stopped
        m_isDelayRunning = false;
    }
}
