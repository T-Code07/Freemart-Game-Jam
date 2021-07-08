using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Managers.LocationPoint;
using TMPro;
using Freemart.Managers;

public class LocationPointManager : MonoBehaviour
{
    [Header("Location Point Objects: ")]
    [SerializeField] LocationPoint m_startLocation;
    [SerializeField] LocationPoint m_endLocation;

    [Space(5)]

    [Header("Course Status Text: ")]
    [SerializeField] TextMeshProUGUI m_courseStatusText;
    [SerializeField] string m_finishedCourseText = "You Escaped!";
    [SerializeField] string m_goHomeText = "Escape through the entrance!";
    [SerializeField] string m_startGameText = "Please start the game...";
    [SerializeField] string m_goToShelves = "Travel to the shelves (red location)";

    private bool m_playerFinishedCourse = false;
    private string m_currentText;
    private GameManager m_gameManager;
    public bool PlayerFinishedCourse 
    {
        get { return m_playerFinishedCourse; }
    }

    void Start()
    {
        m_gameManager = GetComponent<GameManager>();
        if(m_startLocation.LocationType != LocationType.START || m_endLocation.LocationType != LocationType.END) 
        {
            Debug.LogError("The location points are the same type of enum. Please change the start location to the locationType.START and repeat for the end location.");
        }
    }

    //todo: need to make the player go to the stop location then back to the start location to end the game. Not just go to the stop location.
    void Update()
    {
        //If the course has been finished, return
        if (m_playerFinishedCourse) 
        {
            m_gameManager.isCourseFinished = true;

            return; 
        }
        
        //if the start location has been arrived at but the end location has not, this means the player hasn't
        //reached the stop location yet and therefore has just begun.
        if (m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived == false)
        {
            m_currentText = m_goToShelves;          
        }
        //If the player has arrived at both the locations but the start location hasn't been reset:    
        //reset the start location for the journey back home. 
        else if(m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived && m_startLocation.TimesReset == 0) 
        {
            m_currentText = m_goHomeText;

            //Change the hasArrived back to false and add a reset time to the start location point
            m_startLocation.ResetPoint();
        }
        //If both locations have been arrived at and the start location has already been reset once, end the course
        else if(m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived && m_startLocation.TimesReset == 1) 
        {
            m_playerFinishedCourse = true;
            m_currentText = m_finishedCourseText;
            return;
        }
        //The course hasn't been started because the player hasn't entered the start location yet
        else if(!m_startLocation.PlayerHasArrived && !m_endLocation.PlayerHasArrived) 
        {
            m_currentText = m_startGameText;
        }
        //If the start location hasn't been reset, but the end location has already been arrived at:
        // 1. error - this is weird. This means that the player somehow took a shortcut through the course by missing the start location
        //            check code if this happens
        else if( m_startLocation.TimesReset == 0 && m_endLocation.PlayerHasArrived)
        {
            Debug.LogError("Player arrived at end location before start location. Please review location point settings.");
        }

        try
        {
            m_courseStatusText.text = m_currentText;
        }
        catch 
        
        {
            Debug.LogError("Text never set in Location Manager");
        }

        m_gameManager.isCourseFinished = false;
    }

}
