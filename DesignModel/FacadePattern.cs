using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结： 感觉就是把功能点整合在一起。
    //比如游戏，点击开始时执行的是start方法，但start方法里面执行了很多其他的内容。他们都是为了实现开始游戏而做的。

    
    //外观模式(Facade Pattern）可以将一系列复杂的类包装成一个简单的封闭接口。也称门面模式.

    //Abstract Factory模式可以与Facade模式一起使用以提供一个接口，这一接口可用来以一种子系统独立的方式创建子系统对象。
    //Abatract Factory模式也可以代替Facade模式隐藏那些与平台相关的类
    //Mediator模式与Facade模式的相似之处是，它抽象了一些已有的类的功能 

    //应用场景
    //为子系统中的接口提供一个一致的调用方法，一般在项目的前期不用,后期解耦时候考虑用
    //开发阶段，会产生很多小的类，为了减少类之间的耦合，可以用facade模式定义一个统一的接口
    //在对老系统进行维护和升级的时候，可能要调用以前的方法，同时不对这些方法进行修改，可以考虑定义一个更高层次的接口，调用遗留的老方法，
    //新系统不直接调用原来的方法，而是通过访问这个更高层次的接口调用以前的方法

    class FacadePattern
    {
        public FacadePattern()
        {
            Entity entity = new Entity();
            entity.Activity();
            entity.Rest();
        }
    }

    class Eat
    {
        public void Action()
        {
            Console.WriteLine("Eat");
        }
    }
    class Drink
    {
        public void Action()
        {
            Console.WriteLine("Drink");       
        }
    }
    class Rest
    {
        public void Sleep()
        {
            Console.WriteLine("sleep");
        }
    }

    internal class Entity
    {
        private readonly Eat eat;
        private readonly Drink drink;
        private readonly Rest rest;
        public Entity() //构造函数
        {
            eat=new Eat();
            drink=new Drink();
            rest=new Rest();
        }
        public void Activity() //吃喝玩乐
        {
            eat.Action();
            drink.Action();
        }
        public void Rest() //休息
        {
            rest.Sleep();
        }
    }


}
