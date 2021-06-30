using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScan : MonoBehaviour
{
    [SerializeField] float m_targetYRotation = 80f;
    [SerializeField] float m_targetYRotation2 = 40f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] GameObject m_cameraView;
    [SerializeField] float m_scanRadius = 1f;
    [SerializeField] LayerMask m_playerMask;
    [SerializeField] float m_startAngle = 0f;

    private Quaternion m_targetQuaternion;
    private float m_movedSoFar; 

    private void Start()
    {
        m_targetQuaternion = Quaternion.Euler(transform.rotation.x, m_targetYRotation - m_startAngle, transform.rotation.z);


    }
    void Update()
    {
       //Check sorrounding area over the Camera view gameobject. Ignores every collider expect for the ones in the m_playerMask layer.
       //Return whether there is something there.
       bool hitPlayer = Physics.CheckSphere(m_cameraView.transform.position, m_scanRadius, m_playerMask);
       
       
       if (hitPlayer)
       {
           print("Detecting player");
       }

       //Check to see whether the target is positive or negative.
       //This will be used to check whether the current Y rotation is close to 
       //the target Y rotation.
        bool isTargetPositive = m_targetYRotation > 0;
        bool isTargetNegative = m_targetYRotation < 0;


        //Find the difference between the original Y rotation before moving and the target 
        float differenceOfYRotation = m_targetYRotation - m_startAngle;

        bool isDifferencePositive = differenceOfYRotation > 0;
        bool isDifferenceNegative = differenceOfYRotation < 0;
        print("Target rotation Y: " + m_targetQuaternion.y *  100);
        print("Start ANGLE Y: " + m_startAngle);
        print("Difference of Y Rotation: " + differenceOfYRotation);
        print("Moves So Far: " + m_movedSoFar);
       
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
            return;
        }
        else if(m_movedSoFar < differenceOfYRotation && isTargetNegative) 
        {
            print("Too far negative");
            return;
        }
       
        //Predict the step of rotation by using the same equation used for the actual movement
        Vector3 predictedRotation = new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed;

        //Add the predicted step to the movement so far
        m_movedSoFar += predictedRotation.y;

        //Actually move.
        transform.Rotate(new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed);
    }

    //Show the area of the camera scanning for the player in the Camera View object
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_cameraView.transform.position, m_scanRadius);
    }
}
