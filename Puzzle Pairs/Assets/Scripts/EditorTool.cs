using UnityEditor;
using UnityEngine;

public class EditorTool : EditorWindow
{
    string assetBundleName;
    Sprite xSprite;
    Sprite oSprite;
    Sprite backGround;

    [MenuItem("Tools/Editor Tools")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorTool));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Delete Save File"))
        {
            DeleteSaves();
        }
    }

    void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
