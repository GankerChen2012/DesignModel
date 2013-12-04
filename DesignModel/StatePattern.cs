using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //状态模式： 在适当的时候 替代if else，使代码层次更分明。 和策略模式差不多


    //在面向对象软件设计时，常常碰到某一个对象由于状态的不同而有不同的行为。
    //如果用if else或是switch case等方法处理，对象操作及对象的状态就耦合在一起，碰到复杂的情况就会造成代码结构的混乱。
    //在这种情况下，就可以使用状态模式来解决问题。

    //状态模式允许一个对象在其内部状态改变时改变它的行为，使对象看起来似乎修改了它的类。

  //状态模式可以有效的替换充满在程序中的if else语句：将不同条件下的行为封装在一个类里面，再给这些类一个统一的父类来约束他们。
  
  //1) 使用环境（Context）角色：客户程序是通过它来满足自己的需求。
  //   它定义了客户程序需要的接口；并且维护一个具体状态角色的实例，这个实例来决定当前的状态。 
  //2) 状态（State）角色：定义一个接口以封装与使用环境角色的一个特定状态相关的行为。
  //3) 具体状态（Concrete State）角色：实现状态角色定义的接口。


    class StatePattern
    {
        public StatePattern()
        {
            Light light=new Light();

            light.PressSwtich();
            light.PressSwtich();
            light.PressSwtich();

        }
    }

    public class Light
    {
        public LightState State;

        public Light()
        {
            State=new LightOn();
        }

        public void PressSwtich()
        {
            State.PressSwitch(this);
        }

    }

    public interface LightState
    {
        void PressSwitch(Light light);
    }

    public class LightOn : LightState
    {
        public void PressSwitch(Light light)
        {
            Console.WriteLine("Light off");
            light.State = new LightOff();
        }
    }
    public class LightOff : LightState
    {
        public void PressSwitch(Light light)
        {
            Console.WriteLine("Light on");
            light.State = new LightOn();
        }
    }


}
