using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class LoadBootstrapScene
    {
        [MenuItem("Load Scene/Switch to Bootstrapper Scene")]
            private static void SwitchToBootstrapperScene()
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity");
                }
            }

            [MenuItem("Load Scene/Switch to Game Scene")]
            private static void SwitchToGameScene()
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
                }
            }

            [MenuItem("Load Scene/Switch to GameBoss Scene")]
            private static void SwitchToGameBossScene()
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/GameBoss.unity");
                }
            }
    }
}
