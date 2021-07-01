using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScan : MonoBehaviour
{
    [SerializeField] float m_FirstTargetYRotation = 80f;
    [SerializeField] float m_SecondTargetYRotation = 40f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] GameObject m_cameraView;
    [SerializeField] float m_scanScaleBoost = 1f;
    [SerializeField] LayerMask m_playerMask;
    [SerializeField] float m_startAngle = 0f;

    private bool m_movedToFirstRotation = false;
    private Vector3 m_cameraViewScale;
    private float m_scanArea;
    private float m_honingDistance = 1.5f;
    private void Start()
    {
        //make the scan are the product of the scale of the z axis and the boost.
        //The z and x axis make the object circular.
        //Because of the Circle_Keeper script, the x will always match the z.
        m_cameraViewScale = m_cameraView.transform.localScale;
        m_scanArea = (m_scanScaleBoost * m_cameraViewScale.z);
    }

    void Update()
    {
        //Check sorrounding area over the Camera view gameobject. Ignores every collider expect for the ones in the m_playerMask layer.
        //Return whether there is something there.
        bool hitPlayer = Physics.CheckSphere(m_cameraView.transform.position, m_scanArea, m_playerMask);


        if (hitPlayer)
        {
            print("Detecting player");
        }

        if (RotateToPosition(m_FirstTargetYRotation)) 
        {
            print("successfully rotates to position");
            if (RotateToPosition(m_SecondTargetYRotation)) 
            {
                print("successfully rotates to 2ND POSITION");
                return;
            }
        }
    //    RotateToPosition(m_FirstTargetYRotation);
        //If haven't reach first rotation
        //Check to see if roatate to positon -- this works bc rotate to 
        //--position only returns true if it has made it. 
        //--Everytime you call it, it moves and checks again
    /*      if (m_movedToFirstRotation == false)
           {
            print("trying to rotate to first posiont");
               if (RotateToPosition(m_FirstTargetYRotation))
               {
                print("Fully rotated to first positon");
                   //Rotated to postion
                   //Therefore:
                   m_movedToFirstRotation = true;
               }
               print("Moved to 1st p");
           }

           //Repeat same logic from above here
           //Except, check to see if moved to first rotation is true.
           //then set it back to false the second target rotation has been reached. 
           if (m_movedToFirstRotation == true)
           {
            print("trying to rotate to 2ND posiont");

            if (RotateToPosition(m_SecondTargetYRotation))
               {
                   print("moved to 2nd p");
                   m_movedToFirstRotation = false;
               }

           }   */
    }

    private bool RotateToPosition(float target)
    {
        print("Vector 3 distance: " + Vector3.Distance(transform.rotation.eulerAngles, new Vector3(0, target, 0)));

        //Find the difference between the original Y rotation before moving and the target 


        float differenceOfYRotation = target - m_startAngle;
        print("Difference of Y: " + differenceOfYRotation);

        if (Vector3.Distance(transform.rotation.eulerAngles, new Vector3(0, target, 0)) < m_honingDistance)
        {
            print("reached rotation");
            return true;
        }

        //Actually move.
        transform.Rotate(new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed);
        return false;
            
            
    }

    //Show the area of the camera scanning for the player in the Camera View object
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_cameraView.transform.position, m_scanScaleBoost * m_cameraView.transform.localScale.z);
    }
}
