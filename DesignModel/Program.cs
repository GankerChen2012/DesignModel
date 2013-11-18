using System;
using System.Collections;

namespace DesignModel
{
    //http://bbs.51aspx.com/showtopic-43429.html

    internal class Program
    {
        private static void Main()
        {
            #region 创建型模式

            //工厂模式/抽象工厂模式
            //var factoryMethod = new FactoryMethod();

            //建造者模式
            //BuilderPattern builderPattern = new BuilderPattern();

            //单例模式
            //SinglePattern singlePattern=new SinglePattern();

            //原型模式(浅拷贝和深拷贝)
            //ProtypePattern protypePattern = new ProtypePattern();

            #endregion

            #region 结构型模式

            //适配器模式
            //AdapterPattern adapterPattern=new AdapterPattern();

            //桥接模式
            //BridgePattern bridgePattern = new BridgePattern();

            //组合模式
            //CompositePattern compositePattern=new CompositePattern();

            //装饰模式
            //DecoratorPattern decoratorPattern=new DecoratorPattern();

            //外观模式
            //FacadePattern facadePattern=new FacadePattern();

            //享元模式
            //FlyweightPattern flyweightPattern=new FlyweightPattern();

            //代理模式
            //ProxyPattern proxyPattern=new ProxyPattern();

            #endregion

            #region 行为型模式

            //职责链模式
            //ChainOfResponsibility chainOfResponsibility=new ChainOfResponsibility();

            //命令模式
            //CommandPattern commandPattern=new CommandPattern();

            //解释器模式
            InterpreterPattern InterpreterPattern=new InterpreterPattern();

            #endregion


            Console.ReadKey();
        }
    }



}
