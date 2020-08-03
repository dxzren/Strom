using UnityEngine;
using System.Collections;
using LinqTools;
using SimpleJson;
using System.Collections.Generic;

namespace StormBattle
{
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 战斗回放 </summary>
    public class BattleReport
    {
        public bool                     IsShowHP                        = false;                                            /// 是否血量设置
        public bool                     IsReplay                        = false;                                            /// 是否回放
        public bool                     IsNeedRecord                    = false;                                            /// 是否记录
        public bool                     IsAutoBattle                    = true;                                             /// 是否自动战斗

        public bool                     IsMemComplete                   = false;                                            /// 成员行为完成
        public bool                     IsRandEventComplete             = false;                                            /// 随机事件完成
        public bool                     IsError                         = false;                                            /// 错误

        public static BattleReport      sInstance                       { get { return _Instance; } }                       /// 实例
        private                         BattleReport()      { }                                                             /// 构造函数


        public bool                     GetRandEvent        ( BattleRandomType   inRandType, int inRateValue,               //  返回随机结果,并记录事件信息
                                                              IBattleMemMediator inAttacker, IBattleMemMediator inDefender = null)
        {
            int                         DefendPosNum                    = 0;
            bool                        IsAction                        = false;

            if ( inDefender != null )   DefendPosNum                    = (int)inAttacker.MemPos_D.FixedPosNum;             /// 固定位置

            if (BattleReport.sInstance.IsReplay)                                                                            // 回放
            {
                BattleRandomEvent       TheRandEvent                    =  _RandEventList.Find( P => 
                                        P.Process                       == BattleControll.sInstance.EnemyProgress &&        /// 战斗进度
                                        P.AttackCount                   == inAttacker.AttackCount &&                        /// 攻击累计
                                        P.UltCount                      == inAttacker.UltSkillCount &&                      /// 大招累计
                                        P.AttackPosNum                  == (int)inAttacker.MemPos_D.FixedPosNum &&                        /// 攻击方位置号码
                                        P.DefendPosNum                  == DefendPosNum &&                                  /// 防御方位置号码
                                        P.RandType                      == inRandType);                                     /// 随机事件类型

                IsAction                                                = (TheRandEvent != null);                           /// 返回 - 随机结果失败 false 不记录信息
            }
            else                                                                                                            // 非回放
            {
                switch ( inRandType)
                {
                    case BattleRandomType.Dodge:        IsAction        = (Random.Range(0,100)   < inRateValue);    break;  /// 闪避
                    case BattleRandomType.MagicCrit:                                                                        /// 魔法暴击
                    case BattleRandomType.PhyCrit:      IsAction        = (Random.Range(0, 1000) < inRateValue);    break;  /// 物理暴击
                    case BattleRandomType.VenomTalent:                                                                      /// 毒灭
                    case BattleRandomType.ThunderTalent:                                                                    /// 雷灭
                    case BattleRandomType.FireTalent:   IsAction        = (Random.Range(0,100) < inRateValue);      break;  /// 火灭
                    case BattleRandomType.CameraVibrate:IsAction        = (Random.Range(0,100) < inRateValue);      break;  /// 震屏
                }

                if ( IsNeedRecord && IsAction )                                                                             // 记录回放数据 
                {
                    _RandEventList.Add   (new BattleRandomEvent()
                    {
                        Process                                         = BattleControll.sInstance.EnemyProgress,           /// 进度
                        AttackCount                                     = inAttacker.AttackCount,                           /// 攻击累计
                        UltCount                                        = inAttacker.UltSkillCount,                         /// 大招累计
                        AttackPosNum                                    = (int)inAttacker.MemPos_D.FixedPosNum,             /// 攻击方位置
                        DefendPosNum                                    = DefendPosNum,                                     /// 防守方位置

                        RandType                                        = inRandType,                                       /// 随机事件类型
                        AttackCamp                                      = inAttacker.Camp,                                  /// 攻击方阵营
                        RandResultList                                  = new List<int>(),                                  /// 命中多个目标列表
                    });
                }
            }
            return                      IsAction;                                                                           /// 返回
        }
        public List<int>                GetRandTargetList   ( BattleRandomType   inRandType, int inMaxValue, IBattleMemMediator inAttacker) // 获取随机命中目标列表 
        {
            List<int>                   TheTargetList                   = new List<int>();

            if (sInstance.IsReplay)
            {
                BattleRandomEvent       TheRandEvent                    = _RandEventList.Find(P => 
                                                                          P.Process         == BattleControll.sInstance.EnemyProgress &&
                                                                          P.AttackCount     == inAttacker.AttackCount &&
                                                                          P.UltCount        == inAttacker.UltSkillCount &&
                                                                          P.AttackPosNum    == (int)inAttacker.MemPos_D.FixedPosNum &&
                                                                          P.DefendPosNum    == 0 &&
                                                                          P.AttackCamp      == inAttacker.Camp);
                if (TheRandEvent != null)                               return TheRandEvent.RandResultList;                                                                        
            }
            else
            {
                switch(inRandType)
                {
                    case BattleRandomType.RanOnce:      TheTargetList   = GetRandTargList(0, inMaxValue, 1);    break;      /// 命中一个随机目标
                    case BattleRandomType.RanDouble:    TheTargetList   = GetRandTargList(0, inMaxValue, 2);    break;      /// 命中两个随机目标
                    case BattleRandomType.RanThird:     TheTargetList   = GetRandTargList(0, inMaxValue, 3);    break;      /// 命中三个随机目标
                }
            }
            if ( TheTargetList.Count > 0)
            {
                _RandEventList.Add(new BattleRandomEvent()
                {
                    Process                                             = BattleControll.sInstance.EnemyProgress,           /// 进度
                    AttackCount                                         = inAttacker.AttackCount,                           /// 攻击累计
                    UltCount                                            = inAttacker.UltSkillCount,                         /// 大招累计
                    AttackPosNum                                        = (int)inAttacker.MemPos_D.FixedPosNum,             /// 攻击方位置
                    DefendPosNum                                        = 0,                                                /// 防守方位置

                    RandType                                            = inRandType,                                       /// 随机事件类型
                    AttackCamp                                          = inAttacker.Camp,                                  /// 攻击方阵营
                    RandResultList                                      = TheTargetList,                                    /// 命中多个目标列表
                });
            }
            return                      TheTargetList;                                                                      /// 返回
        } 

