using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PSDUIImporter
{
    //------------------------------------------------------------------------------
    // class definition
    //------------------------------------------------------------------------------
    public class PSDImportMenu : Editor
    {
        [MenuItem("QuickTool/PSDImport ...", false, 1)]
        static public void ImportPSD()
        {
            string inputFile = EditorUtility.OpenFilePanel("Choose PSDUI File to Import", Application.dataPath, "xml");

            if (!string.IsNullOrEmpty(inputFile) &&
                inputFile.StartsWith(Application.dataPath))
            {
                ImportPSDFromFile(inputFile);
            }

            GC.Collect();
        }

        static void ImportPSDFromFile(string assetPath)
        {
            Debug.Log($"import {assetPath}");
            PSDImporterConst.LoadConfig();  //重载wizard配置

            PSDImportCtrl import = new PSDUIImporter.PSDImportCtrl(assetPath);
            import.BeginDrawUILayers();
            import.BeginSetUIParents();

            GC.Collect();
        }

        [MenuItem("Assets/Convert PSD to UGUI ...")]
        public static void ImportPSDFromFile()
        {
            var obj = Selection.objects.FirstOrDefault();
            if (obj)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);
                if (!assetPath.EndsWith($".xml"))
                {
                    EditorUtility.DisplayDialog("Select xml file exported from psd to execute",
                        "You muse select xml file exported from psd", "ok");
                    return;
                }
                ImportPSDFromFile(assetPath);
            }
        }
    }
}