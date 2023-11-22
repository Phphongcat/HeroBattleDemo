using UnityEngine;
using Debug = UnityEngine.Debug;

public class DebugEditor
{
	//-----------------------------------
	//--------------------- Log , warning, 

	public static void Log(object message)
	{
#if UNITY_EDITOR
		Debug.Log(message);
#endif
	}

	public static void Log(string format, params object[] args)
	{
#if UNITY_EDITOR
		Debug.Log(string.Format(format, args));
#endif
	}

	public static void LogWarning(string message, Object context = null)
	{
#if UNITY_EDITOR
		Debug.LogWarning(message, context);
#endif
	}

	public static void LogWarning(Object context, string format, params object[] args)
	{
#if UNITY_EDITOR
		Debug.LogWarning(string.Format(format, args), context);
#endif
	}

	public static void Warning(bool condition, object message)
	{
#if UNITY_EDITOR
		if (!condition) Debug.LogWarning(message);
#endif
	}

	public static void Warning(bool condition, object message, Object context)
	{
#if UNITY_EDITOR
		if (!condition) Debug.LogWarning(message, context);
#endif
	}

	public static void Warning(bool condition, Object context, string format, params object[] args)
	{
#if UNITY_EDITOR
		if (!condition) Debug.LogWarning(string.Format(format, args), context);
#endif
	}


	//---------------------------------------------
	//------------- Assert ------------------------

	/// Thown an exception if condition = false
	public static void Assert(bool condition)
	{
#if UNITY_EDITOR
		if (!condition) throw new UnityException();
#endif
	}

	/// Thown an exception if condition = false, show message on console's log
	public static void Assert(bool condition, string message)
	{
#if UNITY_EDITOR
		if (!condition) throw new UnityException(message);
#endif
	}

	/// Thown an exception if condition = false, show message on console's log
	public static void Assert(bool condition, string format, params object[] args)
	{
#if UNITY_EDITOR
		if (!condition) throw new UnityException(string.Format(format, args));
#endif
	}
}