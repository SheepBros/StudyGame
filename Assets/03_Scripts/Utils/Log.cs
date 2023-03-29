using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace TRTS.Util
{
    public static class Log
    {
        [Conditional("ENABLE_DEBUG_LOGS")]
        public static void DebugInfo(string message)
        {
            Debug.Log(message);
        }
        
        [Conditional("ENABLE_DEBUG_LOGS")]
        public static void DebugInfo(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }
        
        [Conditional("ENABLE_DEBUG_LOGS")]
        public static void DebugWarning(string message)
        {
            Debug.LogWarning(message);
        }
        
        [Conditional("ENABLE_DEBUG_LOGS")]
        public static void DebugError(string message, params object[] args)
        {
            Debug.LogErrorFormat(message, args);
        }
        
        [Conditional("ENABLE_DEBUG_LOGS")]
        public static void DebugError(string message)
        {
            Debug.LogError(message);
        }
        
        [Conditional("ENABLE_DEBUG_LOGS")]
        public static void DebugError(Exception exception)
        {
            Debug.LogError(exception.Message);
        }
        
        [Conditional("ENABLE_RELEASE_LOGS")]
        public static void Info(string message)
        {
            Debug.Log(message);
        }
        
        [Conditional("ENABLE_RELEASE_LOGS")]
        public static void Info(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }
        
        [Conditional("ENABLE_RELEASE_LOGS")]
        public static void Warning(string message)
        {
            Debug.LogWarning(message);
        }
        
        [Conditional("ENABLE_RELEASE_LOGS")]
        public static void Error(string message)
        {
            Debug.LogError(message);
        }
        
        [Conditional("ENABLE_RELEASE_LOGS")]
        public static void Error(string message, params object[] args)
        {
            Debug.LogErrorFormat(message, args);
        }
        
        [Conditional("ENABLE_RELEASE_LOGS")]
        public static void Error(Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }
}