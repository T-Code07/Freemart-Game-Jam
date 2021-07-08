using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freemart.Managers.LocationPoint
{
    /// <summary>
    /// Basic types of location points
    /// </summary>
    public enum LocationType 
    {
        START,
        END
    }

    /// <summary>
    /// This is the class that holds the base functions for location points.
    /// </summary>
    public class LocationPoint : MonoBehaviour
    {
        [SerializeField] float m_LocationRadius = 5f; 
        [SerializeField] LayerMask m_playerLayer;
        [SerializeField] LocationType m_locationType; 
        private bool m_playerHasArrived = false;
        private int m_timesReset = 0; //<- used to track points reached so far in the LocationPointManager

        //read only value
        public bool PlayerHasArrived 
        {
            get { return m_playerHasArrived; }
        }

        //read only
        public int TimesReset 
        {
            get { return m_timesReset; }
        }

        //read only
        public LocationType LocationType 
        {
            get { return m_locationType; }
        }

        void Update()
        {
            //check if player has arrived yet:
            // if yes, return and don't do anything
            //
            //else:
            //check for player
            if (m_playerHasArrived) return;
            else
            {
                //check the sphere are around the location only in the player layer
                bool isCurrentlyThere = Physics.CheckSphere(transform.position, m_LocationRadius, m_playerLayer);

                //if the player is there:
                if (isCurrentlyThere) 
                {
                    //this basically stops the loop.
                    m_playerHasArrived = true;
                }
            }
                    
        }

        /// <summary>
        /// Resets the point to its original state. It also adds 1 to the timesReset variable. 
        /// </summary>
       public void ResetPoint() 
        {
            m_playerHasArrived = false;
            m_timesReset += 1;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, m_LocationRadius);
        }
    }
}
