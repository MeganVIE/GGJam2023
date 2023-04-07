using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
   [Header("Максимальное")]
   [SerializeField] private int m_maxHealth = 100;
   [SerializeField] private int m_maxFuel = 100;
   [Header("Текущее")]
   [SerializeField] private int m_health = 100;
   [SerializeField] private int m_fuel = 100;

   public event Action OnFuelOut;
   public event Action OnHealthOut;

   public void Repair(int value)
   {
      m_health += value;
      if (m_health > m_maxHealth)
         m_health = m_maxHealth;
   }

   public void Fill(int value)
   {
      m_fuel += value;
      if (m_fuel > m_maxFuel)
         m_fuel = m_maxFuel;
   }

   public void SpendFuel(int value)
   {
      m_fuel -= value;
      if (m_fuel <= 0)
      {
         m_fuel = 0;
         OnFuelOut?.Invoke();
      }
   }

   public void SpendHealth(int value)
   {
      m_health -= value;
      if (m_health <= 0)
      {
         m_health = 0;
         OnHealthOut?.Invoke();
      }
   }
}