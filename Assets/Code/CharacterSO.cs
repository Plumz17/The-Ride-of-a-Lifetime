using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/CharacterScriptableObject")]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
}