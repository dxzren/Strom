using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------///<summary>  战斗基础环境设置  </summary>
    public class BattleContext : MVCSContext
    {
        public BattleContext(MonoBehaviour view) : base(view)
        { }

        protected override void mapBindings()
        {


            mediationBinder.Bind    <TipsPanelView>().To                        <TipsPanelViewMediator>     ();             /// Tips面板视图
            
            mediationBinder.Bind    <BattleMainPanelView>().To                  <BattleMainPanelMediator>   ();             /// 战斗主界面视图
            mediationBinder.Bind    <BattleMemView>().To                        <BattleMemMediator>         ();             /// 战斗成员视图

            commandBinder.Bind      (BattleEvent.BattleResInit_Event).To        <BattleResInit_Command>     ();             /// 战斗资源初始化
            commandBinder.Bind      (BattleEvent.BattleResLoad_Event).To        <BattleResLoad_Command>     ();             /// 战斗资源加载
            commandBinder.Bind      (BattleEvent.BattleDataInit_Event).To       <BattleDataInit_Command>    ();             /// 战斗数据初始化
            commandBinder.Bind      (BattleEvent.BattleMemDataLoad_Event).To    <BattleMemDataLoad_Command> ();             /// 战斗成员数据 加载
            commandBinder.Bind      (BattleEvent.BattleMemShow_Event).To        <BattleMemShow_Command>     ();             /// 战斗成员展示
//            commandBinder.Bind      (BattleEvent.BattleMemPosInit_Event).To     <BattleMemPosInit_Command>  ();             /// 战斗成员初始化位置

            commandBinder.Bind      (BattleEvent.BackToMianUI_Event).To         <BackToMainUI_Command>      ();             /// 返回主界面

                                                                                                                            /// <| 断线重连 >
            commandBinder.Bind      (StartEvent.LogIn_Event).To                 <LogIn_Command>             ();             /// 登录账号
            commandBinder.Bind      (StartEvent.GameEnter_Event).To             <GameEnter_Command>         ();             /// 进入游戏服务器
            commandBinder.Bind      (StartEvent.RoleEnter_Event).To             <RoleEnter_Command>         ();             /// 角色进入
            commandBinder.Bind      (StartEvent.ClientReady_Event).To           <ClientReady_Command>       ();             /// 客户端准备完毕

            commandBinder.Bind      (ContextEvent.START).To                     <StartGame_Command>().Once();               /// 启动  < 本环境加载完毕后启动 返回到第一个执行命令>
        }

    }
}

