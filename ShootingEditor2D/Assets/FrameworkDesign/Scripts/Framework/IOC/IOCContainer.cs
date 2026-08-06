using System;
using System.Collections.Generic;
namespace FrameworkDesign
{
    public class IOCContainer
    {
        private Dictionary<Type, object> mInstances = new Dictionary<Type, object>();
        public void Register<T>(T instance)
        {
            var key = typeof(T);
            if(mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else
            {
                mInstances.Add(key, instance);
            }
        }
        public T Get<T>() where T : class
        {
            var key = typeof(T);
            object retobj;
            if(mInstances.TryGetValue(key,out retobj))
            {
                return retobj as T;
            }
            return null;
        }
    }
}