        public void                     AddUltEvent         ( IBattleMemMediator inAttacker)                                //  添加大招事件      
        {
            if ( IsNeedRecord && !IsAutoBattle )
            {
                _UltEventList.Add(new UltSkillEvent()
                {
                    Process                                             = BattleControll.sInstance.EnemyProgress,
                    AttackerPosition                                    = (int)inAttacker.MemPos_D.FixedPosNum,
                    ActionTime                                          = Time.time - _BeginActionTime,
                    RealTime                                            = Time.realtimeSinceStartup - _BeginRealTime,
                });
            }
        }

        public void                     RestReportTimer()
        {
            _BeginActionTime            = UnityEngine.Time.time;
            _BeginRealTime              = UnityEngine.Time.realtimeSinceStartup;
            _LastActionTime             = 0;
            _LastRealTime               = 0;
            if (IsReplay && !IsAutoBattle)
            {
                BattleControll.sInstance.TheMono.StopCoroutine(FireUltEventList());
                BattleControll.sInstance.TheMono.StartCoroutine(FireUltEventList());
            }
        }

        public void                     Clear()
        {
            IsShowHP                        = false;                                            /// 是否血量设置
            IsReplay                        = false;                                            /// 是否回放
            IsNeedRecord                    = false;                                            /// 是否记录
            IsAutoBattle                    = true;                                             /// 是否自动战斗
            IsMemComplete                   = false;                                            /// 成员行为完成
            IsRandEventComplete             = false;                                            /// 随机事件完成
            IsError                         = false;                                            /// 错误
        }
    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private float                   _BeginActionTime                = 0;                                                ///  行动时间
        private float                   _BeginRealTime                  = 0;                                                ///  真实时间 
        private float                   _LastActionTime                 = 0;
        private float                   _LastRealTime                   = 0;

        private static BattleReport     _Instance                       = new BattleReport();                               /// 实例
        private List<UltSkillEvent>     _UltEventList                   = new List<UltSkillEvent>();                        /// 大招事件数据列表
        private List<BattleRandomEvent> _RandEventList                  = new List<BattleRandomEvent>();                    /// 随机事件列表

        IEnumerator                     FireUltEventList()                                                                  //
        {
            List<UltSkillEvent>         TheUltList                      = new List<UltSkillEvent>();
            TheUltList                  = _UltEventList.FindAll(P => P.Process == BattleControll.sInstance.EnemyProgress).OrderBy(P => P.RealTime).ToList();  
            
            foreach (var Item in TheUltList)
            {
                float TheTime           = Item.ActionTime - _LastActionTime;
                if (TheTime > 0)
                {
                    yield return new UnityEngine.WaitForSeconds(TheTime);
                    _LastActionTime     = Item.ActionTime;
                    _LastRealTime       = Item.RealTime;
                    BattleControll.sInstance.TheMono.StartCoroutine(FireUltEvent(Item));
                }
            }     
        }
        IEnumerator                     FireUltEvent        (UltSkillEvent inEvt)                                           //
        {
            yield return null;
            IBattleMemMediator          IMemMediator;
            IMemMediator                = BattleControll.sInstance.OurTeam.TeamList[inEvt.AttackerPosition];
            if (IMemMediator != null)   IMemMediator.IMemUI.OnClicked(IMemMediator.IMemUI);
        }
        private List<int>               GetRandTargList     (int inMin, int inMax, int inAmount)                            //  执行随机目标结果 列表 
        {
            List<int>                   TheRandList                     = new List<int>();
            if ( inMax - inMin <= inAmount)                                                                                 /// 目中单位数 大于 目标数量上限,添加所有目标 
            {
                for ( int i = 0; i < inMax - inMin; i++ )               TheRandList.Add(i);
            }
            else                                                                                                            /// 添加随机命中目标 到列表    
            {
                for ( int i = 0; i < inAmount;) 
                {
                    int                 RandValue                       = UnityEngine.Random.Range(inMin, inMax);
                    if (!TheRandList.Contains(RandValue))
                    {
                        TheRandList.Add(RandValue);
                        i++;
                    }
                }
            }
            return                      TheRandList;
        }

