using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.IO;

public class ParentWindow : EditorWindow
{
    [DllImport("LoggingPlugin")]
    public static extern void Log(string a_Objectname, string a_Item, string a_Value);

    [DllImport("LoggingPlugin")]
    public static extern IntPtr LoadFile(string FileName);

    bool myBool;
    List<string> ParentMap = new List<string>();
    [MenuItem("Window/Parent Window Editor")]
    static void ShowWindow()
    {
        GetWindow<ParentWindow>("Parent Window Editor");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Save Parents"))
        {
            GameObject root = GameObject.Find("ParentManager");
            forEachChild(root.transform);
            for (int i = 0; i < ParentMap.Count; i++)
            {
                Debug.Log(ParentMap[i]);
                Log("Root","Parent",ParentMap[i]);
            }
            
        }
        if (GUILayout.Button("Load Parents"))
        {
            string[] stringSeparators = new string[] { " Parent " };
            //this is where you should do the loading of the text file and assignment of parents for the objects
            string loadedData = Marshal.PtrToStringAnsi(LoadFile("Root/Parent.txt"));
            ParentMap.Clear();
            using (StringReader reader = new StringReader(loadedData))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    Debug.Log("did " + line);
                    if (line != null) {

                        string[] ssize = line.Split(stringSeparators, StringSplitOptions.None);

                        GameObject CurrentObject = GameObject.Find(ssize[0]);
                        CurrentObject.transform.parent = GameObject.Find(ssize[1]).transform;

                    }
                    
                } while (line != null);
            }
        }

    }


    string forEachChild(Transform T)
    {

        foreach (Transform child in T)
        {
                ParentMap.Add(forEachChild(child));
        }
        if (T.parent != null)
            return T.name + " Parent " + T.parent.name;
        else
            return T.name;

    }
}
