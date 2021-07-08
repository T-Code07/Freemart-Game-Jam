using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Managers.LocationPoint;

public class LocationPointManager : MonoBehaviour
{
    [SerializeField] LocationPoint m_startLocation;
    [SerializeField] LocationPoint m_endLocation;

    private bool m_playerFinishedCourse = false;

    public bool PlayerFinishedCourse 
    {
        get { return m_playerFinishedCourse; }
    }

    void Start()
    {
        if(m_startLocation.LocationType != LocationType.START || m_endLocation.LocationType != LocationType.END) 
        {
            Debug.LogError("The location points are the same type of enum. Please change the start location to the locationType.START and repeat for the end location.");
        }
    }

    //todo: need to make the player go to the stop location then back to the start location to end the game. Not just go to the stop location.
    void Update()
    {
        //If the course has been finished, return
        if (m_playerFinishedCourse) return;
        
        //if the start location has been arrived at but the end location has not, this means the player hasn't
        //reached the stop location yet and therefore has just begun.
        if (m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived == false)
        {
            print("Game in progress...");
           
        }
        //If the player has arrived at both the locations but the start location hasn't been reset:    
        //reset the start location for the journey back home. 
        else if(m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived && m_startLocation.TimesReset == 0) 
        {
            //Change the hasArrived back to false and add a reset time to the start location point
            m_startLocation.ResetPoint();
        }
        //If both locations have been arrived at and the start location has already been reset once, end the course
        else if(m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived && m_startLocation.TimesReset == 1) 
        {
            print("Course Ended!!!!!!!!!!!!!");
            m_playerFinishedCourse = true;

        }
        //The course hasn't been started because the player hasn't entered the start location yet
        else if(!m_startLocation.PlayerHasArrived && !m_endLocation.PlayerHasArrived) 
        {
            print("waiting for game to start...");
        }
        //If the start location hasn't been reset, but the end location has already been arrived at:
        // 1. error - this is weird. This means that the player somehow took a shortcut through the course by missing the start location
        //            check code if this happens
        else if( m_startLocation.TimesReset == 0 && m_endLocation.PlayerHasArrived)
        {
            Debug.LogError("Player arrived at end location before start location. Please review location point settings.");
        }

    }

}
