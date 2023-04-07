using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
   [Header("Максимальное")]
   [SerializeField] private int m_maxHealth = 100;
   [SerializeField] private int m_maxEnergy = 100;
   [SerializeField] private int m_MaxOxygen = 100;
   [Header("Текущее")]
   [SerializeField] private int m_health = 100;
   [SerializeField] private int m_energy = 100;
   [SerializeField] private int m_oxygen = 100;

   public event Action OnFuelOut;
   public event Action OnHealthOut;
   public event Action OnOxygenOut;

   public void Repair(int value)
   {
      m_health += value;
      if (m_health > m_maxHealth)
         m_health = m_maxHealth;
   }

   public void FillEnergy(int value)
   {
      m_energy += value;
      if (m_energy > m_maxEnergy)
         m_energy = m_maxEnergy;
   }

   public void FillOxygen(int value)
   {
      m_oxygen += value;
      if (m_oxygen > m_MaxOxygen)
         m_oxygen = m_MaxOxygen;
   }

   public void SpendEnergy(int value)
   {
      m_energy -= value;
      if (m_energy <= 0)
      {
         m_energy = 0;
         OnFuelOut?.Invoke();
      }
   }

   public void SpendOxygen(int value)
   {
      m_oxygen -= value;
      if (m_oxygen <= 0)
      {
         m_oxygen = 0;
         OnOxygenOut?.Invoke();
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