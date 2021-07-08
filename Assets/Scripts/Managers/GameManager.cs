using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Freemart.Managers
{
    /// <summary>
    /// The brains of the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_deathTXT;
        [SerializeField] GameObject m_mainCamera;
        [SerializeField] GameObject m_deathCamera;
        private bool m_isPlayerDisabled = false;
        public bool isPlayerDisabled
        {
            get { return m_isPlayerDisabled; }
            set { m_isPlayerDisabled = value; }
        }

        void Update()
        {
            if (m_isPlayerDisabled) 
            {
                m_deathTXT.enabled = true;

                //enable death cam and disable main cam
                m_mainCamera.SetActive(false);
                m_deathCamera.SetActive(true);
            }
            else 
            {
                m_deathTXT.enabled = false;

                //enable main cam and disable death cam
                m_mainCamera.SetActive(true);
                m_deathCamera.SetActive(false);
            }
        }
    }
}
