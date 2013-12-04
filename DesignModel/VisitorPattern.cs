using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //访问者模式表示一个作用于某对象结构中的个元素操作。
    //它使你可以在不改变各元素类的前提下定义作用于这些元素的新操作. 访问者模式的目的是要把处理从数据结构分离出来.

    //解决的问题（What To Solve）
    //访问者模式的目的是要把处理从数据结构分离出来。
    //如果系统有比较稳定的数据结构，又有易于变化的算法的话，使用访问者模式是个不错的选择，因为访问者模式使的算法操作的增加变的容易。
    //相反，如果系统的数据结构不稳定，易于变化，则此系统就不适合使用访问者模式了。

    //访问者模式结构
    //Visitor：为对象结构中Element的每一个子类声明一个Visitor操作。
    //ConcreteVisitor：具体的访问者，实现父类Visitor的操作。每个操作实现算法的一部分，而该算法片段仍是对应与结构中对象的类。
    //Element：定义一个Accept操作，它以一个访问者为参数。
    //ConcreteElementA、ConcreteElementB: 具体元素，实现父类Element的方法Accept。


    class VisitorPattern
    {
        public VisitorPattern()
        {
            var concreteElementB = new ConcreteElementB();
            concreteElementB.Accept(new ConcreteVisitor());

            var concreteElementA = new ConcreteElementA();
            concreteElementA.Accept(new ConcreteVisitor());
        }
    }

    public abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }
    public class ConcreteElementA : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
    public class ConcreteElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public abstract class Visitor
    {
        public abstract void Visit(ConcreteElementA me);
        public abstract void Visit(ConcreteElementB me);
    }
    public class ConcreteVisitor : Visitor
    {
        public override void Visit(ConcreteElementA me)
        {
            Console.WriteLine("ConcreteElementA");
        }
        public override void Visit(ConcreteElementB me)
        {
            Console.WriteLine("ConcreteElementB");
        }
    }


}
