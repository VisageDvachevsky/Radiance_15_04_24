using UnityEditor;
using UnityEditor.SceneManagement;

namespace Project.Editor
{
    /// <summary>
    /// ����� �������������� �������� �������� ����� ��� ������� ���� � ������ ��������� Unity.
    /// </summary>
    [InitializeOnLoad]
    public static class MainSceneAutoLoader
    {
        /// <summary>
        /// ����������� ����������� ������.
        /// ���������� ������������� ��� �������� ������ � ��������� Unity.
        /// </summary>
        static MainSceneAutoLoader()
        {
            // ���������, ��� � ���������� ������ ���� ���� �� ���� �����
            if (EditorBuildSettings.scenes.Length == 0) return;

            // ������������� �������� ����� ��� ������� � ������ play � ���������
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}