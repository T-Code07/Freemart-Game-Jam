using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScan : MonoBehaviour
{
    [SerializeField] float m_FirstTargetYRotation = 80f;
    [SerializeField] float m_SecondTargetYRotation = 40f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] GameObject m_cameraView;
    [SerializeField] float m_scanRadius = 1f;
    [SerializeField] LayerMask m_playerMask;
    [SerializeField] float m_startAngle = 0f;

    private float m_movedSoFar;
    private bool m_movedToFirstRotation = false;

    void Update()
    {
        //Check sorrounding area over the Camera view gameobject. Ignores every collider expect for the ones in the m_playerMask layer.
        //Return whether there is something there.
        bool hitPlayer = Physics.CheckSphere(m_cameraView.transform.position, m_scanRadius, m_playerMask);


        if (hitPlayer)
        {
            print("Detecting player");
        }

        //If haven't reach first rotation
        //Check to see if roatate to positon -- this works bc rotate to 
        //--position only returns true if it has made it. 
        //--Everytime you call it, it moves and checks again
        if (m_movedToFirstRotation == false)
        {
            if (RotateToPosition(m_FirstTargetYRotation))
            {
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
            if (RotateToPosition(m_SecondTargetYRotation))
            {
                print("moved to 2nd p");
                m_movedToFirstRotation = false;
            }

        }   
    }

    private bool RotateToPosition(float target)
    {
        //Check to see whether the target is positive or negative.
        //This will be used to check whether the current Y rotation is close to 
        //the target Y rotation.
        bool isTargetPositive = target > 0;
        bool isTargetNegative = target < 0;


        //Find the difference between the original Y rotation before moving and the target 
        float differenceOfYRotation = target - m_startAngle;

        //If the distance moved so far is greater than the difference between the target value and the start angle
        //AND the value is positive
        //stop moving (return)

        //Check if it is positive bc when moving from a positive number to a negative number, the
        //target value is smaller than the difference. Becuase of this bug, it will stop the movement. 
        //todo: make moving between larger and smaller numbers when the number is still positive or negative work
        //EX: from -80 to -40, from 80 to 40
        if (m_movedSoFar > differenceOfYRotation && isTargetPositive)
        {
            print("TOOOOOOOOOOOOOOOOOOOo FAR");
            return true;
        }
        else if (m_movedSoFar < differenceOfYRotation && isTargetNegative)
        {
            print("Too far negative");
            return true;
        }

        //Predict the step of rotation by using the same equation used for the actual movement
        Vector3 predictedRotation = new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed;

        //Add the predicted step to the movement so far
        m_movedSoFar += predictedRotation.y;

        //Actually move.
        transform.Rotate(new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed);
        return false;
    }

    //Show the area of the camera scanning for the player in the Camera View object
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_cameraView.transform.position, m_scanRadius);
    }
}
