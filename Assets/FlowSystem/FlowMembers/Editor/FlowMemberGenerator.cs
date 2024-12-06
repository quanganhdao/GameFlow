using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlowMemberGenerator))]
public class FlowMemberGenerator : EditorWindow
{
	private GameObject flowPrefab;
	private MonoScript scriptReference;
	private const string prefabKey = "FlowPrefab";
	private const string ScriptKey = "SavedScriptPath";



	[MenuItem("Tools/Flow Member Generator %#m")]
	public static void ShowWindow()
	{
		// Hiển thị cửa sổ
		GetWindow<FlowMemberGenerator>("Flow Member Generator");
	}

	private void OnEnable()
	{
		// Tải đường dẫn Prefab đã lưu
		string savedPath = EditorPrefs.GetString(prefabKey, null);

		if (!string.IsNullOrEmpty(savedPath))
		{
			// Tìm đối tượng Prefab dựa trên đường dẫn
			flowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(savedPath);

			if (flowPrefab == null)
			{
				Debug.LogWarning("Saved Prefab not found. It may have been moved or deleted.");
			}
		}

		string savedScriptPath = EditorPrefs.GetString(ScriptKey, null);
		if (!string.IsNullOrEmpty(savedScriptPath))
		{
			scriptReference = AssetDatabase.LoadAssetAtPath<MonoScript>(savedScriptPath);
		}
	}
	private void OnDisable()
	{
		// Lưu đường dẫn Prefab khi đóng cửa sổ
		if (flowPrefab != null)
		{
			string prefabPath = AssetDatabase.GetAssetPath(flowPrefab);
			EditorPrefs.SetString(prefabKey, prefabPath);
		}
		else
		{
			EditorPrefs.DeleteKey(prefabKey); // Xóa giá trị nếu không có Prefab
		}


		if (scriptReference != null)
		{
			string scriptPath = AssetDatabase.GetAssetPath(scriptReference);
			EditorPrefs.SetString(ScriptKey, scriptPath);
		}
		else
		{
			EditorPrefs.DeleteKey(ScriptKey);
		}
	}
	private void OnGUI()
	{
		GUILayout.Label("Drag and Drop Prefab Here", EditorStyles.boldLabel);

		// Hiển thị trường ObjectField
		flowPrefab = (GameObject)EditorGUILayout.ObjectField(
			"Prefab", // Nhãn
			flowPrefab, // Giá trị hiện tại
			typeof(GameObject), // Kiểu đối tượng cho phép
			false // Chỉ cho phép kéo thả từ Project, không phải từ Hierarchy
		);

		// Hiển thị thông báo khi có tham chiếu
		if (flowPrefab != null)
		{
			GUILayout.Label($"Prefab Selected: {flowPrefab.name}", EditorStyles.helpBox);
		}


		scriptReference = (MonoScript)EditorGUILayout.ObjectField(
			"Script",
			scriptReference,
			typeof(MonoScript),
			false // Chỉ cho phép từ Project
		);

		// Hiển thị thông báo Script
		if (scriptReference != null)
		{
			GUILayout.Label($"Script Selected: {scriptReference.name}", EditorStyles.helpBox);
		}
		else
		{
			GUILayout.Label("No Script Selected", EditorStyles.helpBox);
		}
	}
}