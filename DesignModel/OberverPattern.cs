using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{

    //自我总结：观察者模式 能根据数据的变化同步更新相应的内容。 
    // 通知者(subject)给观察者(Ob)说 我这儿有个属性(State)，你帮我看到哈，然后当属性变化时，观察者就给通知者说，你的状态变了哈。
    // 一个通知者可以有多个观察者，从不同的角度去分析 通知者想让观察者知道的。


    //观察者模式可以用来同时以多种方式表示数据。
    //我们可能有一组数据，然后希望同时在用户界面用多种表示方式显示这组数据，比如用表格和绘图的两种方式。
    //并且当数据发生变化的时候我们希望数据的显示能够自动的更新，这就需要我们使用观察者模式。
    //观察者模式假设包含数据的对象与显示的对象是分离的，显示对象就需要观察数据中的变化。
    //在实现观察者模式的时候，我们通常把数据成为主题(Subject)，把每种显示方式成为观察者(Observer).
    //观察者都有一个为其他对象所知的接口，这样当数据放生变化的时候主题就可以通过调用这个接口告知观察者数据的变化。


    //观察者模式:定义对象间的一种一对多的关系，当一个对象的状态发生变化时，所有依赖于它的对象都得到通知并自动更新

    //观察者促进了到主题的抽象耦合。主题并不知道其任何观察者的细节。
    //不过，观察者模式也有一个缺点，当数据发生了一些列的增量改变的时候，观察者就会收到连续的数据变化通知并做出反复的更新。
    //如果这些更新的成本很高，则引入某种变更管理就是很有必要的了，这样观察者就不会太快或者太过于频繁的收到数据变化通知。

    class OberverPattern
    {
        public OberverPattern()
        {
            ConcreteSubject s=new ConcreteSubject();
            s.Attach(new ConcreteObserver(s, "a"));
            s.Attach(new ConcreteObserver(s, "b"));
            s.Attach(new ConcreteObserver(s, "c"));

            s.Attach(new TwoConcreteObserver(s, "stop"));

            s.SubjectState = "run";
            s.Notify();
            s.SubjectState = "stop";
            s.Notify();


            //用委托的方式
            var one = new ConcreteObserver(s, "a");
            var two = new TwoConcreteObserver(s, "b");

            var de=new DelegetSubject();
            de.Up += new DelegetSubject.EventHand(one.TestOneDelegate);
            de.Up += new DelegetSubject.EventHand(two.TestDelegate);
            de.Notify();
        }

    }

    //抽象观察者
    public abstract class Observer
    {
        public abstract void Update();
    }
    //抽象通知者
    public abstract class Subject
    {
        IList<Observer> listObserver=new List<Observer>();

        public void Attach(Observer observer)
        {
            listObserver.Add(observer);
        }

        public void Detach(Observer observer)
        {
            listObserver.Remove(observer);
        }

        public virtual void Notify()
        {
            foreach (var observer in listObserver)
            {
                observer.Update();
            }
        }

    }
    public class ConcreteSubject : Subject
    {
        public string SubjectState { get; set; }
    }

    public class ConcreteObserver : Observer
    {
        private readonly string name;
        private string observerState;
        private readonly ConcreteSubject subject;
        public ConcreteSubject Subject { get; set; }

        public ConcreteObserver(ConcreteSubject subject,string name)
        {
            this.subject = subject;
            this.name = name;
        }

        public override void Update()
        {
            observerState = subject.SubjectState;
            Console.WriteLine("{0} is {1}",name,observerState);
        }

        public void TestOneDelegate()
        {
            Console.WriteLine(name + "可以了");
        }

    }
    public class TwoConcreteObserver : Observer
    {
        private ConcreteSubject concreteTest;
        private string name;
        public TwoConcreteObserver(ConcreteSubject concreteTest, string name)
        {
            this.concreteTest = concreteTest;
            this.name = name;
        }

        public override void Update()
        {
            if (name == concreteTest.SubjectState)
                Console.WriteLine(name+"：吻合了");
        }

        public void TestDelegate()
        {
            Console.WriteLine(name+"可以了");
        }
    }


    public class DelegetSubject : Subject
    {
        public delegate void EventHand();
        public event EventHand Up;

        public override void Notify()
        {
            Up();
        }
    }


}
