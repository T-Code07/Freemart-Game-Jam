using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Player.Control;
using Freemart.Player.Health;
namespace Freemart.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private bool m_isDisabled = false;
        private PlayerController m_playerController;
        private PlayerHealth m_playerHealth;

        public bool isDisabled 
        {
            get { return m_isDisabled; }
            set { m_isDisabled = value; }
        }

        private void Start()
        {
            m_playerController = GetComponent<PlayerController>();
            m_playerHealth = GetComponent<PlayerHealth>();
        }
        void Update()
        {
            if (m_playerHealth.playerState == PlayerState.DEAD)
            {
                m_playerController.isMovementFrozen = true;

            }
        }
    }
    
}
