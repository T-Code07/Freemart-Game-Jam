using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    float m_MouseSensitivity = 100f;
    float m_xRotation = 0f;
    [SerializeField] Transform m_playerTransform;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Code from Unity's example code: https://www.youtube.com/watch?v=_QajrabyTJc

        float Mouse_Input_X = Input.GetAxis("Mouse X") * Time.deltaTime * m_MouseSensitivity;
        float Mouse_Input_Y = Input.GetAxis("Mouse Y") * Time.deltaTime * m_MouseSensitivity;

         m_xRotation -= Mouse_Input_Y;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);
        m_playerTransform.Rotate(Vector3.up * Mouse_Input_X);

    }
}
