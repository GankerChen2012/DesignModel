using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结：中介者模式 把所有的业务逻辑放到中介者类里面去处理，这样修改时只需要修改中介者类，很方便。 
    //但如果同事类多了的话，那中介者类逻辑会很复杂。。


    //没有使用中介者模式之前，对象之间是直接依赖的，对象间的交互方式为直接交互。
    //使用了中介者模式后，每一个对象依赖于中介者对象，需要与其他对象交互时，通过中介者与其他对象交互，是一种间接交互。
    //我们可以再不修改任何同事类的条件下，改变同事类的交互（通过修改中介者）。

    //中介者模式在实际中的应用：
    //1. 每一个机场都有"XXX机场调度中心"。
    //这是一个现实版的中介者模式，如果没有这个调度中心，每一架飞机（同事类）在到达机场时，
    //都需要先与其他飞机交互看看有没有飞机与我降落冲突，然后与机场（同事类）交互有没有空闲跑到，
    //最后与后勤部门（同事类）交互看有没有将停机资源准备好等等，这样的情况是难以想象的。
    //2. MVC框架。Struts2的控制器就是一个典型的中介者。
    //3. 现实中的各种中介公司。




    public class MediatorPattern
    {
        public MediatorPattern()
        {
            var playerController = new PlayerController();
            var playerControllerButton1 = new PauseButton(playerController);
            var playerControllerButton2 = new StartButton(playerController);
            var playerControllerButton3 = new StopButton(playerController);
            
            playerControllerButton1.Click();
            playerControllerButton2.Click();
            playerControllerButton3.Click();
        }
    }

    public abstract class PlayerControllerButton
    {
        protected PlayerController Controller;
        public bool Enable { get; set; }

        protected PlayerControllerButton(PlayerController controller)
        {
            Controller = controller;
        }

        public virtual void Click()
        {
            Controller.ClickButton(this);
        }
    }

    public class StartButton : PlayerControllerButton
    {
        public StartButton(PlayerController controller)
            : base(controller)
        {
            controller.Register(this);
        }
    }
    public class StopButton : PlayerControllerButton
    {
        public StopButton(PlayerController controller)
            : base(controller)
        {
            controller.Register(this);
        }
    }
    public class PauseButton : PlayerControllerButton
    {
        public PauseButton(PlayerController controller)
            : base(controller)
        {
            controller.Register(this);
        }
    }

    public class PlayerController
    {
        private StartButton startButton;
        private StopButton stopButton;
        private PauseButton pauseButton;

        public void Register(PlayerControllerButton button)
        {
            switch (button.GetType().ToString())
            {
                case "DesignModel.StartButton":
                    startButton = (StartButton) button;
                    break;
                case "DesignModel.StopButton":
                    stopButton = (StopButton) button;
                    break;
                case "DesignModel.PauseButton":
                    pauseButton = (PauseButton) button;
                    break;
            }
        }

        public void ClickButton(PlayerControllerButton button)
        {
            if (button == startButton)
            {
                startButton.Enable = true;
                stopButton.Enable = false;
                pauseButton.Enable = false;
                DisplayButtonState();
            }
            else if (button == stopButton)
            {
                startButton.Enable = false;
                stopButton.Enable = true;
                pauseButton.Enable = false;
                DisplayButtonState();
            }
            else if (button == pauseButton)
            {
                startButton.Enable = false;
                stopButton.Enable = false;
                pauseButton.Enable = true;
                DisplayButtonState();
            }
        }

        public void DisplayButtonState()
        {
            Console.WriteLine(
                "StartButton is {0}, StopButton is {1}, PauseButton is {2}", 
                startButton.Enable,
                stopButton.Enable, 
                pauseButton.Enable);
        }

    }
}
