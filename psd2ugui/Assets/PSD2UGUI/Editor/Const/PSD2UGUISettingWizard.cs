using PSDUIImporter;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PSD2UGUISettingWizard : ScriptableWizard
{
    [MenuItem("QuickTool/PSD2UGUISettingWizard",false,0)]
    private static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<PSD2UGUISettingWizard>("Create Light", "Create");
    }

    private PSD2UGUIConfig m_config;

    private void OnEnable()
    {
        LoadOrCreateConfig();
    }

    private void LoadOrCreateConfig()
    {
        if (File.Exists(PSDImporterConst.ConfigPath))
        {
            m_config = Instantiate(AssetDatabase.LoadAssetAtPath<PSD2UGUIConfig>(PSDImporterConst.ConfigPath));

            Debug.Log("读取配置");
        }
        else
        {
            m_config = CreateInstance<PSD2UGUIConfig>();
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.TextField("PSD2UGUI文件夹路径:", m_config._BaseFolderPath);
        if (GUILayout.Button("选择文件夹"))
        {
            string _path = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty).Replace('\\', '/');

            _path = GetValue(_path);

            m_config._BaseFolderPath = _path;
            if (!string.IsNullOrEmpty(_path))
            {
                PSDImporterConst.BaseFolder = _path;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();



        EditorGUILayout.BeginHorizontal();

        // 公用图片路径
        EditorGUILayout.TextField("公用图集路径:", m_config.m_commonAtlasPath);

        if (GUILayout.Button("选择文件夹"))
        {
            string _path = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty).Replace('\\', '/');

            _path = GetValue(_path);

            m_config.m_commonAtlasPath = _path;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        m_config.m_commonAtlasName = EditorGUILayout.TextField("公用图集名:", m_config.m_commonAtlasName);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();



        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.TextField("默认字体路径:", m_config.m_fontPath);

        if (GUILayout.Button("选择文件夹"))
        {
            string _path = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty).Replace('\\', '/');

            _path = GetValue(_path);

            m_config.m_fontPath = _path;
        }

        //m_config.m_fontPath =

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.TextField("默认静态字体路径:", m_config.m_staticFontPath);

        if (GUILayout.Button("选择文件夹"))
        {
            string _path = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty).Replace('\\', '/');

            _path = GetValue(_path);

            m_config.m_staticFontPath = _path;
        }

        //m_config.m_fontPath =

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();



        EditorGUILayout.BeginHorizontal();

        //EditorGUILayout.TextField("默认资源模板加载路径:", m_config.m_psduiTemplatePath);
        EditorGUILayout.LabelField("默认资源模板加载路径:", m_config.m_psduiTemplatePath);

        /*if (GUILayout.Button("选择文件夹"))
        {
            string _path = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, string.Empty).Replace('\\', '/');

            _path = GetValue(_path);

            m_config.m_psduiTemplatePath = _path;
        }*/

        //m_config.m_fontPath =

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();



        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("配置文件路径:", PSDImporterConst.ConfigPathRelative);

        EditorGUILayout.EndHorizontal();

        var str = File.Exists(PSDImporterConst.ConfigPath) ? "写入" : "创建";
        if (GUILayout.Button(str))
        {
            if (string.IsNullOrEmpty(m_config.m_commonAtlasPath) ||
                string.IsNullOrEmpty(m_config.m_commonAtlasName) ||
                string.IsNullOrEmpty(m_config.m_psduiTemplatePath) ||
                string.IsNullOrEmpty(m_config.m_fontPath))
            {
                ShowNotification(new GUIContent("配置路径不应该为空!"));

                return;
            }

            OnClickConfig();

            ShowNotification(new GUIContent("创建配置成功!"));

            //Close();
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        if (GUILayout.Button("根据路径重置"))
        {
            m_config.m_fontPath = PSDImporterConst.DefaultFontFolder;
            m_config.m_staticFontPath = PSDImporterConst.DefaultFontStaticFolder;
        }
    }

    private static string GetValue(string _path)
    {
        string _path2 = Application.dataPath.Replace('\\', '/');

        if (!_path.Contains("/Assets"))
        {
            Debug.LogError("必须选择UnityAssets路径下的文件夹!");

            return _path;
        }

        int _index = _path.IndexOf("/Assets", StringComparison.Ordinal);

        _path = _path.Substring(_index + 1, _path.Length - _index - 1);

        int idx = _path.LastIndexOf('/');
        string last = _path.Substring(idx, _path.Length - idx);
        if(last != "/")
        {
            _path = _path + "/";
        }
        Debug.Log("GetValue:" + _path);

        return _path;
    }

    /// <summary>
    /// 创建配置
    /// </summary>
    private void OnClickConfig()
    {
        //var _psd2UguiConfig = ScriptableObject.CreateInstance<PSD2UGUIConfig>();

        //_psd2UguiConfig = m_config;

        var path = PSDImporterConst.ConfigPath;
        var folder = Path.GetDirectoryName(path);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        AssetDatabase.DeleteAsset(path);

        AssetDatabase.CreateAsset(m_config, path);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("创建成功", "创建配置完成!", "确认");

        PSDImporterConst.LoadConfig();
    }
}