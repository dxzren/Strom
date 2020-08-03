using UnityEngine;
using System;
using System.IO;
using SimpleJson;
using System.Collections;

namespace StormBattle
{
    public class BattleUtil
    {
        public static int           LightMapLayer                   = 13;                                                   /// 场景层,场景灯光贴图层,过滤灯光
        
        #region================================================||   ResourceLoad --    资源数据加载      ||<FourNode>================================================        
        public static               UnityEngine.Object Load         ( string inPath )                                       // 加载资源         
        {
            string                  ResName                         = "";
            UnityEngine.Object      TheObj                          = null;

            if ( inPath.Trim().EndsWith("/0") || inPath.Trim().EndsWith("/"))                                               /// 资源路径尾部(0,/)返回
            {   Debug.LogError("LoadRes is Error: " + inPath);      return null ;      }

            ResName                 = Path.GetFileNameWithoutExtension(inPath);                                             /// 下载源查找文件( 文件名<不带扩展名>) 过滤路径
#if UNITY_ANDROID && !UNITY_EDITOR && !Force_Loacl                                                                          /// Android 模式_(非编辑和强制本地 模式)
            if      ( inPath.Contains("RoleModel"))
            {
                string[]             TheStrArr                      = inPath.Split('/');                                    // 路径'/'分割 字符串数组
                string               TheModel                       = TheStrArr[TheStrArr.Length - 1];                      // 路径最后字符串<文件名>
                TheObj              = ResourceManager.Instance.LoadAsset<UnityEngine.Object>(AssetBundleName.commonhero, TheModel); /// 加载资源对象
            }
            else if ( inPath.Contains("RoleEffect"))
            {
                string[]            TheStrArr                       = inPath.Split('/');                                    // 路径'/'分割 字符串数组
                string              TheModel                        = TheStrArr[TheStrArr.Length - 1];                      // 路径最后字符串<文件名>
                TheObj              = ResourceManager.Instance.LoadAsset<UnityEngine.Object>(AssetBundleName.effect, TheModel);     /// 加载资源对象
            }
            else
            {
#endif
            TheObj                  = Resources.Load(inPath);
#if UNITY_ANDROID && !UNITY_EDITOR && !Force_Local                                                                          /// Android 模式_(非编辑和强制本地 模式)
            }
#endif
            return                  TheObj;
            }

        #endregion
        #region================================================||   DataTypeConvert -- 数据类型转换      ||<FourNode>================================================
                                                                                                                            /// <@ TypeTans-- 普通数据类型转换 >
        public static               T PhaseTo <T>       ( object inObj )                                                    // 阶段 < 友盟 >     
        {
            T                       TheResult                       = default(T);
            try
            {
                TheResult                                           = (T)inObj;
            }
            catch (Exception e)
            {
                Debug.LogError      ("Phase Failed! obj: " + inObj.ToString());
            }
            return TheResult;
        }
        public static int           ParseStrToInt       ( string inStr )                                                    // StrToInt         
        {
            int                     TheReInt                        = 0;                                                    /// 返回 Int
            if (inStr.Trim().Length < 1)                            TheReInt = 0;                                           /// 错误 :返回:0 

            if (int.TryParse(inStr,out TheReInt))                   return TheReInt;                                        /// 正常返回转换值
            else                                                    TheReInt = 0;                                           /// 错误 :返回:0                                    
            if ( TheReInt == 0 )                                    Debug.LogError("Str:" + inStr + "(转换Int 失败!)");      /// 错误信息
            return                  0;
        }
        public static long          ParseStrToLong      ( string inStr )                                                    // StrToLong        
        {
            long                    TheReInt                        = 0;                                                    /// 返回 Long
            if (inStr.Trim().Length < 1)                            TheReInt = 0;                                           /// 错误 :返回:0 

            if (long.TryParse(inStr,out TheReInt))                  return TheReInt;                                        /// 正常返回转换值
            else                                                    TheReInt = 0;                                           /// 错误 :返回:0                                    
            if ( TheReInt == 0 )                                    Debug.LogError("Str:" + inStr + "(转换Long 失败!)");     /// 错误信息
            return                  0;
        }
        public static float         ParseStrToFloat     ( string inStr )                                                    // StrToInt         
        {
            float                   TheRefloat                      = 0;                                                    /// 返回 Int
            if (inStr.Trim().Length < 1)                            TheRefloat = 0;                                         /// 错误 :返回:0 

            if (float.TryParse(inStr,out TheRefloat))               return TheRefloat;                                      /// 正常返回转换值
            else                                                    TheRefloat = 0;                                         /// 错误 :返回:0                                    
            if ( TheRefloat == 0 )                                  Debug.LogError("Str:" + inStr + "(转换Float 失败!)");    /// 错误信息
            return                  0;
        }
        public static bool          ParseStrToBool      ( string inStr )                                                    // StrToLong        
        {
            if      ( inStr == "1")                                 return true  ;
            else if ( inStr == "0")                                 return false ;
            else                                                    Debug.LogError("Str:" + inStr + "(转换Bool 失败!)");
            return                                                  false;
        }
        public static DateTime      ParseToDateTime     ( string inStr )                                                    // StrToDataTime    
        {
            DateTime                TheDataTime                     = DateTime.MinValue;
            if ( inStr.Trim().Length < 1 )                          return TheDataTime;
            if ( DateTime.TryParse ( inStr, out TheDataTime))       return TheDataTime;
            else                    Debug.LogError("Str:" + inStr + "(转换DateTime 失败!)");
            return                  TheDataTime;
        }

                                                                                                                            /// <@ JsonTans-- JSon数据转换 >  
        public static int           JsonKeyToInt        ( JsonObject inJsonObj, string inKey)                               // JSonObj To Int   
        {
            if ( inJsonObj.ContainsKey(inKey) && inJsonObj[inKey] != null )
            {   return              ParseStrToInt( inJsonObj[inKey].ToString() );                    }
            else                    Debug.LogError("Json:" + inJsonObj + "<Key> 值:" + inKey + "( 不存在或等于null)" );
            return                  0;
        }
        public static long          JsonKeyToLong       ( JsonObject inJsonObj, string inKey)                               // JSonObj To Long  
        {
            if ( inJsonObj.ContainsKey(inKey) && inJsonObj[inKey] != null )
            {   return              ParseStrToLong  ( inJsonObj[inKey].ToString());                  }
            else                    Debug.LogError  ( "Json:" + inJsonObj + "<Key> 值:" + inKey + "( 不存在或等于null)");
            return                  0;
        }
        public static float         JsonKeyToFloat      ( JsonObject inJsonObj, string inKey)                               // JSonObj To Float 
        {
            if ( inJsonObj.ContainsKey(inKey) && inJsonObj[inKey] != null )
            {   return              ParseStrToFloat ( inJsonObj[inKey].ToString());                  }
            else                    Debug.LogError  ( "Json:" + inJsonObj + "<Key> 值:" + inKey + "( 不存在或等于null)");
            return                  0;
        }
        public static bool          JsonKeyToBool       ( JsonObject inJsonObj, string inKey)                               // JSonObj To Bool  
        {
            if ( inJsonObj.ContainsKey(inKey) && inJsonObj[inKey] != null )
            {   return              ParseStrToBool  ( inJsonObj[inKey].ToString());                  }
            else                    Debug.LogError  ("Json:" + inJsonObj + "<Key> 值:" + inKey + "( 不存在或等于null)");
            return                  false;
        }

        #endregion
    }
}
