using Ink.UnityIntegration;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "ScriptableObjects/Cutscene")]
public class Cutscene : ScriptableObject
{
    public int cutsceneID;
    public Sprite backgroundImage;
    public TextAsset inkFile;
}