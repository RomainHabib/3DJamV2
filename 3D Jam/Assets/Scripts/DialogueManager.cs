using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Game Design")]
    [Space(10)]
    [Range(0.0f, 0.5f)] public float TimeBetweenLetters;
    [Range(0.0f, 5.0f)] public float TimeBeforeVanish;
    public bool NextDialWithKey;
    [Header("Autre")]
    public TextMeshProUGUI ReferenceText;
    private float timer;
    private float charCount;
    private bool textBeingWrited;
    private bool nextDialBool;
    private DialogueScriptableObject textSO;
    public DialogueScriptableObject[] dialoguesSO;



    public static DialogueManager Instance { get; protected set; }
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("2 DialogueManagers?");
        }
        Instance = this;

        nextDialBool = false;

        List<DialogueScriptableObject> list = new List<DialogueScriptableObject>();
        foreach (DialogueScriptableObject SO in Resources.LoadAll<DialogueScriptableObject>("Dialogues/")) list.Add(SO);
        dialoguesSO = list.ToArray();

       // textSO = dialoguesSO[0];
       // DisplayDialogue("Oklm");
    }

    void Update()
    {
        if (textBeingWrited)
        {
            TypeWriteText(); // Typewriter effect
        }
        else if (nextDialBool)
        {
            if (Input.GetKey(KeyCode.Mouse0)) // KeyCode pour skip dialogue
            {
                print("On affiche le dialogue d'après (skip): " + textSO.nextDialogue.name);
                DisplayDialogue(textSO.nextDialogue.name);
                textSO = null;
                nextDialBool = false;
            }
        }
        else
        {
            if (ReferenceText.maxVisibleCharacters != 0)
            {
                timer += Time.deltaTime;
                if(timer >= TimeBeforeVanish)
                {
                    timer = 0;
                    if(textSO == null || textSO.nextDialogue == null)
                    {
                        textSO = null;
                        ReferenceText.maxVisibleCharacters = 0;
                        return;
                    }
                    else
                    {
                        if (NextDialWithKey)
                        {
                            nextDialBool = true;
                            print("En attente du skip..");
                        }
                        else
                        {
                            print("On affiche le dialogue d'après: " + textSO.nextDialogue.name);
                            DisplayDialogue(textSO.nextDialogue.name);
                            textSO = null;
                            ReferenceText.maxVisibleCharacters = 0;
                        }
                    }

                    
                }
            }
        }

        
    }

    public void DisplayDialogue(string nomDuDialogue) // A appeller à chaque fois qu'on souhaite afficher un nv texte
    {
        textSO = dialogueNameToSO(nomDuDialogue);
        if (textSO == null)
        {
            print("DialogueSO non trouvé.");
            return;
        }

        string newText = textSO.dialogue;
        print("On affiche le dialogue: " + nomDuDialogue);
        ReferenceText.text = "";
        ReferenceText.maxVisibleCharacters = 0;
        ReferenceText.text = newText;
        timer = 0;
        charCount = newText.Length;
        textBeingWrited = true;
    }

    DialogueScriptableObject dialogueNameToSO(string name)
    {
        for (int i = 0; i < dialoguesSO.Length; i++)
        {
            if (dialoguesSO[i].name == name)
                return dialoguesSO[i];
        }
        print("Dialogue non trouvé: '" + name+"'" + " -- Mauvais nom dans le code ou dans les assets ?");
        return null;
    }

    void TypeWriteText()
    {
        timer += Time.deltaTime;
        if(timer >= TimeBetweenLetters)
        {
            timer = 0;
            ReferenceText.maxVisibleCharacters += 1;
            if (ReferenceText.maxVisibleCharacters >= charCount)
            {
                textBeingWrited = false;
                timer = 0;
            }
        }
    }
}
