using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freemart.Sensors
{
    public enum LocationType 
    {
        START,
        END
    }
    public class LocationPoint : MonoBehaviour
    {
        [SerializeField] float m_LocationRadius = 5f;
        [SerializeField] LayerMask m_playerLayer;
        [SerializeField] LocationType m_locationType; 
        private bool m_playerHasArrived = false;

        //read only value
        public bool PlayerHasArrived 
        {
            get { return m_playerHasArrived; }
        }

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
                    print("Arrived!");
                    m_playerHasArrived = true;
                }
            }
                    
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, m_LocationRadius);
        }
    }
}
