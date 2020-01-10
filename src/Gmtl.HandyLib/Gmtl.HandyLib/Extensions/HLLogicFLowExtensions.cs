using System;

namespace Gmtl.HandyLib.Extensions
{
    public static class HLLogicFlowExtensions
    {
        //bool result = Operation(state);
        //if (result)
        //{
        //    ActionWithTrue(state);
        //}
        //else
        //{
        //    ActionWithFalse(state);
        //}
        //
        //ActionAlways(state);

        //bool result = Operation(false);
        //result
        //.True(state, s => { ActionWithTrue(s); })
        //.False(state, s => { ActionWithFalse(s); })
        //.Always(state, s => { ActionAlways(s); });

        public static bool True(this bool flowFlag, Action action)
        {
            if (flowFlag)
                action();

            return flowFlag;
        }

        public static bool False(this bool flowFlag, Action action)
        {
            if (!flowFlag)
                action();
            
            return flowFlag;
        }

        public static bool Always(this bool flowFlag, Action action)
        {
            action();
            
            return flowFlag;
        }

        public static bool True<T>(this bool flowFlag, T obj, Action<T> action)
        {
            if (flowFlag)
                action(obj);

            return flowFlag;
        }

        public static bool False<T>(this bool flowFlag, T obj, Action<T> action)
        {
            if (!flowFlag)
                action(obj);

            return flowFlag;
        }

        public static bool Always<T>(this bool flowFlag, T obj, Action<T> action)
        {
            action(obj);

            return flowFlag;
        }
    }
}