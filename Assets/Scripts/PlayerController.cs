using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code from Unity's example code: https://www.youtube.com/watch?v=_QajrabyTJc
public class PlayerController : MonoBehaviour
{
    private CharacterController m_controller;
    [SerializeField] float m_playerSpeed = 10f;
    [SerializeField] float m_gravity = -9.8f;
    [SerializeField] float m_jumpHeight = 5f;
    Vector3 m_velocity;
    private void Start()
    {
        m_controller = GetComponent<CharacterController>();
    }
    private void Update()
    {

        //The difference may be the direction needed to move?
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //"consider move an arrow pointing to the direction we want to move"
       //this makes x and z to the local area of movement relative to the player body. 
        Vector3 move = transform.right * x + transform.forward * z;

        //v needed to reach h (height):
        //v = sqrt(-2hg) (Physics)
        if (Input.GetButtonDown("Jump"))
        {
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * m_gravity);
        }
        //change in y = 1/2g * t^2 (Physics)
        m_controller.Move(move * m_playerSpeed * Time.deltaTime);

        //Multiply by time.deltatime twice to forfill the t^2
        m_velocity.y += m_gravity * Time.deltaTime;
        m_controller.Move(m_velocity * Time.deltaTime);

        if (m_controller.isGrounded) 
        {
            m_velocity = Vector3.zero;
        }

    }
}
