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
        private bool m_isPLayerDisabled = false;
        public bool isPlayerDisabled
        {
            get { return m_isPLayerDisabled; }
            set { m_isPLayerDisabled = value; }
        }

        void Update()
        {
            if (m_isPLayerDisabled) 
            {
                m_deathTXT.enabled = true;
                m_mainCamera.SetActive(false);
                m_deathCamera.SetActive(true);
            }
            else 
            {
                m_deathTXT.enabled = false;
                m_mainCamera.SetActive(true);
                m_deathCamera.SetActive(false);
            }
        }
    }
}
