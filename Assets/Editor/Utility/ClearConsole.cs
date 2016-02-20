using UnityEditor;

public class ClearConsole : Editor
{
	[MenuItem ("Tools/ClearLog %l")]
	public static void ClearLog ()
	{
		System.Type logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		System.Reflection.MethodInfo clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null,null);
	}
}
