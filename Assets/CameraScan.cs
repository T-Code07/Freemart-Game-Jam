using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScan : MonoBehaviour
{
    [SerializeField] float m_targetYRotation = -80f;
    [SerializeField] float m_speed = 1f;
    [SerializeField] GameObject m_cameraView;
    [SerializeField] float m_scanRadius = 1f;
    [SerializeField] LayerMask m_pointMask;
    [SerializeField] LayerMask m_playerMask;
    [SerializeField] GameObject m_endPoint;
    [SerializeField] float m_startAngle = 0f;
    [SerializeField] float m_honingDistance = 2f;

    private Quaternion m_startRotation;
    private Quaternion m_targetQuaternion;
    private float m_newDiff;
    private float m_movedSoFar; 

    private void Start()
    {
        m_startRotation = transform.rotation;

        m_targetQuaternion = Quaternion.Euler(m_startRotation.x, m_targetYRotation + m_startAngle, m_startRotation.z);
        print(m_targetQuaternion);
   //     RotateY(m_targetYRotation);
 //       RotateY(0);

    }
    void Update()
    {
       bool reachedEndLocation = Physics.CheckSphere(m_cameraView.transform.position, m_scanRadius, m_pointMask);
        bool hitPlayer = Physics.CheckSphere(m_cameraView.transform.position, m_scanRadius, m_playerMask);
        if (reachedEndLocation)
        {
            print("At end location");
        }
        if (hitPlayer)
        {
            print("Detecting player");
        }



       float differenceOfYRotation = m_startRotation.y + m_targetYRotation;
        print("Start rotation Y: " + m_startRotation.y);
        print("Difference of Y Rotation: " + differenceOfYRotation);
        print("New Diff: " + m_newDiff);

        //     transform.LookAt(m_endPoint.transform);
        //  float steps = Mathf.Abs(differenceOfYRotation / m_moveTimeDelay);
        bool samePos = m_startRotation.y == transform.rotation.y;
        float moveToGo = differenceOfYRotation - m_newDiff;
        print("move so far: " + moveToGo);

        if (m_movedSoFar > m_targetYRotation)
        {
            print("TOOOOOOOOOOOOOOOOOOOo FAR");
            return;
        }

    //    if (Mathf.Abs(moveToGo) > m_honingDistance || samePos)
      //  {
            Vector3 predictedRotation = new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed;
            print(predictedRotation);
            m_movedSoFar += predictedRotation.y;
            transform.Rotate(new Vector3(0, differenceOfYRotation, 0) * Time.deltaTime * m_speed);
 //       }
         m_newDiff = transform.rotation.y - m_startRotation.y;

        //    transform.rotation = Quaternion.Lerp( m_startRotation, m_targetQuaternion, m_moveTimeDelay * Time.deltaTime);


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_cameraView.transform.position, m_scanRadius);
    }
}
