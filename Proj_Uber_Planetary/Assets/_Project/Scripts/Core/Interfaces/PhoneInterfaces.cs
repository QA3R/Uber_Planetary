using System.Collections.Generic;

namespace UberPlanetary.Core.Interfaces
{
    public interface IPhoneApplicationFeature : IPhoneNavigable
    {
        IPhoneNavigable ParentNavigable { get; set; }
        //Elements inside an application that can be interacted with
        //like a volume slider
        //a cross button to back out
        //Switching to different radio stations
        //toggling on car features
        //etc
    }

    public interface IPhoneApplication : IPhoneNavigable
    {
        //application specific functions
        //Like displaying notifications
        //Minimizing apps
        //Playing in background?
        //etc
        void DisplayNotification();
    }

    public interface IPhoneNavigable
    {
        void Enter();
        void Exit();

        void Select();
        void Deselect();
    }

    public interface IPhoneNavigator : IScrollHandler
    {
        IPhoneNavigable CurrentNavigable { get; }

        List<IPhoneNavigable> NavigableList { get; set; }
    }
}