        private List<UltSkillEvent>     CurrUltEventList()                                                                  //  获取大招事件数据列表   
        {
            return _UltEventList.FindAll ( P => P.Process == BattleControll.sInstance.EnemyProgress).OrderBy( P => P.RealTime).ToList();
        }

        #endregion
    }
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 战斗随机事件数据 </summary>
    public class BattleRandomEvent                                                                                          
    {

        public int                      Process                         = 0;                                                /// 进度
        public int                      AttackCount                     = 0;                                                /// 攻击累计
        public int                      UltCount                        = 0;                                                /// 大招累计

        public int                      AttackPosNum                    = 0;                                                /// 攻击方位置
        public int                      DefendPosNum                    = 0;                                                /// 防守方位置

        public BattleRandomType         RandType                        = BattleRandomType.Dodge;                           /// 随机事件类型
        public Battle_Camp              AttackCamp                      = Battle_Camp.Our;                                  /// 攻击方阵营

        public List<int>                RandResultList                  = new List<int>();                                  /// 命中多个目标 列表

        public string                   ToSplitStr()                                                                        // RandEvent To Str   
        {
            return                      Process   + "," + AttackCount   + "," + UltCount        + ","  + AttackPosNum + "," + 
                                        DefendPosNum + "," + (int)RandType + "," + (int)AttackCamp + ","  + ResultListToStr();
        }
        public JsonObject               RandEventToJson()                                                                   // RandEvent To Json  
        {
            JsonObject                  TheJsonObj                      = new JsonObject();
            TheJsonObj.Add              ("AttackCount", AttackCount);                                                       /// 攻击累计
            TheJsonObj.Add              ("AttackPosNum",AttackPosNum);                                                      /// 攻击方位置
            TheJsonObj.Add              ("AttackCamp",  (int)AttackCamp);                                                   /// 攻击方阵营

            TheJsonObj.Add              ("DefendPosNum",DefendPosNum);                                                      /// 防守方位置
            TheJsonObj.Add              ("RandType",    (int)RandType);                                                     /// 随机事件类型
            TheJsonObj.Add              ("Process",     Process);                                                           /// 进度
            TheJsonObj.Add              ("UltCount",    UltCount);                                                          /// 大招累计

            JsonArray                   TheJsonArr                      = new JsonArray();                                  /// 命中目标列表 To Json数组
            RandResultList.ForEach      ( P => TheJsonArr.Add(P) );                                                         
            if ( TheJsonArr.Count > 0 ) TheJsonObj.Add("ResultArr",TheJsonArr);                                             /// Json数组 添加入JsonObj
                
            return                      TheJsonObj;                                                                         /// 返回
        }
        public static BattleRandomEvent JsonToRandEvent ( JsonObject inJsonObj)                                             // Json To RandEvent  
        {
            BattleRandomEvent           TheRandEvent                    = new BattleRandomEvent();
            TheRandEvent.AttackCount    = BattleUtil.JsonKeyToInt(inJsonObj,"AttackCount");                                 /// 攻击累计
            TheRandEvent.AttackPosNum   = BattleUtil.JsonKeyToInt(inJsonObj, "AttackPosNum");                               /// 攻击方位置
            TheRandEvent.AttackCamp     = (Battle_Camp)BattleUtil.JsonKeyToInt(inJsonObj, "AttackCamp");                    /// 攻击方阵营

            TheRandEvent.DefendPosNum   = BattleUtil.JsonKeyToInt(inJsonObj, "DefendPosNum");                               /// 防守方位置
            TheRandEvent.RandType       = (BattleRandomType)BattleUtil.JsonKeyToInt(inJsonObj, "RandType");                 /// 随机事件类型
            TheRandEvent.Process        = BattleUtil.JsonKeyToInt(inJsonObj, "Process");                                    /// 进度
            TheRandEvent.UltCount       = BattleUtil.JsonKeyToInt(inJsonObj, "UltCount");                                   /// 大招累计

            JsonArray                   TheJosnArr                      = inJsonObj["ResultArr"] as JsonArray;              /// Json数组 To ResultList
            for ( int i = 0; i < TheJosnArr.Count; i++)
            {   TheRandEvent.RandResultList.Add( BattleUtil.ParseStrToInt( TheJosnArr[i].ToString()) );     }

            return                      TheRandEvent;                                                                       /// 返回
        }

