using UnityEngine;
using System;
using System.Collections;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------// <summary> 移动目标 </summary>
    public interface IMoveTarget
    {
        bool                        IsCancel { get; }                                                                       /// 是否已取消

        float                       GetTargetBodySize();                                                                    // 获取目标尺寸
        Vector3                     GetTargetPos();                                                                         // 获取目标位置坐标
        void                        RemoveTarget (IBattleMemMediator inTarget = null);                                      // 移除目标(null 直接移除, 非null 判定参数)
    }
    ///---------------------------------------------------------------------------------------------------------------------// <summary> 移动到成员目标 </summary>
    public class MoveToMember : IMoveTarget
    {
        public IBattleMemMediator   TargetMem                       = null;                                                 /// 目标成员
        public                      MoveToMember (IBattleMemMediator inTarget)                                              // 构造函数        
        {   TargetMem               = inTarget; }

        public bool                 IsCancel                                                                                // 是否取消
        {   get { return TargetMem == null || TargetMem.MemState != BattleMemState.Normal; }            }    
        public float                GetTargetBodySize ()                                                                    // 获取目标模型比例 
        {
            if (TargetMem != null )                                 return TargetMem.BodySize;
            return                  0;
        }
        public Vector3              GetTargetPos()                                                                          // 获取目标坐标     
        {
            if (!IsCancel)          return TargetMem.MemPos_D.NowPosV3;                                                     /// 重新定位
            return                  Vector3.zero;                                                                           /// 坐标归零
        }

        public void                 RemoveTarget (IBattleMemMediator inTarget)                                              // 移除目标         
        {
            if      (inTarget == null || TargetMem == null)           TargetMem = null;
            else if (TargetMem == inTarget)                           TargetMem = null;
        }


    }

}