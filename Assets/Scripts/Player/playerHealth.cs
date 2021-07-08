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
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float m_maxHealth = 10f;
        [SerializeField] TextMeshProUGUI m_healthText;

        private bool m_isDelayRunning = false;
        private PlayerState m_playerState = PlayerState.ALIVE;

        public PlayerState playerState 
        {
            get { return m_playerState; }
            set { m_playerState = value; }
        }
        //To be called in objects that do damage.
        public void DecreaseHealth(float damage, float delay = 0)
        {
            if (m_playerState == PlayerState.DEAD) return;

            if (m_isDelayRunning) return;

            StartCoroutine(damageDelay(damage, delay));

            if (m_maxHealth <= 0)
            {
                m_playerState = PlayerState.DEAD;
            }
        }
        IEnumerator damageDelay(float damage, float delay = 0)
        {
            //Delay is running
            m_isDelayRunning = true;

            //Decrease health
            m_maxHealth -= damage;

            //Delay
            yield return new WaitForSeconds(delay);

            //Delay stopped
            m_isDelayRunning = false;
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
