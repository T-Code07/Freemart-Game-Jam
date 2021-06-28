using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Freemart.Player.Control
{
    //Code inspired by Unity's example code: https://www.youtube.com/watch?v=_QajrabyTJc
    public class PlayerController : MonoBehaviour
    {
        private CharacterController m_controller;

        [Header("Speed:")]
        [SerializeField] float m_playerSpeed = 10f;
        [SerializeField] float m_sprintBoost = 1.5f;
        [SerializeField] float m_crouchSpeedReduce = 0.5f;
        [SerializeField] float m_crouchHeight = 0.5f;
        [SerializeField] float m_crouchStepHeight = 0.2f;
        //todo: finish the defualt step height and implament

        [Space(5)]

        [Header("Jumping/Physics:")]
        [SerializeField] float m_jumpHeight = 5f;
        [SerializeField] float m_gravity = -9.8f;
        [SerializeField] Transform m_groundCheck;
        [SerializeField] float m_groundDistance = 0.4f;
        [SerializeField] LayerMask m_groundMask;

        private bool m_isGrounded;
        private Vector3 m_velocity;
        private bool m_isCrouching;
        private void Start()
        {
            m_controller = GetComponent<CharacterController>();
        }
        private void Update()
        {
            //Physics.CheckSphere projects a sphere from the given position with the radius of the given distance 
            //that returns true if any colliders are in it.
            //If a layer mask is provided, that means only check colliders in this layer. 
            m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);
            if (m_isGrounded && m_velocity.y < 0)
            {
                //Person found that because the sphere is a little bit lower than the actual body,
                //it will register that it is grounded before the body is on the ground.
                //Therefore, move the player to the ground if it thinks it is grounded. 
                //Set to height of the character.  
                m_velocity.y = -m_controller.height;
            }
            //The difference may be the direction needed to move?
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //"consider move an arrow pointing to the direction we want to move"
            //this makes x and z to the local area of movement relative to the player body. 
            Vector3 move = transform.right * x + transform.forward * z;

            //v needed to reach h (height):
            //v = sqrt(-2hg) (Physics)
            if (Input.GetButtonDown("Jump") && m_isGrounded && !m_isCrouching)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * m_gravity);
            }

            //change in y = 1/2g * t^2 (Physics)
            float speed = m_playerSpeed;
            if (Input.GetButton("Sprint") && !m_isCrouching)
            {
                //add playerSpeed to the sprin boost percentage of the playerSpeed:
                //speed = (playerSpeed * speed %) 
                speed = (m_playerSpeed * m_sprintBoost);
            }

            //Crouching reduces the speed and the height of the player
            if (Input.GetButton("Crouch")) 
            {
                m_isCrouching = true;
                speed = m_crouchSpeedReduce * m_playerSpeed;
                transform.localScale = new Vector3 (1, m_crouchHeight, 1) ;
            }
            else 
            {
                m_isCrouching = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
            m_controller.Move(move * speed * Time.deltaTime);

            //Multiply by time.deltatime twice to forfill the t^2
            m_velocity.y += m_gravity * Time.deltaTime;
            m_controller.Move(m_velocity * Time.deltaTime);
        }
    }
}
