using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Player.Health;


namespace Freemart.Player.EnviromentSensors
{
    //This is used on the player to check if it is touching an objstacle or something like it. 
    public class Obstacle_Check : MonoBehaviour
    {
        [SerializeField] LayerMask m_obstacleMesh;
        [SerializeField] float m_obstacleDamage = 1f;
        [SerializeField] float m_damageDelay = 1f;
        private PlayerHealth m_healthScript;
        static private bool m_isDelayRunning = false;
        private void Start()
        {
            m_healthScript = GetComponent<PlayerHealth>();
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
            if (hit.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {           
                m_healthScript.DecreaseHealth(m_obstacleDamage, m_damageDelay);
            }
        }
    }
}
