using UnityEngine;

public class LocalPoint : Point
{
   [SerializeField] private LocalPointQuestDataSO m_quest;

   public LocalPointQuestDataSO Quest => m_quest;

   public void SetQuest(LocalPointQuestDataSO quest) => m_quest = quest;
}