using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结： 装饰模式是相当于把一个类加了一层或几层壳。


    //效果及实现要点
    //1．Component类在Decorator模式中充当抽象接口的角色，不应该去实现具体的行为。
    //   而且Decorator类对于Component类应该透明，换言之Component类无需知道Decorator类，Decorator类是从外部来扩展Component类的功能。
    //2．Decorator类在接口上表现为is-a Component的继承关系，即Decorator类继承了Component类所具有的接口。
    //   但在实现上又表现为has-a Component的组合关系，即Decorator类又使用了另外一个Component类。
    //   我们可以使用一个或者多个Decorator对象来“装饰”一个Component对象，且装饰后的对象仍然是一个Component对象。
    //3．Decortor模式并非解决“多子类衍生的多继承”问题，Decorator模式的应用要点在于解决“主体类在多个方向上的扩展功能”——是为“装饰”的含义。
    //4．对于Decorator模式在实际中的运用可以很灵活。
    //   如果只有一个ConcreteComponent类而没有抽象的Component类，那么Decorator类可以是ConcreteComponent的一个子类。
    //   如果只有一个ConcreteDecorator类，那么就没有必要建立一个单独的Decorator类，而可以把Decorator和ConcreteDecorator的责任合并成一个类。
    //5．Decorator模式的优点是提供了比继承更加灵活的扩展，通过使用不同的具体装饰类以及这些装饰类的排列组合，可以创造出很多不同行为的组合。
    //6．由于使用装饰模式，可以比使用继承关系需要较少数目的类。使用较少的类，当然使设计比较易于进行。
    //   但是，在另一方面，使用装饰模式会产生比使用继承关系更多的对象。更多的对象会使得查错变得困难，特别是这些对象看上去都很相像。
    //适用性
    //在以下情况下应当使用装饰模式：
    //1.需要扩展一个类的功能，或给一个类增加附加责任。 
    //2.需要动态地给一个对象增加功能，这些功能可以再动态地撤销。 
    //3.需要增加由一些基本功能的排列组合而产生的非常大量的功能，从而使继承关系变得不现实。
    //总结
    //Decorator模式采用对象组合而非继承的手法，实现了在运行时动态的扩展对象功能的能力，而且可以根据需要扩展多个功能，
    //避免了单独使用继承带来的“灵活性差”和“多子类衍生问题”。
    //同时它很好地符合面向对象设计原则中“优先使用对象组合而非继承”和“开放-封闭”原则。


    class DecoratorPattern
    {
        public DecoratorPattern()
        {
            DecLog decLog1 = new DecFileLog();
            DecLog decLog2 = new DecDataLog();

            LogWrapper logWrapper1 = new ErrorLogWrapper(decLog1);
            LogWrapper logWrapper2 = new PriorityLogWrapper(decLog2);
            logWrapper1.Write("aasswwe");
            logWrapper2.Write("qqwwe");
           
            Console.WriteLine("----------------------");
            logWrapper2.Log = logWrapper1;
            logWrapper2.Write("zzxxc");

        }

    }

    internal abstract class DecLog
    {
        public abstract void Write(string msg);
    }
    class DecFileLog:DecLog
    {
        public override void Write(string msg)
        {
            Console.WriteLine("DecFileLog-" + msg);
        }
    }
    class DecDataLog : DecLog
    {
        public override void Write(string msg)
        {
            Console.WriteLine("DecDataLog-"+msg);
        }
    }


    internal abstract class LogWrapper : DecLog
    {
        public DecLog Log;
        protected LogWrapper(DecLog log)
        {
            Log = log;
        }
        public override void Write(string msg)
        {
            Log.Write(msg);
        }
    }

    class ErrorLogWrapper:LogWrapper
    {
        public ErrorLogWrapper(DecLog Log) : base(Log)
        {
        }
        public override void Write(string msg)
        {
            ErrorStyle();
            base.Write(msg);
        }
        public void ErrorStyle()
        {
            Console.WriteLine("1级");
        }
    }

    class PriorityLogWrapper : LogWrapper
    {
        public PriorityLogWrapper(DecLog Log)
            : base(Log)
        {
        }
        public override void Write(string msg)
        {
            PriorityStyle();
            base.Write(msg);
        }
        public void PriorityStyle()
        {
            Console.WriteLine("2");
        }
    }
}
