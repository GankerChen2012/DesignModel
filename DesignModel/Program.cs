using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignModel
{
    //http://bbs.51aspx.com/showtopic-43429.html

    internal class Program
    {
        //-----抽象类和接口的区别-----
        //就像铁门木门都是门（抽象类），你想要个门我给不了（不能实例化），但我可以给你个具体的铁门或木门（多态）；
        //而且只能是门，你不能说它是窗（单继承）；
        //一个门可以有锁（接口）也可以有门铃（多实现）。 
        //门（抽象类）定义了你是什么，接口（锁）规定了你能做什么
        //（一个接口最好只能做一件事，你不能要求锁也能发出声音吧（接口污染））。


        private static void Main()
        {
            #region 创建型模式

            //工厂模式/抽象工厂模式
            //var factoryMethod = new FactoryMethod();
            
            //反射
            //Reflect reflect = new Reflect();
            //reflect.ProductReflect("CarA");

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
            //InterpreterPattern InterpreterPattern=new InterpreterPattern();

            //模版方法模式
            //TemplateMethod TemplateMethod=new TemplateMethod();

            //迭代器模式
            //IteratorPattern IteratorPattern = new IteratorPattern();

            //观察者模式
            //OberverPattern OberverPattern=new OberverPattern();

            //中介者模式
            //MediatorPattern mediatorPattern=new MediatorPattern();

            //备忘录模式
            //MementoPattern MementoPattern=new MementoPattern();

            //状态模式
            //StatePattern statePattern = new StatePattern();

            //策略模式
            //StrategyPattern strategyPattern=new StrategyPattern();

            //访问者模式
            //VisitorPattern VisitorPattern=new VisitorPattern();

            #endregion

            //迭代
            //Parallel.For(0, 1000, i =>
            //    {
            //        Console.WriteLine(i);
            //    });
            
            //线程安全
            //Interlocked.Increment(ref counter);
            



            

            Console.ReadKey();

        }
    }






}
