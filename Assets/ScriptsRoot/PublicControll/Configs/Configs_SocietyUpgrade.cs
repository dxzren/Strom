/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公会升级表)客户端配置结构体
            /// </summary>
            public partial class Configs_SocietyUpgradeData 
             { 
                /// <summary>
                /// 公会等级--主键
                /// </summary>
                public int GuildsLevel{get;set;}

                
                /// <summary>
                /// 升级所需贡献
                /// </summary>
                public int MaxEXP { get;set; }
                /// <summary>
                /// 成员上限
                /// </summary>
                public int MemberNumber { get;set; }
                /// <summary>
                /// 活跃度上限
                /// </summary>
                public int LivenessNumber { get;set; }
                /// <summary>
                /// 免费刷新次数
                /// </summary>
                public int FreeNumber { get;set; }
                /// <summary>
                /// 副会长人数
                /// </summary>
                public int ViceChairmanNumber { get;set; }
                /// <summary>
                /// 会长设置副会长限制
                /// </summary>
                public int CSetVice { get;set; }
                /// <summary>
                /// 管理踢人每日限制
                /// </summary>
                public int FireNumber { get;set; }
                /// <summary>
                /// 捐献活跃度
                /// </summary>
                public List<int> Liveness { get;set; }
                /// <summary>
                /// 捐献贡献
                /// </summary>
                public List<int> Contribution { get;set; }
                /// <summary>
                /// 手动分配战利品限制次数
                /// </summary>
                public int Distribution { get;set; }
                /// <summary>
                /// 捐献礼包ID
                /// </summary>
                public List<int> GiftID { get;set; }
            } 
            /// <summary>
            /// (公会升级表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SocietyUpgrade
            { 

                static Configs_SocietyUpgrade _sInstance;
                public static Configs_SocietyUpgrade sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SocietyUpgrade();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公会升级表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SocietyUpgradeData> mSocietyUpgradeDatas
                {
                    get { return _SocietyUpgradeDatas; }
                }

                /// <summary>
                /// (公会升级表)字典集合
                /// </summary>
                Dictionary<int, Configs_SocietyUpgradeData> _SocietyUpgradeDatas = new Dictionary<int, Configs_SocietyUpgradeData>();

                /// <summary>
                /// 根据GuildsLevel读取对应的配置信息
                /// </summary>
                /// <param name="GuildsLevel">配置的GuildsLevel</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SocietyUpgradeData GetSocietyUpgradeDataByGuildsLevel(int GuildsLevel)
                {
                    if (_SocietyUpgradeDatas.ContainsKey(GuildsLevel))
                    {
                        return _SocietyUpgradeDatas[GuildsLevel];
                    }
                    return null;
                }

               /// <summary>
/// 初始化配置信息
/// </summary>
/// <param name="configData">配置文件内容</param>
 public void InitConfiguration(string configData) 
 {
  JsonObject data = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(configData);
 foreach (KeyValuePair<string, object> element in data) 
 { 
  JsonObject body = (JsonObject)element.Value;
  Configs_SocietyUpgradeData cd = new Configs_SocietyUpgradeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.GuildsLevel = key; 
  cd.MaxEXP =  Util.GetIntKeyValue(body,"MaxEXP"); 
  cd.MemberNumber =  Util.GetIntKeyValue(body,"MemberNumber"); 
  cd.LivenessNumber =  Util.GetIntKeyValue(body,"LivenessNumber"); 
  cd.FreeNumber =  Util.GetIntKeyValue(body,"FreeNumber"); 
  cd.ViceChairmanNumber =  Util.GetIntKeyValue(body,"ViceChairmanNumber"); 
  cd.CSetVice =  Util.GetIntKeyValue(body,"CSetVice"); 
  cd.FireNumber =  Util.GetIntKeyValue(body,"FireNumber"); 
 
 string[] LivenessStrs= Util.GetStringKeyValue(body, "Liveness").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Liveness = new List<int>();
foreach(string LivenessStr in LivenessStrs)  cd.Liveness.Add(Util.ParseToInt(LivenessStr)); 
 

 string[] ContributionStrs= Util.GetStringKeyValue(body, "Contribution").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Contribution = new List<int>();
foreach(string ContributionStr in ContributionStrs)  cd.Contribution.Add(Util.ParseToInt(ContributionStr)); 
 
 cd.Distribution =  Util.GetIntKeyValue(body,"Distribution"); 
 
 string[] GiftIDStrs= Util.GetStringKeyValue(body, "GiftID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.GiftID = new List<int>();
foreach(string GiftIDStr in GiftIDStrs)  cd.GiftID.Add(Util.ParseToInt(GiftIDStr)); 
 
 
 if (mSocietyUpgradeDatas.ContainsKey(key) == false)
 mSocietyUpgradeDatas.Add(key, cd);
  }
 //Debug.Log(mSocietyUpgradeDatas.Count);
}

            }