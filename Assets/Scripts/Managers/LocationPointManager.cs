using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Managers.LocationPoint;

public class LocationPointManager : MonoBehaviour
{
    [SerializeField] LocationPoint m_startLocation;
    [SerializeField] LocationPoint m_endLocation;

    private bool m_playerFinishedCourse = false;
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
        if (m_playerFinishedCourse) return;
        
        if (m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived == false)
        {
            print("Game in progress...");
        }
        else if(m_startLocation.PlayerHasArrived && m_endLocation.PlayerHasArrived) 
        {
            print("Game ended.");
            m_playerFinishedCourse = true;
        }
        else if(!m_startLocation.PlayerHasArrived && !m_endLocation.PlayerHasArrived) 
        {
            print("waiting for game to start...");
        }
        else 
        {
            Debug.LogError("Player arrived at end location before start location. Please review location point settings.");
        }

    }
}
