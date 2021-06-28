using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement code inspired by: https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
public class PlayerController : MonoBehaviour
{
    private CharacterController m_controller;
    [SerializeField] float m_playerSpeed = 10f;
    private void Start()
    {
        m_controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //Code from Unity's example code: https://www.youtube.com/watch?v=_QajrabyTJc

        //The difference may be the direction needed to move?
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //"consider move an arrow pointing to the direction we want to move"
       //this makes x and z to the local area of movement relative to the player body. 
        Vector3 move = transform.right * x + transform.forward * z;
       
        m_controller.Move(move * m_playerSpeed * Time.deltaTime);
    


    }
}
