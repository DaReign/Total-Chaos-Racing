using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad]
public static class SetupTestHUD
{
    static SetupTestHUD()
    {
        // Run after scene is fully loaded
        EditorApplication.update += TryAutoSetup;
    }

    static void TryAutoSetup()
    {
        // Wait until scene is loaded and not compiling
        if (EditorApplication.isCompiling) return;
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;

        // Unsubscribe immediately
        EditorApplication.update -= TryAutoSetup;

        // Only run once per session
        if (SessionState.GetBool("TestHUD_Setup_v2", false)) return;
        SessionState.SetBool("TestHUD_Setup_v2", true);

        // Check if TestHUD already exists
        if (GameObject.Find("TestHUD") != null)
        {
            Debug.Log("[SetupTestHUD] TestHUD already exists. Skipping.");
            return;
        }

        Setup();
    }

    [MenuItem("Tools/UI Toolkit/Setup TestHUD in Scene")]
    static void Setup()
    {
        // Find or create PanelSettings
        var panelSettings = AssetDatabase.LoadAssetAtPath<PanelSettings>("Assets/MyAssets/UI/Styles/RacingPanelSettings.asset");
        if (panelSettings == null)
        {
            panelSettings = ScriptableObject.CreateInstance<PanelSettings>();
            panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
            panelSettings.referenceResolution = new Vector2Int(1920, 1080);
            panelSettings.screenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;
            panelSettings.match = 0.5f;
            panelSettings.sortingOrder = 100;

            AssetDatabase.CreateAsset(panelSettings, "Assets/MyAssets/UI/Styles/RacingPanelSettings.asset");
            AssetDatabase.SaveAssets();
            Debug.Log("[SetupTestHUD] Created PanelSettings asset");
        }

        // Load UXML
        var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/MyAssets/UI/Screens/TestHUD.uxml");
        if (uxml == null)
        {
            Debug.LogError("[SetupTestHUD] TestHUD.uxml not found at Assets/MyAssets/UI/Screens/TestHUD.uxml");
            return;
        }

        // Check if TestHUD already exists
        var existing = GameObject.Find("TestHUD");
        if (existing != null)
        {
            Debug.LogWarning("[SetupTestHUD] TestHUD already exists in scene. Skipping.");
            return;
        }

        // Create GameObject
        var go = new GameObject("TestHUD");
        Undo.RegisterCreatedObjectUndo(go, "Create TestHUD");

        // Add UIDocument
        var doc = go.AddComponent<UIDocument>();
        doc.panelSettings = panelSettings;
        doc.visualTreeAsset = uxml;

        // Add controller
        go.AddComponent<TestHUDController>();

        // Mark scene dirty
        EditorUtility.SetDirty(go);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
            UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());

        Debug.Log("[SetupTestHUD] TestHUD GameObject created with UIDocument + TestHUDController. Save scene with Ctrl+S.");
    }
}
