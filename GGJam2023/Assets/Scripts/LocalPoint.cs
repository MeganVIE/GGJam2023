using UnityEngine;

public class LocalPoint : Point
{
   [SerializeField] private SpriteRenderer m_icon;

   public void SetIcon(Sprite sprite) => m_icon.sprite = sprite;
}