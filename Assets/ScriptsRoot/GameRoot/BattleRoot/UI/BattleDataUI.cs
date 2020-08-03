using UnityEngine;
using System.Collections;
namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  战斗数据设置UI  </summary>
    public class BattleDataSetUI : MonoBehaviour
    {
        public UIGrid               Our_Grid, Enemy_Grid;
        public GameObject           ConfirmBtnObj;

        System.Action               OnComplete;

        private void                Start           ()                                                                      // 基础初始化              
        {
            UIEventListener.Get(ConfirmBtnObj).onClick              = ConfirmOnClick;                                       // 确认点击_事件监听
        }

        public void                 ViewInit        (IBattleStartData inIBattleStart_D,System.Action inOnComplete)          // 视图初始化              
        {
            OnComplete                                              = inOnComplete;                                         // 完成回调处理

            foreach ( var Item in inIBattleStart_D.OurMemberList)                                                           // 添加我方成员到Grid子项
            {   AddDataItem(Item, Our_Grid);                        }
            Our_Grid.repositionNow                                  = true;                                                 // 我方重新定位

            foreach ( var Item in inIBattleStart_D.EnemyMemListAtWaveDic[inIBattleStart_D.CurrBattleWave])                  // 添加敌方成员到Grid子项
            {   AddDataItem(Item, Enemy_Grid);                      }


            Enemy_Grid.repositionNow                                = true;                                                 // 敌方重新定位
        }
        public void                 ConfirmOnClick  ( GameObject inObj)                                                     // 确认按钮点击            
        {
            foreach(var Item in gameObject.GetComponentsInChildren<BattleDataUI>(true))                                     /// 保存设置对象子集_设置到成员数据
            {                       Item.SaveToMemData();           }

            PanelManager.sInstance.HidePanel(SceneType.Battle, this.name);                                                  /// 隐藏当前面板
            OnComplete();                                                                                                   /// 完成回调处理
        }


        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private void                AddDataItem     (IBattleMemberData inIMemData,UIGrid inGrid)                            // 添加数据到(this)子项    
        {
            Object                  TempObj                         = Util.Load(BattleResStrName.PanelName_BattleDataItem); /// 加载UI资源
            GameObject              TheObj                          = Instantiate(TempObj) as GameObject;                   /// 实例化对象
            TempObj                                                 = null;                                                 /// 清理临时数据

            TheObj.name                                             = inIMemData.MemberID.ToString();                       /// BattleDataUI_Obj 初始化配置
            TheObj.transform.parent                                 = inGrid.transform;
            TheObj.transform.localPosition                          = Vector3.zero;
            TheObj.transform.localScale                             = Vector3.one;
            TheObj.transform.localRotation                          = Quaternion.identity;      

            TheObj.GetComponent<BattleDataUI>().MemberSet(inIMemData);                                                      /// 战斗数据UI_成员设置
        }

        #endregion

    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  战斗数据UI  </summary>

    public class BattleDataUI : MonoBehaviour
    {
        public UILabel              Name_Label, MaxHp_Label,    MaxAttack_Label;                                            /// 名称, 最大血量, 最大攻击
        public UIInput              Hp_Input,   Energy_Input,   Attack_Input;                                               /// 血量, 能量, 攻击

        public IBattleMemberData    TheIMem_D;                                                                              /// 成员数据实例

        public void                 MemberSet       (IBattleMemberData inIMem_D)                                            // 成员设置                 
        {
            TheIMem_D                                               = inIMem_D;                                             /// 成员数据
            Name_Label.text                                         = Language.GetValue(TheIMem_D.memberName);              /// 名称
            MaxHp_Label.text                                        = TheIMem_D.Hp.ToString();                              /// 最大血量
            MaxAttack_Label.text                                    = TheIMem_D.PhyAttack.ToString();                       /// 最大攻击力

            Hp_Input.value                                          = TheIMem_D.Hp.ToString();                              /// 血量
            Energy_Input.value                                      = "0";                                                  /// 能力值
            Attack_Input.value                                      = TheIMem_D.PhyAttack.ToString();                       /// 攻击力
        }
        public void                 SaveToMemData   ()                                                                      // 设置参数保存到成员数据    
        {
            TheIMem_D.Hp                                            = Util.ParseToInt(Hp_Input.value);
            TheIMem_D.CurrAnger                                     = Util.ParseToInt(Energy_Input.value);
            TheIMem_D.PhyAttack                                     = Util.ParseToInt(Attack_Input.value);
        }
    }

}
