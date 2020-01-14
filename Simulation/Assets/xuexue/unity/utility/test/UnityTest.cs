using System;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace xuexue.unity.utility.test
{
    /// <summary>
    /// Unity下的单元测试方法
    /// </summary>
    public class UnityTest
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary> 运行所有测试. </summary>
        ///
        /// <remarks> Surface, 2020/1/14. </remarks>
        ///
        /// <param name="AssemblyNameList"> (Optional)搜索方法的程序集名数组. </param>
        ///-------------------------------------------------------------------------------------------------
        public static void Run(string[] AssemblyNameList = null)
        {
            Assembly assembly = null;
            Assembly[] arr = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in arr)
            {

                if (item.GetName().Name == "Assembly-CSharp" ||
                    (AssemblyNameList != null && AssemblyNameList.Contains(item.GetName().Name)))//包含传参进来的这个
                {
                    assembly = item;
                    break;
                }
            }

            Type[] typeArr = assembly.GetTypes();

            foreach (Type t in typeArr)
            {
                if (t.IsDefined(typeof(TestClass), false))
                {
                    object obj = Activator.CreateInstance(t);
                    var methods = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (var m in methods)
                    {
                        if (m.IsDefined(typeof(TestMethod)))
                        {
                            try
                            {
                                m.Invoke(obj, new object[] { });
                                Debug.Log($"<color=#00ff00ff>UnityTest.Run():执行测试  {t.Name}.{m.Name}  通过！</color>");
                            }
                            catch (Exception e)
                            {
                                Debug.LogError($"UnityTest.Run():执行测试  {t.Name}.{m.Name}  异常:{e.StackTrace}");
                            }
                        }
                    }
                }
            }
        }
    }
}