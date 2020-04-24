using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.context.api;
public class GameContext : MVCSContext {
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="autoMapping"></param>
    public GameContext(MonoBehaviour view, bool autoMapping) : base(view, autoMapping)
    {

    }
    protected override void mapBindings()
    {
        // Modle
        //绑定数据模型类，在外界使用已绑定的模型类不需要再实例化，定义模型对象属性前[Inject]
        injectionBinder.Bind<IntegrationModel>().To<IntegrationModel>().ToSingleton();
        injectionBinder.Bind<RoundModel1>().To<RoundModel1>().ToSingleton();
        injectionBinder.Bind<CardModel>().To<CardModel>().ToSingleton();
        //View
        mediationBinder.Bind<StartView>().To<StartMediator>();
        mediationBinder.Bind<IntegrationView>().To<IntegrationMediator>();
        mediationBinder.Bind<CharacterView>().To<CharacterMediator>();
        mediationBinder.Bind<GameOverView>().To<GameOverMediator>();
        //Command
        commandBinder.Bind(CommandEvent.ChangeMulitiple).To<ChangeMulitipleCommand>();
        commandBinder.Bind(CommandEvent.RequestDealCommand).To<RequestDealCommand>();//绑定发牌命令与命令事件
        commandBinder.Bind(CommandEvent.GrabLandord).To<GrabLnadlordCommand>();//绑定发地主牌
        commandBinder.Bind(CommandEvent.PlayCard).To<PlayCardCommand>();//绑定是否出牌命令的触发事件
        commandBinder.Bind(CommandEvent.PassCard).To<PassCardCommand>();//绑定不出牌命令
        commandBinder.Bind(CommandEvent.GameOver).To<GameOverCommand>();//绑定游戏结束命令的事件
        commandBinder.Bind(CommandEvent.RequestUpdate).To<RequestUpdateCommand>();//绑定更新积分显示命令的事件
        commandBinder.Bind(CommandEvent.CommandUpdateGameOver).To<UpdateGameOverCommand>();//绑定更新结束面板的命令与事件
        #region //绑定命令事件
        //绑定框架内置的命令方法，在命令方法中调用我们要执行的事件,执行一次就解绑
        #endregion
        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
      
    }
}
