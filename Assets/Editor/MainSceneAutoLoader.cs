using UnityEditor;
using UnityEditor.SceneManagement;

namespace Project.Editor
{
    /// <summary>
    /// Класс автоматической загрузки основной сцены при запуске игры в режиме редактора Unity.
    /// </summary>
    [InitializeOnLoad]
    public static class MainSceneAutoLoader
    {
        /// <summary>
        /// Статический конструктор класса.
        /// Вызывается автоматически при загрузке класса в редакторе Unity.
        /// </summary>
        static MainSceneAutoLoader()
        {
            // Проверяем, что в настройках сборки есть хотя бы одна сцена
            if (EditorBuildSettings.scenes.Length == 0) return;

            // Устанавливаем основную сцену для запуска в режиме play в редакторе
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
    }
}