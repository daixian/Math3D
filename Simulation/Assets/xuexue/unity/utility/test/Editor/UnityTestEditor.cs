using UnityEditor;
using UnityEngine;

namespace xuexue.unity.utility.test
{
    /// <summary>
    /// 测试的Editor
    /// </summary>
    public class UnityTestEditor
    {
        /// <summary>
        /// 运行所有测试
        /// </summary>
        [MenuItem("测试/运行所有测试")]
        public static void Run()
        {
            ClearConsole();
            //因为这里没有传参,所以默认就把单元测试方法写到这个Assembly-CSharp里好了
            UnityTest.Run();
        }

        /// <summary>
        /// 尝试清一下控制台
        /// </summary>
        private static void ClearConsole()
        {
            try
            {
                var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
                var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                clearMethod.Invoke(null, null);
            }
            catch (System.Exception)
            {
            }
        }
    }
}