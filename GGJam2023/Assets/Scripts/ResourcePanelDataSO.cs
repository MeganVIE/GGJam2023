using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "create " + nameof(ResourcePanelDataSO))]
public class ResourcePanelDataSO : ScriptableObject
{
    public ResourceType Type;
    public string ResourceName;
    public string ResourceDisc;
    public bool NeedUseButton;
}