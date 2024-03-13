using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MonLab;
using UnityEditor;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static int[] GetNoneOverlapNumbers(int maxCount, int n)
    {
        int[] source = new int[maxCount];
        for (int i = 0; i < maxCount; i++) { source[i] = i; }

        int[] result = new int[n];
        int pick;

        for (int i = 0; i < n; i++)
        {
            pick = Random.Range(0, maxCount);

            result[i] = source[pick];
            source[pick] = source[maxCount - 1];
            maxCount -= 1;
        }

        return result;
    }
#if UNITY_EDITOR

    [MenuItem("smilejsu/Remove All Missing Script Components")]
    private static void RemoveAllMissingScriptComponents() {

        Object[] deepSelectedObjects = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);

        Debug.Log(deepSelectedObjects.Length);

        int componentCount = 0;
        int gameObjectCount = 0;

        foreach (Object obj in deepSelectedObjects)
        {
            if (obj is GameObject go)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                //Debug.LogFormat("<color=cyan>{0}</color>", count);

                if (count > 0) {
                    Undo.RegisterCompleteObjectUndo(go, "Remove Missing Scripts");

                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);

                    componentCount += count;
                    gameObjectCount++;
                }

            }
        }

    }
    
    private static List<VrButtonTest> GetAllObjectInHierarchy()
    {
        List<VrButtonTest> list = new List<VrButtonTest>();
        
        var selectedGameObjects = Selection.gameObjects;
        
        foreach (GameObject selectedGameObject in selectedGameObjects)
        {
            VrButtonTest vrButtonTest = selectedGameObject.GetComponent<VrButtonTest>();

            if (!(ReferenceEquals(vrButtonTest, null)))
            {
                list.Add(vrButtonTest);
            }
        }
        
        return list;
    }

    [MenuItem("__CSG__/VrButton/TestScript/On")]
    public static void VrButtonTestScriptOn()
    {
        List<VrButtonTest> list = GetAllObjectInHierarchy();

        foreach (VrButtonTest o in list)
        {
            o.enabled = true;
        }
    }
    
    [MenuItem("__CSG__/VrButton/TestScript/Off")]
    public static void VrButtonTestScriptOff()
    {
        List<VrButtonTest> list = GetAllObjectInHierarchy();

        foreach (VrButtonTest o in list)
        {
            o.enabled = false;
        }
    }
    #endif
}
