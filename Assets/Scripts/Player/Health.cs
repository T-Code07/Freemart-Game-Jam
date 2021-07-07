using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Health : MonoBehaviour
{
    [SerializeField] float m_maxHealth = 10f;
    [SerializeField] TextMeshProUGUI m_healthText;
   
    //To be called in objects that do damage.
    public void DecreaseHealth(float damage) 
    {
        m_maxHealth -= damage;
    }

    private void Update()
    {
        m_healthText.text = m_maxHealth.ToString();
    }
}
