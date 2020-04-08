using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Creer un nouveau objet dialogue", order = 1)]

public class DialogueScriptableObject : ScriptableObject
{
    [Header("!! RENOMMER L'OBJET !!")]
    //public string nomDuDialogue;
    [TextArea]
    [Space(10)]
    public string dialogue;
    [Header("Dialogue a afficher apres?")]
    public DialogueScriptableObject nextDialogue;
}