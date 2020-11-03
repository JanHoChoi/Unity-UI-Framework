using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
public class TagAndLayerManager
{
    #region SortingLayer
    [MenuItem("GameTools/TagAndLayer管理器/SortingLayer")]
    public static void AddSortingLayer()
    {
        // 遍历枚举拿到所有的字符串
        List<string> lstScreenPriority = new List<string>();
        foreach(int v in Enum.GetValues(typeof(EScreenPriority)))
        {
            lstScreenPriority.Add(Enum.GetName(typeof(EScreenPriority), v));
        }

        // 清除数据
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        if(tagManager == null)
        {
            Debug.LogError("未能序列化tagManager!!");
            return;
        }
        SerializedProperty it = tagManager.GetIterator();
        while(it.NextVisible(true))
        {
            if (it.name != "m_SortingLayers")
            {
                Debug.Log(it.arraySize);
                continue;
            }
            // 先删除所有
            while(it.arraySize > 0)
            {
                it.DeleteArrayElementAtIndex(0);
            }

            // 重新插入
            // 将枚举字符串生成到sortingLayer
            foreach(var s in lstScreenPriority)
            {
                it.InsertArrayElementAtIndex(it.arraySize);
                SerializedProperty dataPoint = it.GetArrayElementAtIndex(it.arraySize - 1);

                while(dataPoint.NextVisible(true))
                {
                    if(dataPoint.name == "name")
                    {
                        dataPoint.stringValue = s;
                    }
                    else if(dataPoint.name == "uniqueID")
                    {
                        dataPoint.intValue = (int)Enum.Parse(typeof(EScreenPriority), s);
                    }
                }
            }
        }
    }

    public static bool IsHaveSortingLayer(string sortingLayer)
    {
        
    }
    #endregion
}
#endif