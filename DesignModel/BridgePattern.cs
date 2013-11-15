using System;

namespace DesignModel
{
    //自我总结： 桥接模式很好的分离了各个模块的实现。让实现简单化。
    //实现了封闭-开发原则


    //效果及实现要点
    //1．Bridge模式使用“对象间的组合关系”解耦了抽象和实现之间固有的绑定关系，使得抽象和实现可以沿着各自的维度来变化。
    //2．所谓抽象和实现沿着各自维度的变化，即“子类化”它们，得到各个子类之后，便可以任意它们，从而获得不同平台上的不同型号。
    //3．Bridge模式有时候类似于多继承方案，但是多继承方案往往违背了类的单一职责原则（即一个类只有一个变化的原因），复用性比较差。Bridge模式是比多继承方案更好的解决方法。
    //4．Bridge模式的应用一般在“两个非常强的变化维度”，有时候即使有两个变化的维度，但是某个方向的变化维度并不剧烈——换言之两个变化不会导致纵横交错的结果，并不一定要使用Bridge模式。
    //适用性
    //在以下的情况下应当使用桥梁模式：
    //1．如果一个系统需要在构件的抽象化角色和具体化角色之间增加更多的灵活性，避免在两个层次之间建立静态的联系。 
    //2．设计要求实现化角色的任何改变不应当影响客户端，或者说实现化角色的改变对客户端是完全透明的。 
    //3．一个构件有多于一个的抽象化角色和实现化角色，系统需要它们之间进行动态耦合。 
    //4．虽然在系统中使用继承是没有问题的，但是由于抽象化角色和具体化角色需要独立变化，设计要求需要独立管理这两者。
    //总结
    //Bridge模式是一个非常有用的模式，也非常复杂，它很好的符合了开放-封闭原则和优先使用对象，而不是继承这两个面向对象原则。


    class BridgePattern
    {
        public BridgePattern()
        {
            ImpBridgeLog impN=new NImpBridge();
            ImpBridgeLog impJ=new JImpBridge();

            BridgeLog bridge=new BridgeDataLog();
            bridge.ImpBridgeLog = impN;
            bridge.Write("BridgeDataLog");

            bridge.ImpBridgeLog = impJ;
            bridge.Write("BridgeDataLog");

            BridgeLog bl=new BridgeFileLog();
            bl.ImpBridgeLog = impJ;
            bl.Write("BridgeFileLog");

        }
    }

    public abstract class ImpBridgeLog
    {
        public abstract void WriteLog(string msg);
    }

    public class NImpBridge:ImpBridgeLog
    {
        public override void WriteLog(string msg)
        {
            Console.WriteLine("N-Log-"+msg);
        }
    }
    public class JImpBridge:ImpBridgeLog
    {
        public override void WriteLog(string msg)
        {
            Console.WriteLine("J-Log-" + msg);
        }
    }


    public abstract class BridgeLog
    {
        protected ImpBridgeLog impBridgeLog;
        public ImpBridgeLog ImpBridgeLog
        {
            set { impBridgeLog = value; }
        }

        public abstract void Write(string msg);
    }

    public class BridgeFileLog : BridgeLog
    {
        public override void Write(string msg)
        {
            impBridgeLog.WriteLog(msg);
        }
    }

    public class BridgeDataLog : BridgeLog
    {
        public override void Write(string msg)
        {
            impBridgeLog.WriteLog(msg);
        }
    }



}
