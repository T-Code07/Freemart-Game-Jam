using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Player.Control;
using Freemart.Player.Health;
namespace Freemart.Managers
{
    /// <summary>
    /// Used to manage the player in order to make the game manager code more simple.
    /// This class is mainly used to monitor the state of the player and then disable the controls if he/she dies. 
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        private PlayerController m_playerController;
        private PlayerHealth m_playerHealth;
        private GameManager m_gameManager;
       

        private void Start()
        {
            m_gameManager = FindObjectOfType<GameManager>();
            m_playerController = GetComponent<PlayerController>();
            m_playerHealth = GetComponent<PlayerHealth>();
        }
        void Update()
        {
            //Freeze the player's movment and tell the game manager that the player is frozen if:
            //1. The player is dead
            //2. the gameManager doesn't know the player is dead.
            if (m_playerHealth.playerState == PlayerState.DEAD && m_gameManager.isPlayerDisabled == false)
            {
                //Freeze movement in player
                m_playerController.isMovementFrozen = true;

                //Tell game manager player has been disabled
                m_gameManager.isPlayerDisabled = true;
                
                return;
            }
        }
    }
    
}
