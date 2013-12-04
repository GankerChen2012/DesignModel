using System;
using System.Collections;
using System.Collections.Generic;

namespace DesignModel
{
    //自我总结：组合模式是加强版的工厂模式。使类可以组合存在也可以单独存在。动态组合与解除。


    //效果及实现要点
    //1．Composite模式采用树形结构来实现普遍存在的对象容器，从而将“一对多”的关系转化“一对一”的关系，使得客户代码可以一致地处理对象和对象容器，无需关心处理的是单个的对象，还是组合的对象容器。
    //2．将“客户代码与复杂的对象容器结构”解耦是Composite模式的核心思想，解耦之后，客户代码将与纯粹的抽象接口——而非对象容器的复内部实现结构——发生依赖关系，从而更能“应对变化”。
    //3．Composite模式中，是将“Add和Remove等和对象容器相关的方法”定义在“表示抽象对象的Component类”中，还是将其定义在“表示对象容器的Composite类”中，是一个关乎“透明性”和“安全性”的两难问题，需要仔细权衡。这里有可能违背面向对象的“单一职责原则”，但是对于这种特殊结构，这又是必须付出的代价。ASP.NET控件的实现在这方面为我们提供了一个很好的示范。
    //4．Composite模式在具体实现中，可以让父对象中的子对象反向追溯；如果父对象有频繁的遍历需求，可使用缓存技巧来改善效率。
    //适用性
    //以下情况下适用Composite模式：
    //1．你想表示对象的部分-整体层次结构
    //2．你希望用户忽略组合对象与单个对象的不同，用户将统一地使用组合结构中的所有对象。
    //总结
    //组合模式解耦了客户程序与复杂元素内部结构，从而使客户程序可以向处理简单元素一样来处理复杂元素。


    internal class CompositePattern
    {
        public CompositePattern()
        {
            //CompositePattern1 one = new CompositePattern1();

            CompositePattern2 two = new CompositePattern2();
        }

    }

    internal class CompositePattern1
    {
        public CompositePattern1()
        {
            var g = new Picture("test");
            g.Draw();

            Graphics gl = new Line("lineTest");
            gl.Draw();
            Graphics gr = new Round("RountTest");
            gr.Draw();

            Console.WriteLine("----------------------");
            g.Add(gl);
            g.Add(gr);
            g.Draw();

            Console.WriteLine("----------------------");
            g.Remove(gl);
            g.Draw();

        }
    }
    internal abstract class Graphics
    {
        public string Name;

        protected Graphics(string name)
        {
            Name = name;
        }

        public abstract void Draw();
    }
    internal class Line : Graphics
    {
        public Line(string name)
            : base(name)
        {
        }

        public override void Draw()
        {
            Console.WriteLine("Draw:Line-" + Name);
        }
    }
    internal class Round : Graphics
    {
        public Round(string name)
            : base(name)
        {
        }

        public override void Draw()
        {
            Console.WriteLine("Draw:Round-" + Name);
        }
    }
    internal class Picture : Graphics
    {
        protected ArrayList PicList = new ArrayList();

        public Picture(string name) : base(name)
        {
        }

        public override void Draw()
        {
            Console.WriteLine("Draw:Picture-" + Name);
            foreach (Graphics list in PicList)
            {
                list.Draw();
            }
        }

        public void Add(Graphics g)
        {
            PicList.Add(g);
        }

        public void Remove(Graphics g)
        {
            PicList.Remove(g);
        }
    }


    class CompositePattern2
    {
        public CompositePattern2()
        {
            Component root =new Composite("Root");
            root.Add(new Leaf("left A"));
            root.Add(new Leaf("left B"));

            Component a=new Composite("Composise A");
            a.Add(new Leaf("Composiseleft A"));
            a.Add(new Leaf("Composiseleft B"));

            root.Add(a);

            root.Display(1);
        }
    }
    abstract class Component
    {
        public string Name { get; set; }
        protected Component(string name)
        {
            Name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }
    class Leaf:Component
    {
        public Leaf(string name) : base(name)
        {
        }
        public override void Add(Component c)
        {
            throw new NotImplementedException();
        }
        public override void Remove(Component c)
        {
            throw new NotImplementedException();
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + Name);
        }
    }
    class Composite:Component
    {
        readonly List<Component> list=new List<Component>();
        public Composite(string name) : base(name)
        {
        }

        public override void Add(Component c)
        {
            list.Add(c);
        }

        public override void Remove(Component c)
        {
            list.Remove(c);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('~', depth) + Name);
            foreach (var c in list)
            {
                c.Display(depth+2);
            }
        }
    }




}
