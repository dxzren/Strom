using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 攻击目标 </summary>
    public class AttackTarget : MonoBehaviour
    {
        public static List<IBattleMemMediator>      GetAttackTargetList( IBattleMemMediator inAttacker, string inActRangeType )     // 获取攻击目标数据对象列表  
        {
            AttackTarRanageType     TheRanageT                      = AttackRanageStrToType(inActRangeType);
            BattleTeam              AttackTeam                      = GetAttackTeam(inAttacker);
            BattleTeam              DefendTeam                      = GetDefendTeam(inAttacker);

            switch (TheRanageT)
            {
                case AttackTarRanageType.DefendSingle:              return new List<IBattleMemMediator>() { DefendTeam.GetAttackRangeSingle(inAttacker) };  /// 守方单体
                case AttackTarRanageType.DefendFrontRow:            return DefendTeam.GetMemListByRow(DefendTeam.GetMostFrontRow());                        /// 守方前排
                case AttackTarRanageType.DefendBackRow:             return DefendTeam.GetMemListByRow(DefendTeam.GetMostBackRow());                         /// 守方后排
                case AttackTarRanageType.DefendFrontRow_MidRow:                                                                                             //  守方前两排    
                    {
                        X_Axis          TheRow                      = DefendTeam.GetMostFrontRow();

                        if (inAttacker.Camp == Battle_Camp.Our)                                                                                             // 我方   
                        {
                            if (TheRow != X_Axis.X_03 ) return DefendTeam.GetAliveMemList();                                            /// 最前排非固定前排_返回全体目标
                            else                                                                                                        //  返回前中排成员列表    
                            {
                                List<X_Axis> TheRowList             = new List<X_Axis>();
                                TheRowList.Add(X_Axis.X_03);
                                TheRowList.Add(X_Axis.X_02);
                                return DefendTeam.GetMemListByRowList(TheRowList);
                            }
                        }
                        else                                                                                                                                // 敌方   
                        {
                            if (TheRow != X_Axis.X_04) return DefendTeam.GetAliveMemList();                                            /// 最前排非固定前排_返回全体目标
                            else
                            {
                                List<X_Axis> TheRowList             = new List<X_Axis>();
                                TheRowList.Add(X_Axis.X_04);
                                TheRowList.Add(X_Axis.X_05);
                                return DefendTeam.GetMemListByRowList(TheRowList);
                            }
                        }
                    }
                case AttackTarRanageType.DefnedBackRow_MidRow:                                                                                              //  守方后两排    
                    {
                        X_Axis          TheRow                      = DefendTeam.GetMostFrontRow();

                        if (inAttacker.Camp == Battle_Camp.Our)                                                                                             // 我方   
                        {
                            if (TheRow != X_Axis.X_03 ) return DefendTeam.GetAliveMemList();                                            /// 最前排非固定前排_返回全体目标
                            else                                                                                                        //  返回前中排成员列表    
                            {
                                List<X_Axis> TheRowList             = new List<X_Axis>();
                                TheRowList.Add(X_Axis.X_01);
                                TheRowList.Add(X_Axis.X_02);
                                return DefendTeam.GetMemListByRowList(TheRowList);
                            }
                        }
                        else                                                                                                                                // 敌方   
                        {
                            if (TheRow != X_Axis.X_04) return DefendTeam.GetAliveMemList();                                            /// 最前排非固定前排_返回全体目标
                            else
                            {
                                List<X_Axis> TheRowList             = new List<X_Axis>();
                                TheRowList.Add(X_Axis.X_05);
                                TheRowList.Add(X_Axis.X_06);
                                return DefendTeam.GetMemListByRowList(TheRowList);
                            }
                        }
                    }
                case AttackTarRanageType.DefendRandomSingle:        return new List<IBattleMemMediator>() { DefendTeam.GetRandomMem()};                     //  守方随机一人
                case AttackTarRanageType.DefendRandomDouble:                                                                                                //  守方随机二人  
                    {
                        if (DefendTeam.GetAliveMemCount() <= 2)     return DefendTeam.GetAliveMemList();                                                    ///  等于小于2 返回全体
                        List<IBattleMemMediator>    TheMemList      = new List<IBattleMemMediator>();
                        IBattleMemMediator          TempMem         = DefendTeam.GetRandomMem();
                        TheMemList.Add(TempMem);
                        for (int i = 0; i < 2;)
                        {
                            if ( TempMem != DefendTeam.GetRandomMem())
                            {
                                TheMemList.Add(TempMem);
                                i++;
                            }
                        }
                        return                                      TheMemList;
                    }
                case AttackTarRanageType.DefendRandomThird:                                                                                                 //  守方随机三人  
                    {
                        if (DefendTeam.GetAliveMemCount() <= 3)     return DefendTeam.GetAliveMemList();                                                    /// 等于小于3 返回全体
                        List<IBattleMemMediator>    TheMemList      = new List<IBattleMemMediator>();
                        IBattleMemMediator          TempMem         = DefendTeam.GetRandomMem();
                        TheMemList.Add(TempMem);
                        for (int i = 0; i < 3;)
                        {
                            if (TempMem != DefendTeam.GetRandomMem())
                            {
                                TheMemList.Add(TempMem);
                                i++;
                            }
                        }
                        return                                      TheMemList;
                    }
                case AttackTarRanageType.DefendTeam:                return DefendTeam.GetAliveMemList();                                                    /// 守方全体
                case AttackTarRanageType.DefendHpMin:               return new List<IBattleMemMediator>() { DefendTeam.GetHpRateLeast() };                  /// 守方血量最低
                case AttackTarRanageType.DefendHpMax:               return new List<IBattleMemMediator>() { DefendTeam.GetHpRateMost() };                   /// 守方血量最高
                case AttackTarRanageType.DefendJumpThree:                                                                                                   //  弹射3次        
                    {
                        List<IBattleMemMediator>    TheMemList      = new List<IBattleMemMediator>();                                                       /// 返回实例     
                        List<IBattleMemMediator>    AliveMemList    = new List<IBattleMemMediator>();                                                       /// 可攻击成员目标
                        IBattleMemMediator          TheMem          = DefendTeam.GetAttackRangeSingle(inAttacker);                                          /// 弹射初始目标
                        int                         RealCount       = 3;

                        AliveMemList                                = DefendTeam.GetAliveMemList();                                                         /// 获取正常目标成员列表
                        TheMemList.Add(TheMem);                                                                                                             /// 添加起始目标
                        
                        if( DefendTeam.GetAliveMemCount() < 3)      RealCount = AliveMemList.Count;                                                         /// 目标数量<小于3>
                        for(int i = 0; i < RealCount; i++)                                                                                                  //  依次添加邻近目标  
                        {
                            TheMem                                  = DefendTeam.GetNearMem(TheMem, AliveMemList);
                            TheMemList.Add                          (TheMem);
                            AliveMemList.Remove                     (TheMem);
                        }
                        AliveMemList                                = null;
                        return                                      TheMemList;                                                               
                    }
                case AttackTarRanageType.DefendJumpFour:                                                                                                    //  弹射4次        
                    {
                        List<IBattleMemMediator>    TheMemList      = new List<IBattleMemMediator>();                                                       /// 返回实例     
                        List<IBattleMemMediator>    AliveMemList    = new List<IBattleMemMediator>();                                                       /// 可攻击成员目标
                        IBattleMemMediator          TheMem          = DefendTeam.GetAttackRangeSingle(inAttacker);                                          /// 弹射初始目标
                        int                         RealCount       = 4;                                                                                    /// 弹射次数

                        AliveMemList                                = DefendTeam.GetAliveMemList();                                                         /// 获取正常目标成员列表
                        TheMemList.Add(TheMem);                                                                                                             /// 添加起始目标
                        
                        if( DefendTeam.GetAliveMemCount() < 4)      RealCount = AliveMemList.Count;                                                         /// 目标数量<小于3>
                        for(int i = 0; i < RealCount; i++)                                                                                                  //  依次添加邻近目标  
                        {
                            TheMem                                  = DefendTeam.GetNearMem(TheMem, AliveMemList);
                            TheMemList.Add                          (TheMem);
                            AliveMemList.Remove                     (TheMem);
                        }
                        AliveMemList                                = null;
                        return                                      TheMemList;                                                               
                    }
                case AttackTarRanageType.DefendAround:              return DefendTeam.GetAroundMemList(DefendTeam.GetAttackRangeSingle(inAttacker));        //  周围的目标(不含本体)

                case AttackTarRanageType.Owner:                     return new List<IBattleMemMediator>() { inAttacker };                                   // (攻方) 本体
                case AttackTarRanageType.AttackHpMin:               return new List<IBattleMemMediator>() { AttackTeam.GetHpRateLeast() };                  // (攻方) 血量最低
                case AttackTarRanageType.AttackTeam:                return AttackTeam.GetAliveMemList();                                                    // (攻方) 全体
                default:
                    Debug.LogError("AttackTarRanageType: None!___"+"未找到的攻击范围目标");
                    return new List<IBattleMemMediator>() { AttackTeam.GetAttackRangeSingle(inAttacker) };
            }
        }
        public static  AttackTarRanageType          AttackRanageStrToType  (string inAttackRanage)                                  // 攻击范围StrToTpye       
        {
            switch( inAttackRanage )
            {
                case "Defencer":                                    return AttackTarRanageType.DefendSingle;                /// 守方单体
                case "DefenceFrontRows":                            return AttackTarRanageType.DefendFrontRow;              /// 守方前排
                case "DefenceBackRows":                             return AttackTarRanageType.DefendBackRow;               /// 守方后排
                case "DefenceFrontTwoRows":                         return AttackTarRanageType.DefendFrontRow_MidRow;       /// 守方前两排
                case "DefenceBackTwoRows":                          return AttackTarRanageType.DefnedBackRow_MidRow;        /// 守方后两排

                case "DefenceRandomOne":                            return AttackTarRanageType.DefendRandomSingle;          /// 守方随机一人
                case "DefenceRandomTwo":                            return AttackTarRanageType.DefendRandomDouble;          /// 守方随机二人
                case "DefenceRandomThree":                          return AttackTarRanageType.DefendRandomThird;           /// 守方随机三人
                case "DefenceTeam":                                 return AttackTarRanageType.DefendTeam;                  /// 守方全体

                case "DefenceHpRateMin":                            return AttackTarRanageType.DefendHpMin;                 /// 守方血量最低
                case "DefenceHpRateMax":                            return AttackTarRanageType.DefendHpMax;                 /// 守方血量最高
                case "DefenceJumpThree":                            return AttackTarRanageType.DefendJumpThree;             /// 守方弹射三次
                case "DefenceCatapultFour":                         return AttackTarRanageType.DefendJumpFour;              /// 守方弹射四次
                case "DefenceNeighbour":                            return AttackTarRanageType.DefendAround;                /// 守方周围

                case "Attacker":                                    return AttackTarRanageType.Owner;                       /// (攻方)本体
                case "AttackHpRateMin":                             return AttackTarRanageType.AttackHpMin;                 /// (攻方)血量最低
                case "AttackTeam":                                  return AttackTarRanageType.AttackTeam;                  /// (攻方)全体
                default:
                    Debug.LogError("AttackRanageType: null(错误:未找到范围类型)");                                            /// 错误:未找到类型
                    return  AttackTarRanageType.Owner; 
            }
        }

        private static BattleTeam                   GetAttackTeam( IBattleMemMediator inAttacker)                                   // 获取攻方队伍数据         
        {
            return inAttacker.Camp == Battle_Camp.Our   ?           BattleControll.sInstance.OurTeam : BattleControll.sInstance.EnemyTeam;
        }
        private static BattleTeam                   GetDefendTeam( IBattleMemMediator inAttacker)                                   // 获取守方队伍数据         
        {
            return inAttacker.Camp == Battle_Camp.Enemy ?           BattleControll.sInstance.OurTeam : BattleControll.sInstance.EnemyTeam;
        }
       
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 战斗队伍 </summary>
    public class BattleTeam
    {
        public Battle_Camp              TeamCamp                        { get { return _TeamCamp; } }                       /// 阵营
        public List<IBattleMemMediator> TeamList                        { get { return _TeamList; } }                       /// 成员列表



        public                          BattleTeam (Battle_Camp inCamp)                                                     // 构造函数 <阵营>           
        {
            _TeamCamp                   = inCamp;
        }
        public                          BattleTeam (List<IBattleMemMediator> inTeam, Battle_Camp inCamp)                    // 构造函数 <成员列表> <阵营> 
        {
            _TeamList                   = inTeam;
            _TeamCamp                   = inCamp;
        }

                                                                                                                            /// <@ 工具函数  @>
        public int                      GetAliveMemCount    ()                                                              // 获取正常状态目标数量        
        {
            int                         TargetCount                     = 0;
            for ( int i = 0; i < TeamList.Count; i++ )
            {
                if (TeamList[i].MemState == BattleMemState.Normal)      TargetCount++;                                      /// 正常状态 ()
            }
            return                      TargetCount;
        }
        public List<IBattleMemMediator> GetAliveMemList     ()                                                              // 获取正常状态成员列表        
        {
            List<IBattleMemMediator>    TheMemList                      = new List<IBattleMemMediator>();
            foreach ( var Item in TeamList )
            {
                if ( Item.MemState == BattleMemState.Normal)            TheMemList.Add(Item);
            }
            return                      TheMemList;
        }
        public X_Axis                   GetMostFrontRow     ()                                                              // 获取最前排                
        {
            X_Axis                      TheX_Axis                       = X_Axis.None;
            if (TeamCamp == Battle_Camp.Our)                                                                                /// 我方最前排  
            {
                TheX_Axis                                               = X_Axis.X_01;
                foreach (var Item in TeamList)
                {
                    if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.XPos > TheX_Axis)                           /// 冒泡排序 取最大值为前排
                    {                   TheX_Axis                       = Item.MemPos_D.XPos;   }
                }
            }
            else                                                                                                            /// 敌方最前排  
            {
                TheX_Axis                                               = X_Axis.X_06;
                foreach (var Item in TeamList)
                {
                    if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.XPos < TheX_Axis)                           /// 冒泡排序 取最小值为前排
                    {                   TheX_Axis                       = Item.MemPos_D.XPos;   }
                }
            }
            return                      TheX_Axis;
        }
        public X_Axis                   GetMostBackRow      ()                                                              // 获取最后排                
        {
            X_Axis                      TheX_Axis                       = X_Axis.None;
            if (TeamCamp == Battle_Camp.Our)                                                                                /// 我方最后排  
            {
                TheX_Axis                                               = X_Axis.X_03;
                foreach (var Item in TeamList)
                {
                    if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.XPos < TheX_Axis)                           /// 冒泡排序 取最小值为后排
                    {                   TheX_Axis                       = Item.MemPos_D.XPos;   }
                }
            }
            else                                                                                                            /// 敌方最后排  
            {
                TheX_Axis                                               = X_Axis.X_04;
                foreach (var Item in TeamList)
                {
                    if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.XPos > TheX_Axis)                           /// 冒泡排序 取最小值为前排
                    {                   TheX_Axis                       = Item.MemPos_D.XPos;   }
                }
            }
            return                      TheX_Axis;
        }

                                                                                                                            /// <@ 调用函数  @>
        public IBattleMemMediator       GetAttackRangeSingle( IBattleMemMediator inAttacker)                                // 获取攻击范围内的单体成员   
        {
            int                         CompareValue                    = 10;                                               /// 绝对数比较值
            X_Axis                      TheRow                          = GetMostFrontRow();                                /// 守方最前排
            IBattleMemMediator          TheMem                          = null;                                             /// 成员数据
            List<IBattleMemMediator>    TheTeamList                     = GetMemListByRow(TheRow);                          /// 守方前排成员列表

            if (TeamCamp == Battle_Camp.Our)                                                                                /// 我方  
            {
                if ((int)inAttacker.MemPos_D.XPos + inAttacker.AttackRange >= (int)TheRow)                                  // 攻击范围内取绝对值小的成员
                {
                    foreach( var Item in TheTeamList )
                    {
                        if ( TheMem == null) TheMem                          = Item;

                        if (Mathf.Abs(Item.MemPos_D.ZPos - inAttacker.MemPos_D.ZPos) < CompareValue)
                        {
                            TheMem          = Item;
                            CompareValue    = Mathf.Abs(Item.MemPos_D.ZPos - inAttacker.MemPos_D.ZPos);
                        }
                    }
                    return                  TheMem;
                }
            }
            else                                                                                                            /// 敌方  
            {
                if ((int)inAttacker.MemPos_D.XPos - inAttacker.AttackRange >= (int)TheRow)                                  // 攻击范围内取绝对值小的成员
                {
                    foreach( var Item in TheTeamList )
                    {
                        if ( TheMem == null) TheMem                          = Item;

                        if (Mathf.Abs(Item.MemPos_D.ZPos - inAttacker.MemPos_D.ZPos) < CompareValue)
                        {
                            TheMem          = Item;
                            CompareValue    = Mathf.Abs(Item.MemPos_D.ZPos - inAttacker.MemPos_D.ZPos);
                        }
                    }
                    return                  TheMem;
                }
            }
            return null;
        }
        public IBattleMemMediator       GetHpRateLeast      ()                                                              // 获取血量比例最低的成员     
        {
            IBattleMemMediator          HpLeast                         = null;
            foreach ( var Item in GetAliveMemList())                                                                        // 冒泡排序 
            {
                if      (HpLeast == null)                                                   HpLeast = Item;
                else if (HpLeast.Hp * 1.0 / HpLeast.MaxHp > Item.Hp * 1.0 / Item.MaxHp)     HpLeast = Item;
            }
            return                      HpLeast;
        }
        public IBattleMemMediator       GetHpRateMost       ()                                                              // 获取血量比例最高的成员     
        {
            IBattleMemMediator          TheMem                      = null;
            foreach( var Item in TeamList )
            {
                if (TheMem == null )    TheMem                      = Item;
                else
                {
                    if (TheMem.Hp * 1.0f / TheMem.MaxHp < Item.Hp * 1.0f / Item.MaxHp) TheMem = Item;
                }
            }
            return                      TheMem;
            
        }
        public IBattleMemMediator       GetRandomMem        ()                                                              // 获取一个随机成员          
        {
            int                         RankValue           = UnityEngine.Random.Range(0, GetAliveMemCount()-1 );
            return                      GetAliveMemList()[RankValue];
        }

        public IBattleMemMediator       GetNearMem(IBattleMemMediator inAttacker, List<IBattleMemMediator> inAliveMemList)  // 获取目标周围一个成员<连锁闪电>
        {
            foreach (var Item in inAliveMemList)
            {
                if (Vector3.Distance(inAttacker.MemPos_D.NowPosV3, Item.MemPos_D.NowPosV3) == 1)                          /// 返回邻近目标
                    return              Item;
            }
            return                      inAliveMemList[0];                                                                  /// 周围没有目标,列表目标头部目标
        }
        public IBattleMemMediator       GetRandomAliveMem   ()                                                              // 随机获取一个成活队员(没有则返NUll)
        {
            foreach (var Item in _TeamList)
            {
                if (Item.MemState == BattleMemState.Normal) return Item;
            }
            return null;
        }
        public List<IBattleMemMediator> GetMemListBy        ( Polarity inPolarity)                                          // 元素属性获取成员列表       
        {
            List<IBattleMemMediator>    TheMemList = new List<IBattleMemMediator>();
            foreach ( var Item in GetAliveMemList())
            {
                if (Item.IMemData.MemberPolarity == (int)inPolarity)    TheMemList.Add(Item);
            }
            return                      TheMemList;
        }
        public List<IBattleMemMediator> GetAroundMemList    ( IBattleMemMediator inTarget)                                  // 获取目标邻近的成员列表     
        {
            List<IBattleMemMediator>    TheMemList                      = new List<IBattleMemMediator>();
            foreach ( var Item in GetAliveMemList())
            {
                if ( Item.MemPos_D.FixedPosNum != inTarget.MemPos_D.FixedPosNum && Vector3.Distance(inTarget.MemPos_D.NowPosV3, Item.MemPos_D.NowPosV3) <= BattleParmConfig.NeighbourRange)
                {    TheMemList.Add(Item);   }
            }
            return                      TheMemList;
        }

        public List<IBattleMemMediator> GetMemListByRow  ( X_Axis inRow)                                                    // 根据竖排位置_获取成员列表  
        {
            List<X_Axis>                TheRowList                   = new List<X_Axis>();
            TheRowList.Add(inRow);
            return                      GetMemListByRowList(TheRowList);
        }
        public List<IBattleMemMediator> GetMemListByRowList (List<X_Axis> inRowList)                                        // 根据竖排列表_获取成员列表  
        {
            List<IBattleMemMediator>    TheMemList                      = new List<IBattleMemMediator>();

            for(int i = 0; i < inRowList.Count;i++)
            {
                foreach (var Item in TeamList)
                {
                    if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.XPos == inRowList[i])
                    {
                            TheMemList.Add(Item);
                    }
                }
            }
            return TheMemList;
        }



        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private List<IBattleMemMediator>_TeamList                       = new List<IBattleMemMediator>();                   /// 队伍实例
        private Battle_Camp             _TeamCamp                       = Battle_Camp.Our;                                  /// 阵营实例
        #endregion
    }
}
