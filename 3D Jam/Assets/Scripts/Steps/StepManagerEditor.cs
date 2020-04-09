using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(StepsManager))]
public class StepManagerEditor : Editor
{
    StepsManager stepsManager;
    SerializedObject targetObject;

    SerializedProperty steps;
    List<bool> foldOutSteps = new List<bool>();
    List<List<bool>> foldOutCallbacks = new List<List<bool>>();
    //int foldOutIndex;

    private void OnEnable()
    {
        stepsManager = (StepsManager)target;
        targetObject = new SerializedObject(stepsManager);
        steps = targetObject.FindProperty("steps");
        //foldOutIndex = -1;
        //foldOutSteps = new List<bool>();
        for (int i = 0; i < steps.arraySize; i++)
        {
            foldOutSteps.Add(false);
        }

        //foldOutCallbacks = new List<List<bool>>();
        for (int i = 0; i < steps.arraySize; i++)
        {
            foldOutCallbacks.Add(new List<bool>());
            for (int j = 0; j < stepsManager.steps[i].steps.Count; j++)
            {
                foldOutCallbacks[i].Add(false);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        targetObject.Update();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add list"))
        {
            stepsManager.steps.Add(new StepsManager.StepList());

            steps.InsertArrayElementAtIndex(steps.arraySize);
            foldOutSteps.Add(false);
            foldOutCallbacks.Add(new List<bool>());
        }

        if (GUILayout.Button("Remove") && stepsManager.steps.Count > 0)
        {
            stepsManager.steps.RemoveAt(stepsManager.steps.Count - 1);

            steps.DeleteArrayElementAtIndex(steps.arraySize - 1);
            foldOutSteps.RemoveAt(foldOutSteps.Count - 1);
            foldOutCallbacks.RemoveAt(foldOutCallbacks.Count - 1);
        }
        GUILayout.EndHorizontal();

        for (int i = 0; i < steps.arraySize; i++)
        {
            SerializedProperty stepList = steps.GetArrayElementAtIndex(i).FindPropertyRelative("steps");
            SerializedProperty callbacksList = steps.GetArrayElementAtIndex(i).FindPropertyRelative("callbacks");
            //foldOutSteps[i] = EditorGUILayout.BeginFoldoutHeaderGroup(foldOutSteps[i], i.ToString());
            EditorGUILayout.BeginHorizontal();
            foldOutSteps[i] = EditorGUILayout.BeginFoldoutHeaderGroup(foldOutSteps[i], "");
            stepsManager.steps[i].name = EditorGUILayout.TextField(stepsManager.steps[i].name);
            EditorGUILayout.EndHorizontal();

            //if(EditorGUILayout.BeginFoldoutHeaderGroup(foldOutIndex == i, i.ToString()))
            //{
            //    foldOutIndex = i;
            //}

            //if (foldOutIndex == i)
            if (foldOutSteps[i])
            {
                EditorGUI.indentLevel++;
                for (int j = 0; j < stepList.arraySize; j++)
                {
                    //EditorGUILayout.ObjectField(j.ToString(), stepList.GetArrayElementAtIndex(j).objectReferenceValue, typeof(Step), false);
                    GUILayout.BeginHorizontal();
                    foldOutCallbacks[i][j] = EditorGUILayout.Foldout(foldOutCallbacks[i][j], "");
                    EditorGUILayout.PropertyField(stepList.GetArrayElementAtIndex(j));
                    if (stepsManager.steps[i].steps[j] != null)
                    {
                        EditorGUILayout.Toggle(stepsManager.steps[i].steps[j].Ended);
                    }
                    GUILayout.EndHorizontal();
                    if (foldOutCallbacks[i][j])
                    {
                        EditorGUILayout.PropertyField(callbacksList.GetArrayElementAtIndex(j));
                    }
                }

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Add step"))
                {
                    stepsManager.steps[i].steps.Add(null);
                    stepsManager.steps[i].callbacks.Add(null);
                    foldOutCallbacks[i].Add(false);
                }

                if (GUILayout.Button("Remove") && stepsManager.steps[i].steps.Count > 0)
                {
                    stepsManager.steps[i].steps.RemoveAt(stepsManager.steps[i].steps.Count - 1);
                    stepsManager.steps[i].callbacks.RemoveAt(stepsManager.steps[i].callbacks.Count - 1);
                    foldOutCallbacks[i].RemoveAt(foldOutCallbacks[i].Count - 1);
                }

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        targetObject.ApplyModifiedProperties();
    }
}
