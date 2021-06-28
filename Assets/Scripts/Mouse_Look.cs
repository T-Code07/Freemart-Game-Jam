using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    [SerializeField] float m_MouseSensitivity = 100f;
    float m_xRotation = 0f;
    [SerializeField] Transform m_playerTransform;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Camera might have moved in update, so update after Update()
    private void LateUpdate()
    {
        //Code from Unity's example code: https://www.youtube.com/watch?v=_QajrabyTJc

        //changed Time.deltaTime to Time.smoothDeltaTime. It was super bumpy and looked as if it was stepping to different grid steps.
        //However, smoothDeltaTime helped with that. Apparently, (according to https://forum.unity.com/threads/time-smoothdeltatime.10253/) it limits the flucuaction in Time.Deltatime. 
        //Technically, it is slower than Time.Deltatime, but the "hitch" is less noticable. 
        float Mouse_Input_X = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.smoothDeltaTime;
        float Mouse_Input_Y = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.smoothDeltaTime;

         m_xRotation -= Mouse_Input_Y;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);
        
          m_playerTransform.Rotate(Vector3.up * Mouse_Input_X);
    }
}
