using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UberPlanetary._Project.Scripts.Editor
{
    public class ShaderOccurenceWindow : EditorWindow
    {
        [MenuItem("Tools/Shader Occurence")]
        public static void Open()
        {
            GetWindow<ShaderOccurenceWindow>();
        }
 
        Shader shader;
        string shaderKeyword;
        List<string> materials = new List<string>();
        Vector2 scroll;

        
        void OnGUI()
        {
            GUILayout.Label("Select a shader");
            Shader prev = shader;
            shader = EditorGUILayout.ObjectField(shader, typeof(Shader), false) as Shader;
            GUILayout.Label("Type out keyword, case sensitive");

            shaderKeyword = EditorGUILayout.TextField("Shader Keyword to enable : ", shaderKeyword);

            if (shader != prev)
            {
                string shaderPath = AssetDatabase.GetAssetPath(shader);
                string[] allMaterials = AssetDatabase.FindAssets("t:Material");
                materials.Clear();
                for (int i = 0; i < allMaterials.Length; i++)
                {
                    allMaterials[i] = AssetDatabase.GUIDToAssetPath(allMaterials[i]);
                    string[] dep = AssetDatabase.GetDependencies(allMaterials[i]);
                    if (ArrayUtility.Contains(dep, shaderPath))
                        materials.Add(allMaterials[i]);
                }
            }
            
            scroll = GUILayout.BeginScrollView(scroll);
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label(Path.GetFileNameWithoutExtension(materials[i]));
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Show"))
                            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(materials[i], typeof(Material)));
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
            
            if (GUILayout.Button("Enable keyword"))
            {
                for (int i = 0; i < materials.Count; i++)
                {
                    Material mat = AssetDatabase.LoadAssetAtPath(materials[i], typeof(Material)) as Material;
                    //mat.EnableKeyword(keywordsToAdd[i]);
                    if (mat != null && !mat.IsKeywordEnabled(shaderKeyword))
                    {
                        mat.EnableKeyword(shaderKeyword);
                    }
                }
            }
        }
    }
}