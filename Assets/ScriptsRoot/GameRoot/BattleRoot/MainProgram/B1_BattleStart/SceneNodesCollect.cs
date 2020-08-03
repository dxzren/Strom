using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StormBattle
{
    ///--------------------------------------------------------------------------------------------------------------------------------/// <summary> 战斗站位设置 集合 </summary>   
    public class SceneNodesCollect
    {
        public SceneNode                BirthNode                   = null;                                                             /// 出生节点
        public SceneNode                FirstWaveNode               = null;                                                             /// 第一波节点
        public SceneNode                SecondWaveNode              = null;                                                             /// 第二波节点
        public SceneNode                ThirdWaveNode               = null;                                                             /// 第三波节点
        public SceneNodesCollect()    { }                                                                                               // 构造函数
        public SceneNodesCollect        (string inSceneName)                                                                            // 构造函数
        {
            inSceneName                                             = inSceneName.Replace("3", "");

            Configs_BattleScenePosData  TheScenePos_D               = Configs_BattleScenePos.sInstance.GetBattleScenePosDataBySceneName(inSceneName);       // 场景位置数据
            if (TheScenePos_D == null)  { Debug.LogError            ("错误，未找到场景的位置信息配置：" + inSceneName);         return; }                      // 配置数据null 

            SetBirthNode                (TheScenePos_D);                                                                                                    // 出生节点设置                                                                              
            SetFirstWaveNode            (TheScenePos_D);                                                                                                    // 第一波节点设置
            SetSecondWaveNode           (TheScenePos_D);                                                                                                    // 第二波节点设置
            SetThirdWaveNode            (TheScenePos_D);                                                                                                    // 第三波节点设置
        }

        private void                    SetBirthNode (Configs_BattleScenePosData inScenePos_D)                                          // 设置 敌我成员 出生坐标顶点
        {
            BirthNode                   = new SceneNode();
            BirthNode.CameraPos         = ListToVector3(inScenePos_D.CameraPoint1);

            BirthNode.OurPosDic.Add     (1, ListToVector3( inScenePos_D.BornPoint1 ));
            BirthNode.OurPosDic.Add     (2, ListToVector3( inScenePos_D.BornPoint2 ));
            BirthNode.OurPosDic.Add     (3, ListToVector3( inScenePos_D.BornPoint3 ));
            BirthNode.OurPosDic.Add     (4, ListToVector3( inScenePos_D.BornPoint4 ));
            BirthNode.OurPosDic.Add     (5, ListToVector3( inScenePos_D.BornPoint5 ));
            BirthNode.OurPosDic.Add     (6, ListToVector3( inScenePos_D.BornPoint6 ));
            BirthNode.OurPosDic.Add     (7, ListToVector3( inScenePos_D.BornPoint7 ));
            BirthNode.OurPosDic.Add     (8, ListToVector3( inScenePos_D.BornPoint8 ));
            BirthNode.OurPosDic.Add     (9, ListToVector3( inScenePos_D.BornPoint9 ));

            BirthNode.EnemyPosDic.Add   (1, ListToVector3( inScenePos_D.NpcBornPoint1 ));
            BirthNode.EnemyPosDic.Add   (2, ListToVector3( inScenePos_D.NpcBornPoint2 ));
            BirthNode.EnemyPosDic.Add   (3, ListToVector3( inScenePos_D.NpcBornPoint3 ));
            BirthNode.EnemyPosDic.Add   (4, ListToVector3( inScenePos_D.NpcBornPoint4 ));
            BirthNode.EnemyPosDic.Add   (5, ListToVector3( inScenePos_D.NpcBornPoint5 ));
            BirthNode.EnemyPosDic.Add   (6, ListToVector3( inScenePos_D.NpcBornPoint6 ));
            BirthNode.EnemyPosDic.Add   (7, ListToVector3( inScenePos_D.NpcBornPoint7 ));
            BirthNode.EnemyPosDic.Add   (8, ListToVector3( inScenePos_D.NpcBornPoint8 ));
            BirthNode.EnemyPosDic.Add   (9, ListToVector3( inScenePos_D.NpcBornPoint9 ));
        }

        private void                    SetFirstWaveNode (Configs_BattleScenePosData inScenePos_D)                                      // 设置 敌我成员 第一波战斗初始坐标顶点
        {
            FirstWaveNode                   = new SceneNode();
            FirstWaveNode.CameraPos         = ListToVector3(inScenePos_D.CameraPoint2);

            FirstWaveNode.OurPosDic.Add     (1, ListToVector3( inScenePos_D.FristPoint1 ));
            FirstWaveNode.OurPosDic.Add     (2, ListToVector3( inScenePos_D.FristPoint2));
            FirstWaveNode.OurPosDic.Add     (3, ListToVector3( inScenePos_D.FristPoint3));
            FirstWaveNode.OurPosDic.Add     (4, ListToVector3( inScenePos_D.FristPoint4));
            FirstWaveNode.OurPosDic.Add     (5, ListToVector3( inScenePos_D.FristPoint5));
            FirstWaveNode.OurPosDic.Add     (6, ListToVector3( inScenePos_D.FristPoint6));
            FirstWaveNode.OurPosDic.Add     (7, ListToVector3( inScenePos_D.FristPoint7));
            FirstWaveNode.OurPosDic.Add     (8, ListToVector3( inScenePos_D.FristPoint8));
            FirstWaveNode.OurPosDic.Add     (9, ListToVector3( inScenePos_D.FristPoint9));

            FirstWaveNode.EnemyPosDic.Add   (1, ListToVector3( inScenePos_D.NpcFristPoint1));
            FirstWaveNode.EnemyPosDic.Add   (2, ListToVector3( inScenePos_D.NpcFristPoint2));
            FirstWaveNode.EnemyPosDic.Add   (3, ListToVector3( inScenePos_D.NpcFristPoint3));
            FirstWaveNode.EnemyPosDic.Add   (4, ListToVector3( inScenePos_D.NpcFristPoint4));
            FirstWaveNode.EnemyPosDic.Add   (5, ListToVector3( inScenePos_D.NpcFristPoint5));
            FirstWaveNode.EnemyPosDic.Add   (6, ListToVector3( inScenePos_D.NpcFristPoint6));
            FirstWaveNode.EnemyPosDic.Add   (7, ListToVector3( inScenePos_D.NpcFristPoint7));
            FirstWaveNode.EnemyPosDic.Add   (8, ListToVector3( inScenePos_D.NpcFristPoint8));
            FirstWaveNode.EnemyPosDic.Add   (9, ListToVector3( inScenePos_D.NpcFristPoint9));
        }
        private void                    SetSecondWaveNode (Configs_BattleScenePosData inScenePos_D)                                     // 设置 敌我成员 第二波战斗初始坐标顶点
        {
            SecondWaveNode                   = new SceneNode();
            SecondWaveNode.CameraPos         = ListToVector3(inScenePos_D.CameraPoint3);

            SecondWaveNode.OurPosDic.Add     (1, ListToVector3( inScenePos_D.SecondPoint1));
            SecondWaveNode.OurPosDic.Add     (2, ListToVector3( inScenePos_D.SecondPoint2));
            SecondWaveNode.OurPosDic.Add     (3, ListToVector3( inScenePos_D.SecondPoint3));
            SecondWaveNode.OurPosDic.Add     (4, ListToVector3( inScenePos_D.SecondPoint4));
            SecondWaveNode.OurPosDic.Add     (5, ListToVector3( inScenePos_D.SecondPoint5));
            SecondWaveNode.OurPosDic.Add     (6, ListToVector3( inScenePos_D.SecondPoint6));
            SecondWaveNode.OurPosDic.Add     (7, ListToVector3( inScenePos_D.SecondPoint7));
            SecondWaveNode.OurPosDic.Add     (8, ListToVector3( inScenePos_D.SecondPoint8));
            SecondWaveNode.OurPosDic.Add     (9, ListToVector3( inScenePos_D.SecondPoint9));

            SecondWaveNode.EnemyPosDic.Add   (1, ListToVector3( inScenePos_D.NpcSecondPoint1));
            SecondWaveNode.EnemyPosDic.Add   (2, ListToVector3( inScenePos_D.NpcSecondPoint2));
            SecondWaveNode.EnemyPosDic.Add   (3, ListToVector3( inScenePos_D.NpcSecondPoint3));
            SecondWaveNode.EnemyPosDic.Add   (4, ListToVector3( inScenePos_D.NpcSecondPoint4));
            SecondWaveNode.EnemyPosDic.Add   (5, ListToVector3( inScenePos_D.NpcSecondPoint5));
            SecondWaveNode.EnemyPosDic.Add   (6, ListToVector3( inScenePos_D.NpcSecondPoint6));
            SecondWaveNode.EnemyPosDic.Add   (7, ListToVector3( inScenePos_D.NpcSecondPoint7));
            SecondWaveNode.EnemyPosDic.Add   (8, ListToVector3( inScenePos_D.NpcSecondPoint8));
            SecondWaveNode.EnemyPosDic.Add   (9, ListToVector3( inScenePos_D.NpcSecondPoint9));
        }
        private void                    SetThirdWaveNode (Configs_BattleScenePosData inScenePos_D)                                      // 设置 敌我成员 第二波战斗初始坐标顶点
        {
            ThirdWaveNode                   = new SceneNode();
            ThirdWaveNode.CameraPos         = ListToVector3(inScenePos_D.CameraPoint4);

            ThirdWaveNode.OurPosDic.Add     (1, ListToVector3( inScenePos_D.ThirdPoint1));
            ThirdWaveNode.OurPosDic.Add     (2, ListToVector3( inScenePos_D.ThirdPoint2));
            ThirdWaveNode.OurPosDic.Add     (3, ListToVector3( inScenePos_D.ThirdPoint3));
            ThirdWaveNode.OurPosDic.Add     (4, ListToVector3( inScenePos_D.ThirdPoint4));
            ThirdWaveNode.OurPosDic.Add     (5, ListToVector3( inScenePos_D.ThirdPoint5));
            ThirdWaveNode.OurPosDic.Add     (6, ListToVector3( inScenePos_D.ThirdPoint6));
            ThirdWaveNode.OurPosDic.Add     (7, ListToVector3( inScenePos_D.ThirdPoint7));
            ThirdWaveNode.OurPosDic.Add     (8, ListToVector3( inScenePos_D.ThirdPoint8));
            ThirdWaveNode.OurPosDic.Add     (9, ListToVector3( inScenePos_D.ThirdPoint9));

            ThirdWaveNode.EnemyPosDic.Add   (1, ListToVector3( inScenePos_D.NpcThirdPoint1));
            ThirdWaveNode.EnemyPosDic.Add   (2, ListToVector3( inScenePos_D.NpcThirdPoint2));
            ThirdWaveNode.EnemyPosDic.Add   (3, ListToVector3( inScenePos_D.NpcThirdPoint3));
            ThirdWaveNode.EnemyPosDic.Add   (4, ListToVector3( inScenePos_D.NpcThirdPoint4));
            ThirdWaveNode.EnemyPosDic.Add   (5, ListToVector3( inScenePos_D.NpcThirdPoint5));
            ThirdWaveNode.EnemyPosDic.Add   (6, ListToVector3( inScenePos_D.NpcThirdPoint6));
            ThirdWaveNode.EnemyPosDic.Add   (7, ListToVector3( inScenePos_D.NpcThirdPoint7));
            ThirdWaveNode.EnemyPosDic.Add   (8, ListToVector3( inScenePos_D.NpcThirdPoint8));
            ThirdWaveNode.EnemyPosDic.Add   (9, ListToVector3( inScenePos_D.NpcThirdPoint9));
        }
        private Vector3                 ListToVector3(List<float> inPosList)
        {
            return new Vector3      ( BattleParmConfig.ListToVector3(inPosList).x - 5,
                                      BattleParmConfig.ListToVector3(inPosList).y,
                                      BattleParmConfig.ListToVector3(inPosList).z     );
        }

        public SceneNode                GetCurrBirthNode()
        {
            if (BattleParmConfig.IsChangeScene)                                                                             return BirthNode;
            if (BattleControll.sInstance.EnemyProgress == 0 && BattleControll.sInstance.BattleType != BattleType.Guild)     return BirthNode;
            else                                                                                                            return GetMoveToNode();
        }
        public SceneNode                GetMoveToNode()                                                                                 // 摄像机移动到节点
        {
            if      (BattleParmConfig.IsChangeScene)                            return FirstWaveNode;
            if      (BattleControll.sInstance.EnemyProgress == 0)               return FirstWaveNode;
            else if (BattleControll.sInstance.EnemyProgress == 1)               return SecondWaveNode;
            else if (BattleControll.sInstance.EnemyProgress == 2)               return ThirdWaveNode;
            else
            {
                int                 TheProcess                                  = BattleControll.sInstance.EnemyProgress - 2;
                Vector3             ThePos                                      = ThirdWaveNode.EnemyPosDic[1] - SecondWaveNode.EnemyPosDic[1];
                return              BuildNewSceneNode                           (ThirdWaveNode, ThePos * TheProcess);
            }
        }
        private SceneNode               BuildNewSceneNode(SceneNode inBaseNode,Vector3 inPos)
        {
            SceneNode                   ChildNode                               = new SceneNode();
            ChildNode.CameraPos                                                 = inBaseNode.CameraPos + inPos;

            foreach ( var Item in inBaseNode.OurPosDic)
            {
                ChildNode.OurPosDic.Add(Item.Key, Item.Value + inPos);
            }
            foreach (var Item in inBaseNode.EnemyPosDic)
            {
                ChildNode.OurPosDic.Add(Item.Key, Item.Value + inPos);
            }
            return                      ChildNode;
        }
    }

    ///-----------------------------------------------------------------------------------------------------------------------------------------------------// <summary> 场景节点 </summary>
    public class SceneNode
    {
        public Vector3                  CameraPos                   = Vector3.zero;                                                     /// 摄影机坐标
        public Dictionary<int, Vector3> OurPosDic                   = new Dictionary<int, Vector3>();                                   /// 我方成员位置坐标集合
        public Dictionary<int, Vector3> EnemyPosDic                 = new Dictionary<int, Vector3>();                                   /// 敌方成员位置坐标集合
    }

}
