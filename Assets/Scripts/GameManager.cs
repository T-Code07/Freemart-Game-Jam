using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freemart.Player.Control;

namespace Freemart.General
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] bool m_shouldBeFrozen = false;
        [SerializeField] PlayerController m_playerController;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_playerController.isMovementFrozen = m_shouldBeFrozen;
        }
    }
}
