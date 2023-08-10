using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace PSDUIImporter
{
    public class PSDImporterConst
    {
        public const string PNG_SUFFIX = ".png";
        public const string JPG_SUFFIX = ".jpg";

        public static string DefaultBaseFolder = "Assets/PSD2UGUI/";
        public static string BaseFolder = DefaultBaseFolder;
        public static string DefaultConfigPath => GetDefaultConfigPath(BaseFolder);
        public static string GetDefaultConfigPath(string baseFolder) => baseFolder + "PSD2UGUIConfig.asset";
        public static string __CONFIG_PATH => DefaultConfigPath;

        /// <summary>
        /// 公用图片路径，按需修改
        /// </summary>
        public static string Globle_BASE_FOLDER = "Assets/Textures/HomeCommon/";

        /// <summary>
        /// 图集文件名
        /// </summary>
        public static string Globle_FOLDER_NAME = "HomeCommon";

        /// <summary>
        ///
        /// </summary>
        public const string RENDER = "Render";

        public const string NINE_SLICE = "9Slice";

        public const string IMAGE = "Image";

        public static string DefaultFontFolder => BaseFolder + "Font/";
        /// <summary>
        /// 字体路径，按需修改
        /// </summary>
        public static string FONT_FOLDER = DefaultFontFolder;
        public static string DefaultFontStaticFolder => BaseFolder + "Font/StaticFont/";

        public static string FONT_STATIC_FOLDER = DefaultFontStaticFolder;

        public const string FONT_SUFIX = ".ttf";

        /// <summary>
        /// 修改资源模板加载路径,不能放在resources目录
        /// </summary>
        public static string PSDUI_PATH => BaseFolder + "Template/UI/";

        public const string PSDUI_SUFFIX = ".prefab";

        public static string ASSET_PATH_EMPTY = PSDUI_PATH + "Empty" + PSDUI_SUFFIX;
        public static string ASSET_PATH_BUTTON = PSDUI_PATH + "Button" + PSDUI_SUFFIX;
        public static string ASSET_PATH_TOGGLE = PSDUI_PATH + "Toggle" + PSDUI_SUFFIX;
        public static string ASSET_PATH_CANVAS = PSDUI_PATH + "Canvas" + PSDUI_SUFFIX;
        public static string ASSET_PATH_EVENTSYSTEM = PSDUI_PATH + "EventSystem" + PSDUI_SUFFIX;
        public static string ASSET_PATH_GRID = PSDUI_PATH + "Grid" + PSDUI_SUFFIX;
        public static string ASSET_PATH_IMAGE = PSDUI_PATH + "Image" + PSDUI_SUFFIX;
        public static string ASSET_PATH_RAWIMAGE = PSDUI_PATH + "RawImage" + PSDUI_SUFFIX;
        public static string ASSET_PATH_HALFIMAGE = PSDUI_PATH + "HalfImage" + PSDUI_SUFFIX;
        public static string ASSET_PATH_SCROLLVIEW = PSDUI_PATH + "ScrollView" + PSDUI_SUFFIX;
        public static string ASSET_PATH_SLIDER = PSDUI_PATH + "Slider" + PSDUI_SUFFIX;
        public static string ASSET_PATH_TEXT = PSDUI_PATH + "Text" + PSDUI_SUFFIX;
        public static string ASSET_PATH_SCROLLBAR = PSDUI_PATH + "Scrollbar" + PSDUI_SUFFIX;
        public static string ASSET_PATH_GROUP_V = PSDUI_PATH + "VerticalGroup" + PSDUI_SUFFIX;
        public static string ASSET_PATH_GROUP_H = PSDUI_PATH + "HorizontalGroup" + PSDUI_SUFFIX;
        public static string ASSET_PATH_INPUTFIELD = PSDUI_PATH + "InputField" + PSDUI_SUFFIX;
        public static string ASSET_PATH_LAYOUTELEMENT = PSDUI_PATH + "LayoutElement" + PSDUI_SUFFIX;
        public static string ASSET_PATH_TAB = PSDUI_PATH + "Tab" + PSDUI_SUFFIX;
        public static string ASSET_PATH_TABGROUP = PSDUI_PATH + "TabGroup" + PSDUI_SUFFIX;

        public PSDImporterConst()
        {
            LoadConfig();
        }

        /// <summary>
        /// 读取工具
        /// </summary>
        public static void LoadConfig([CallerFilePath] string filePath = default)
        {
            var configPath = __CONFIG_PATH;
            if (!string.IsNullOrEmpty(filePath))
            {
                var convertedFilePath = filePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                convertedFilePath = Path.GetDirectoryName(convertedFilePath);
                //convertedFilePath = $"{convertedFilePath}/../../";
                convertedFilePath = Path.GetDirectoryName(convertedFilePath);
                convertedFilePath = Path.GetDirectoryName(convertedFilePath);
                convertedFilePath = convertedFilePath.Replace(@"\", "/");
                var basePath = convertedFilePath.Replace(Application.dataPath, "Assets");
                //basePath.Replace("Editor/Core/", "");
                basePath = $"{basePath}/";
                configPath = GetDefaultConfigPath(basePath);
            }
            // 重读资源路径
            PSD2UGUIConfig _config = AssetDatabase.LoadAssetAtPath<PSD2UGUIConfig>(configPath);

            if (_config != null)
            {
                BaseFolder = _config._BaseFolderPath;
                Globle_BASE_FOLDER = _config.m_commonAtlasPath;
                Globle_FOLDER_NAME = _config.m_commonAtlasName;
                FONT_FOLDER = _config.m_fontPath;
                FONT_STATIC_FOLDER = _config.m_staticFontPath;
                //PSDUI_PATH = _config.m_psduiTemplatePath;

				// 重生成路径
                ASSET_PATH_EMPTY = PSDUI_PATH + "Empty" + PSDUI_SUFFIX;
                ASSET_PATH_BUTTON = PSDUI_PATH + "Button" + PSDUI_SUFFIX;
                ASSET_PATH_TOGGLE = PSDUI_PATH + "Toggle" + PSDUI_SUFFIX;
                ASSET_PATH_CANVAS = PSDUI_PATH + "Canvas" + PSDUI_SUFFIX;
                ASSET_PATH_EVENTSYSTEM = PSDUI_PATH + "EventSystem" + PSDUI_SUFFIX;
                ASSET_PATH_GRID = PSDUI_PATH + "Grid" + PSDUI_SUFFIX;
                ASSET_PATH_IMAGE = PSDUI_PATH + "Image" + PSDUI_SUFFIX;
                ASSET_PATH_RAWIMAGE = PSDUI_PATH + "RawImage" + PSDUI_SUFFIX;
                ASSET_PATH_HALFIMAGE = PSDUI_PATH + "HalfImage" + PSDUI_SUFFIX;
                ASSET_PATH_SCROLLVIEW = PSDUI_PATH + "ScrollView" + PSDUI_SUFFIX;
                ASSET_PATH_SLIDER = PSDUI_PATH + "Slider" + PSDUI_SUFFIX;
                ASSET_PATH_TEXT = PSDUI_PATH + "Text" + PSDUI_SUFFIX;
                ASSET_PATH_SCROLLBAR = PSDUI_PATH + "Scrollbar" + PSDUI_SUFFIX;
                ASSET_PATH_GROUP_V = PSDUI_PATH + "VerticalGroup" + PSDUI_SUFFIX;
                ASSET_PATH_GROUP_H = PSDUI_PATH + "HorizontalGroup" + PSDUI_SUFFIX;
                ASSET_PATH_INPUTFIELD = PSDUI_PATH + "InputField" + PSDUI_SUFFIX;
                ASSET_PATH_LAYOUTELEMENT = PSDUI_PATH + "LayoutElement" + PSDUI_SUFFIX;
                ASSET_PATH_TAB = PSDUI_PATH + "Tab" + PSDUI_SUFFIX;
                ASSET_PATH_TABGROUP = PSDUI_PATH + "TabGroup" + PSDUI_SUFFIX;

                Debug.Log("Load config.");
            }

            // Debug.LogError(_config.m_staticFontPath);
        }
    }
}