using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freemart.Player.Control
{
    public class Mouse_Look : MonoBehaviour
    {
        [SerializeField] float m_MouseSensitivity = 3f;
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


            //Input.getAxis doesn't need the Time.deltaTime becuase it is independent of framerate. (Source: https://answers.unity.com/questions/459132/getaxismouse-x-suddenly-faster-in-build.html)
            float Mouse_Input_X = Input.GetAxis("Mouse X") * m_MouseSensitivity;
            float Mouse_Input_Y = Input.GetAxis("Mouse Y") * m_MouseSensitivity;

            m_xRotation -= Mouse_Input_Y;
            m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);


            transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);

            m_playerTransform.Rotate(Vector3.up * Mouse_Input_X);
        }
    }
}
