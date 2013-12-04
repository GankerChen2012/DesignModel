using System;
using System.Collections;

namespace DesignModel
{
    //迭代器模式 就是 把一个数组里面的数据一个个的查出来 简称 循环


    //Iterator模式就是分离了集合对象的遍历行为，抽象出一个迭代器类来负责，
    //这样既可以做到不暴露集合的内部结构，又可让外部代码透明的访问集合内部的数据

    //效果及实现要点
    //1．迭代抽象：访问一个聚合对象的内容而无需暴露它的内部表示。
    //2．迭代多态：为遍历不同的集合结构提供一个统一的接口，从而支持同样的算法在不同的集合结构上进行操作。
    //3．迭代器的健壮性考虑：遍历的同时更改迭代器所在的集合结构，会导致问题。
    //适用性
    //1．访问一个聚合对象的内容而无需暴露它的内部表示。 
    //2．支持对聚合对象的多种遍历。 
    //3．为遍历不同的聚合结构提供一个统一的接口(即, 支持多态迭代)。
    //总结
    //Iterator模式就是分离了集合对象的遍历行为，抽象出一个迭代器类来负责，这样既可以做到不暴露集合的内部结构，又可让外部代码透明的访问集合内部的数据。



    class IteratorPattern
    {
        CommonIterator CommonIterator=new CommonIterator();
       
        NetTerator netTerator=new NetTerator();
    }

    #region 一个简单的迭代器示例 没有利用任何的.NET特性

    class CommonIterator
    {
        public CommonIterator()
        {
            IIterator iterator;
            IList list = new ConcreteList();
            iterator = list.GetIterator();

            while (iterator.MoveNext())
            {
                var i = (int) iterator.CurrentItem();
                Console.WriteLine(i.ToString());
                iterator.Next();
            }

        }
    }

    public interface IIterator
    {
        bool MoveNext();
        object CurrentItem();
        void First();
        void Next();
    }
    public interface IList
    {
        IIterator GetIterator();
    }
    //具体迭代器
    public class ConcreteIterator : IIterator
    {
        private readonly ConcreteList list;
        private int index;

        public ConcreteIterator(ConcreteList list)
        {
            this.list = list;
            index = 0;
        }

        public bool MoveNext()
        {
            return index < list.Length;
        }
        public object CurrentItem()
        {
            return list.GetElement(index);
        }
        public void First()
        {
            index = 0;
        }
        public void Next()
        {
            if (index < list.Length)
                index++;
        }
    }
    //具体聚集
    public class ConcreteList : IList
    {
        private int[] list;
        public ConcreteList()
        {
            list = new int[] {32, 2, 223, 4, 5};
        }

        public IIterator GetIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Length {
            get { return list.Length; }
        }

        public int GetElement(int index)
        {
            return list[index];
        }

    }

    #endregion

    #region 用.NET特性 实现的一个迭代器
    
    public class Persons : IEnumerable
    {
        private string[] m_Names;

        public Persons(params string[] names)
        {
            m_Names=new string[names.Length];
            names.CopyTo(m_Names,0);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var mName in m_Names)
            {
                yield return mName + "-yield";
            }
        }
      
    }
    class NetTerator
    {
        public NetTerator()
        {
            Persons persons=new Persons("32","43","321","76");
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }
    }

    #endregion

}

