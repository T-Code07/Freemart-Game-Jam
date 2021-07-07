using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Freemart.Player.Health
{
    public enum PlayerState
    {
        ALIVE,
        DEAD
    }
    public class playerHealth : MonoBehaviour
    {
        [SerializeField] float m_maxHealth = 10f;
        [SerializeField] TextMeshProUGUI m_healthText;
        public PlayerState m_playerState = PlayerState.ALIVE;

        //To be called in objects that do damage.
        public void DecreaseHealth(float damage)
        {
            if (m_playerState == PlayerState.DEAD) return;
            m_maxHealth -= damage;

            if (m_maxHealth <= 0)
            {
                m_playerState = PlayerState.DEAD;
            }
        }

        private void Update()
        {
            m_healthText.text = m_maxHealth.ToString();
            if (m_playerState == PlayerState.DEAD)
            {
                print("YOU ARE DEAD");
                return;
            }
        }
    }
}
