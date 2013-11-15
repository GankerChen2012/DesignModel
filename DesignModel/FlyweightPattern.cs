using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结： 享元模式就是共享资源。 有点像单例模式。同一个对象只有一个实例。其他的都是引用这一个实例。


    //效果及实现要点
    //1．面向对象很好的解决了抽象性的问题，但是作为一个运行在机器中的程序实体，我们需要考虑对象的代价问题。Flyweight设计模式主要解决面向对象的代价问题，一般不触及面向对象的抽象性问题。
    //2．Flyweight采用对象共享的做法来降低系统中对象的个数，从而降低细粒度对象给系统带来的内存压力。在具体实现方面，要注意对象状态的处理。
    //3．享元模式的优点在于它大幅度地降低内存中对象的数量。但是，它做到这一点所付出的代价也是很高的：享元模式使得系统更加复杂。为了使对象可以共享，需要将一些状态外部化，这使得程序的逻辑复杂化。另外它将享元对象的状态外部化，而读取外部状态使得运行时间稍微变长。
    //适用性
    //当以下所有的条件都满足时，可以考虑使用享元模式：
    //1、   一个系统有大量的对象。 
    //2、   这些对象耗费大量的内存。 
    //3、   这些对象的状态中的大部分都可以外部化。 
    //4、   这些对象可以按照内蕴状态分成很多的组，当把外蕴对象从对象中剔除时，每一个组都可以仅用一个对象代替。 
    //5、   软件系统不依赖于这些对象的身份，换言之，这些对象可以是不可分辨的。
    //满足以上的这些条件的系统可以使用享元对象。最后，使用享元模式需要维护一个记录了系统已有的所有享元的表，而这需要耗费资源。因此，应当在有足够多的享元实例可供共享时才值得使用享元模式。
    //总结
    //Flyweight模式解决的是由于大量的细粒度对象所造成的内存开销的问题，它在实际的开发中并不常用，但是作为底层的提升性能的一种手段却很有效。

    class FlyweightPattern
    {
        public FlyweightPattern()
        {
            CharFactory charFactory=new CharFactory();

            var ca = charFactory.GetChar("A");
            ca.SetSize(10);
            ca.Write();

            CharB cb1 = (CharB)charFactory.GetChar("B");
            cb1.SetSize(12);
            cb1.Write();

            CharB cb2 = (CharB)charFactory.GetChar("B");
            cb2.SetSize(12);
            cb2.Write();
            Console.WriteLine(object.ReferenceEquals(cb1, cb2));


            CharB cb3 = new CharB();
            cb3.SetSize(12);
            Console.WriteLine(object.ReferenceEquals(cb1, cb3));

        }

    }

    internal abstract class Chars
    {
        protected string CharName;
        protected string Style;
        protected int Size;
        public abstract void SetSize(int size);
        public abstract void Write();
    }
    class CharA:Chars
    {
        public override void SetSize(int size)
        {
            Size = size;
        }
        public override void Write()
        {
            Console.WriteLine("A:1-" + Size);
        }
    }
    class CharB : Chars
    {
        public override void SetSize(int size)
        {
            Size = size;
        }
        public override void Write()
        {
            Console.WriteLine("b:2-" + Size);
        }
    }

    internal class CharFactory
    {
        private readonly Hashtable charFactory=new Hashtable();

        public CharFactory()
        {
            charFactory.Add("A",new CharA());
        }

        public Chars GetChar(string str)
        {
            var chars = charFactory[str] as Chars;
            if (chars == null)
            {
                switch (str)
                {
                    case "A":chars = new CharA();break;
                    case "B":chars = new CharB(); break;
                }
                charFactory.Add(str, chars);
            }
            return chars;
        }

    }


}
