using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class StartContext : MVCSContext
{
    public StartContext(MonoBehaviour View) : base(View) { }

    protected override void mapBindings()
    {
        mediationBinder.Bind<StartMovie>().To                       <StartMovieMediator>();                         // 开场动画
        mediationBinder.Bind<LoadingView>().To                      <LoadingViewMediator>();                        // 加载视图
        mediationBinder.Bind<LogInOrRegisterView>().To              <LogInOrRegisgerViewMediator>();                // 登录ro注册视图
        mediationBinder.Bind<RegisterView>().To                     <RegisterViewMediator>();                       // 注册视图
        mediationBinder.Bind<GameEnterView>().To                    <GameEnterViewMediator>();                      // 游戏进入视图
        mediationBinder.Bind<RoleCreateView>().To                   <RoleCreateViewMediator>();                     // 角色创建视图

        mediationBinder.Bind<ReLogInView>().To                      <ReLogInViewMediator>();                        // 重新登录视图
        mediationBinder.Bind<PublicInfoView>().To                   <PublicInfoViewMediator>();                     // 公告信息视图
        mediationBinder.Bind<ServerLineItemView>().To               <ServerLineItemViewMediator>();                 // 游戏服务器项目视图
        mediationBinder.Bind<ServerZoneItemView>().To               <ServerZoneItemViewMediator>();                 // 游戏服务器列表视图
        mediationBinder.Bind<ServerSelectPanelView>().To            <ServerSelectPanelViewMediator>();              // 游戏服务器选择视图

        mediationBinder.Bind<TextView>().To                         <TextViewMediator>();                           // 文本视图

        mediationBinder.Bind<DialogPanelView>().To                  <DialogPanelViewMediator>();                    // 对话面板视图       :全局
        mediationBinder.Bind<HidePanel30FloorView>().To             <HidePanel30FloorViewMediator>();               // 隐藏30面板视图     :全局
        mediationBinder.Bind<UISequenceEffectView>().To             <UISequenceEffectViewMediator>();               // UI序列特效         :全局


        commandBinder.Bind(StartEvent.CheckRes_Event).To            <CheckRes_Command>();                           // 校验版本,资源
        commandBinder.Bind(StartEvent.REQLoadConfig_Event).To       <REQLoadConfig_Command>();                      // 加载配置文件
        commandBinder.Bind(StartEvent.LogIn_Event).To               <LogIn_Command>();                              // 账号登录
        commandBinder.Bind(StartEvent.Register_Event).To            <Register_Command>();                           // 账号注册
        commandBinder.Bind(StartEvent.FastRegister_Event).To        <FastRegister_Command>();                       // 快速注册
        commandBinder.Bind(StartEvent.AccBind_Event).To             <AccBind_Command>();                            // 注册帐号绑定

        commandBinder.Bind(StartEvent.GetServerList_Event).To       <GetServerList_Command>();                      // 获取服务器列表
        commandBinder.Bind(StartEvent.GetPublicInfo_Event).To       <GetPublicInfo_Command>();                      // 获取公告信息
        commandBinder.Bind(StartEvent.GameEnter_Event).To           <GameEnter_Command>();                          // 登录游戏服

        commandBinder.Bind(StartEvent.GetNickName_Event).To         <GetNickName_Command>();                        // 获取昵称
        commandBinder.Bind(StartEvent.REQCheckCreateRole_Event).To  <REQCheckCreateRole_Command>();                 // 创建角色校验
        commandBinder.Bind(StartEvent.CreateRole_Event).To          <CreateRole_Command>();                         // 创建角色
        commandBinder.Bind(StartEvent.RoleEnter_Event).To           <RoleEnter_Command>();                          // 角色进入游戏
        commandBinder.Bind(StartEvent.ClientReady_Event).To         <ClientReady_Command>();                        // 客户端数据准备完毕


        commandBinder.Bind(RechargeEvent.GetPlayerID_Event).To      <REQPlayerIDCommand>();                         // 请求玩家ID
    }
}

