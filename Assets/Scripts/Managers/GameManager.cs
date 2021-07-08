using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Freemart.Managers
{
    public enum GameStatus 
    {
        WON,
        LOST,
        IN_PROGRESS

    }
    /// <summary>
    /// The brains of the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_gameStatusText;
        [SerializeField] GameObject m_mainCamera;
        [SerializeField] GameObject m_deathCamera;
        [SerializeField] string m_deathText = "You Died";
        [SerializeField] string m_youWonText = "You Got Free Stuff!";

        private bool m_isPlayerDisabled = false;
        private bool m_isCourseFinished = false;
        private GameStatus m_gameStatus = GameStatus.IN_PROGRESS;
        public bool isPlayerDisabled
        {
            get { return m_isPlayerDisabled; }
            set { m_isPlayerDisabled = value; }
        }

        public bool isCourseFinished 
        {
            get { return m_isCourseFinished; }
            set { m_isCourseFinished = value; }
        }

        void Update()
        {
            //if the game status has been set to lost, run the game lost command and return
            if (m_gameStatus == GameStatus.LOST)
            {
                GameLost();
                return;
            }
            else if (m_gameStatus == GameStatus.WON)
            {
                GameWon();
                return;
            }
            //If the game status isnt won or lost, that means it is in progress so:
            //1. monitor the status of the isPlayerDisabled (death or life?) 
            //2. monitor the status of isCourseFinished
            else
            {
                m_gameStatusText.enabled = false;

                //enable main cam and disable death cam
                m_mainCamera.SetActive(true);
                m_deathCamera.SetActive(false);

                if (m_isPlayerDisabled)
                {
                    m_gameStatus = GameStatus.LOST;
                    return;
                }

                if (m_isCourseFinished)
                {
                    m_gameStatus = GameStatus.WON;
                    return;
                }               
            }
            m_gameStatus = GameStatus.IN_PROGRESS;
        }

        /// <summary>
        /// This enables the game over camera amd puts up the DEATH txt.
        /// It also sets the game status to LOST to make sure the game stays that way.
        /// </summary>
        private void GameLost()
        {
            m_gameStatusText.enabled = true;
            m_gameStatusText.text = m_deathText;
            //enable death cam and disable main cam
            m_mainCamera.SetActive(false);
            m_deathCamera.SetActive(true);
            m_gameStatus = GameStatus.LOST;
        }

        /// <summary>
        /// This enables the game over camera amd puts up the WON txt.
        /// It also sets the game status to WON to make sure the game stays that way.
        /// </summary>
        private void GameWon() 
        {
            m_gameStatusText.enabled = true;
            m_gameStatusText.text = m_youWonText;

            //enable death cam and disable main cam
            m_mainCamera.SetActive(false);
            m_deathCamera.SetActive(true);
            m_gameStatus = GameStatus.WON;
        }
    }
}