        public static BattleRandomEvent StrToRandEvent  ( string inSplitStr)                                                // Str To RandEvent   
        {
            BattleRandomEvent           TheRandEvent                    = new BattleRandomEvent();
            string[]                    TheSplistArr                    = inSplitStr.TrimEnd(',').Split(',');
            if( TheSplistArr.Length < 7 )
            {   Debug.LogError("回放数据包错误:" + inSplitStr);          return null; }

            TheRandEvent.Process        = int.Parse(TheSplistArr[0]);                                                       /// 进度
            TheRandEvent.AttackCount    = int.Parse(TheSplistArr[1]);                                                       /// 攻击累计
            TheRandEvent.UltCount       = int.Parse(TheSplistArr[2]);                                                       /// 大招累计
            TheRandEvent.AttackPosNum   = int.Parse(TheSplistArr[3]);                                                       /// 攻击方位置号码
            TheRandEvent.DefendPosNum   = int.Parse(TheSplistArr[4]);                                                       /// 防守方位置号码

            TheRandEvent.RandType       = (BattleRandomType)int.Parse(TheSplistArr[5]);                                     /// 随机事件类型
            TheRandEvent.AttackCamp     = (Battle_Camp)int.Parse(TheSplistArr[6]);                                          /// 攻击方阵营

            string[]                    TheStrList                      = TheSplistArr[7].Split('.');                       /// 命中目标列表
            foreach ( var Item in TheStrList )
            {
                TheRandEvent.RandResultList.Add(int.Parse(Item));
            }

            return                      TheRandEvent;                                                                       /// 返回
        }
        private string                  ResultListToStr()                                                                   // Int列表转成Str      
        {
            string                      TheStr                          = "";
            foreach(var Item in RandResultList)
            {
                TheStr                  += Item.ToString() + ".";
            }
            return                      TheStr.TrimEnd('.');
        }
    }
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 大招事件数据 </summary>
    public class UltSkillEvent                                                                                              
    {
        public int                      Process                         = 0;
        public int                      AttackerPosition                = 0;
        public float                    ActionTime                      = 0;
        public float                    RealTime                        = 0;
        public static UltSkillEvent     JosnToUltSkillEvent( JsonObject inJsonObj)                                          // JsonToThisData       
        {
            UltSkillEvent               TheUltSkillEvent                = new UltSkillEvent();

            TheUltSkillEvent.Process                                    = BattleUtil.JsonKeyToInt(inJsonObj, "Process");
            TheUltSkillEvent.AttackerPosition                           = BattleUtil.JsonKeyToInt(inJsonObj, "AttackerPosition");
            TheUltSkillEvent.ActionTime                                 = BattleUtil.JsonKeyToFloat(inJsonObj, "ActionTime");
            TheUltSkillEvent.RealTime                                   = BattleUtil.JsonKeyToFloat(inJsonObj, "RealTime");

            return                      TheUltSkillEvent;
        }
        public string                   ToSplitString()                                                                     // ThisData To Str      
        {   return Process + "," + ActionTime + "," + RealTime + "," + AttackerPosition;     }
        public JsonObject               ThisToJsonStr()                                                                     // ThisData To JsonObj  
        {
            JsonObject                  TheJsonObj                      = new JsonObject();
            TheJsonObj.Add("Process", Process);
            TheJsonObj.Add("ActionTime", ActionTime);
            TheJsonObj.Add("RealTime", RealTime);
            TheJsonObj.Add("AttackerPosition", AttackerPosition);

            return                      TheJsonObj;
        }

    }
    public enum BattleRandomType                                                                                            // 战斗随机类型        
    {
        Dodge                           = 0,                                                                                /// 闪避
        PhyCrit                         = 1,                                                                                /// 物理暴击
        MagicCrit                       = 2,                                                                                /// 魔法暴击
        ThunderTalent                   = 3,                                                                                /// 雷灭
        VenomTalent                     = 4,                                                                                /// 毒灭
        FireTalent                      = 5,                                                                                /// 燃灭
        RanOnce                         = 6,                                                                                /// 随机一个 目标
        RanDouble                       = 7,                                                                                /// 随机两个 目标
        RanThird                        = 8,                                                                                /// 随机三个 目标
        CameraVibrate                   = 9,                                                                                /// 震屏
    }
}
