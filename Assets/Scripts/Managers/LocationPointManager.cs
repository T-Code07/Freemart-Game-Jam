using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Managers.LocationPoint;

public class LocationPointManager : MonoBehaviour
{
    [SerializeField] LocationPoint m_startLocation;
    [SerializeField] LocationPoint m_endLocation;
    void Start()
    {
        if(m_startLocation.LocationType != LocationType.START || m_endLocation.LocationType != LocationType.END) 
        {
            Debug.LogError("The location points are the same type of enum. Please change the start location to the locationType.START and repeat for the end location.");
        }
    }

    void Update()
    {
        
    }
}
