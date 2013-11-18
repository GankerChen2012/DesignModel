using System;

namespace DesignModel
{
    //自我总结：适配器就是插头 在不改变原有代码的基础上 修改实现流程。


    //实现要点
    //1．Adapter模式主要应用于“希望复用一些现存的类，但是接口又与复用环境要求不一致的情况”，在遗留代码复用、类库迁移等方面非常有用。
    //2．Adapter模式有对象适配器和类适配器两种形式的实现结构，但是类适配器采用“多继承”的实现方式，带来了不良的高耦合，所以一般不推荐使用。对象适配器采用“对象组合”的方式，更符合松耦合精神。
    //3．Adapter模式的实现可以非常的灵活，不必拘泥于GOF23中定义的两种结构。例如，完全可以将Adapter模式中的“现存对象”作为新的接口方法参数，来达到适配的目的。
    //4．Adapter模式本身要求我们尽可能地使用“面向接口的编程”风格，这样才能在后期很方便的适配。[以上几点引用自MSDN WebCast]
    //效果
    //对于类适配器：
    //1．用一个具体的Adapter类对Adaptee和Taget进行匹配。结果是当我们想要匹配一个类以及所有它的子类时，类Adapter将不能胜任工作。
    //2．使得Adapter可以重定义Adaptee的部分行为，因为Adapter是Adaptee的一个子类。
    //3．仅仅引入了一个对象，并不需要额外的指针一间接得到Adaptee.
    //对于对象适配器：
    //1．允许一个Adapter与多个Adaptee，即Adaptee本身以及它的所有子类（如果有子类的话）同时工作。Adapter也可以一次给所有的Adaptee添加功能。
    //2．使得重定义Adaptee的行为比较困难。这就需要生成Adaptee的子类并且使得Adapter引用这个子类而不是引用Adaptee本身。
    //适用性
    //在以下各种情况下使用适配器模式：
    //1．系统需要使用现有的类，而此类的接口不符合系统的需要。
    //2．想要建立一个可以重复使用的类，用于与一些彼此之间没有太大关联的一些类，包括一些可能在将来引进的类一起工作。这些源类不一定有很复杂的接口。
    //3．（对对象适配器而言）在设计里，需要改变多个已有子类的接口，如果使用类的适配器模式，就要针对每一个子类做一个适配器，而这不太实际。


    class AdapterPattern
    {
        public AdapterPattern()
        {
            //类适配器
            FileClassLog fileLog = new FileClassLog();
            fileLog.Write();

            //类适配器
            FileLogTest fileLogTest = new FileLogTest();
            fileLogTest.Write();
            fileLogTest.WriteLog();

            //没有适配器
            FileLog dataBaseLog = new FileLog();
            dataBaseLog.Write();

            FileLogAdapter fileLogAdapter = new FileLogAdapter();
            fileLogAdapter.WriteLog();

            //对象适配器
            LogAdapter fileObject = new LogAdapter(new FileObjectLogAdapter());
            fileObject.Write();

        }
    }

    public interface ILog
    {
        void Write();
    }

    public class FileClassLog : FileClassLogAdapter, ILog
    {
        public void Write()
        {
            WriteLog();
        }
    }
    public class FileLog : ILog
    {
        public void Write()
        {
            Console.WriteLine("FileLog");
        }
    }
    public class FileObjectLog : ILog
    {
        public void Write()
        {
            Console.WriteLine("FileObjectLog");
        }
    }


    public abstract class AbLogAdapter
    {
        public abstract void WriteLog();
    }
    public class FileClassLogAdapter : AbLogAdapter
    {
        public override void WriteLog()
        {
            Console.WriteLine("d");
        }
    }
    public class FileLogAdapter : AbLogAdapter
    {
        public override void WriteLog()
        {
            Console.WriteLine("FileLogAdapter");
        }
    }
    public class FileObjectLogAdapter : AbLogAdapter
    {
        public override void WriteLog()
        {
            Console.WriteLine("FileObjectLog");
        }
    }

    public class FileLogTest : FileLogAdapter, ILog
    {
        public void Write()
        {
            WriteLog();
        }
    }

    public class LogAdapter : ILog
    {
        private readonly AbLogAdapter adaptee;
        public LogAdapter(AbLogAdapter adaptee)
        {
            this.adaptee = adaptee;
        }
        public void Write()
        {
            adaptee.WriteLog();
        }
    }
}


