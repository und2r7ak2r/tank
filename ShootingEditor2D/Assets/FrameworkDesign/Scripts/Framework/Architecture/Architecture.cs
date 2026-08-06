using System;
using System.Collections.Generic;
 
namespace FrameworkDesign
{
    public interface IArchitecture
    {
        void RegisterSystem<T>(T instance) where T : ISystem; 
        void RegisterModel<T>(T instance) where T : IModel;
        void RegisterUtility<T>(T instance);
        T GetSystem<T>() where T : class, ISystem;
        T GetModel<T>() where T : class, IModel;
        T GetUtility<T>() where T : class;
        void SendCommand<T>() where T : ICommand, new();
        void SendCommand<T>(T command) where T : ICommand;
        void SendEvent<T>(T e);
        void SendEvent<T>() where T : new();
        TResult SendQuery<TResult>(IQuery<TResult> query);
        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        void UnRegisterEvent<T>(Action<T> onEvent);
    }

    public abstract class Architecture<T>:IArchitecture where T : Architecture<T>, new()
    {
        private static T mArchitecture = null;
        private IOCContainer mContainer = new IOCContainer();
        private bool mInited = false;
        private List<IModel> mModels = new List<IModel>();
        private List<ISystem> mSystems = new List<ISystem>();

        public static IArchitecture Interface
        {
            get
            {
                if(mArchitecture == null)
                {
                    MakeSureArchitecture();
                }
                return mArchitecture;
            }
        }

        public void RegisterSystem<T>(T instance) where T : ISystem
        {
            instance.SetArchitecture(this);
            mContainer.Register<T>(instance);
            if (mInited)
            {
                instance.Init();
            }
            else
            {
                mSystems.Add(instance);
            }

        }
        static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.init();
                foreach (var architectureModel in mArchitecture.mModels)
                {
                    architectureModel.Init();
                }

                mArchitecture.mModels.Clear();
                foreach (var architectureSystem in mArchitecture.mSystems) 
                {
                    architectureSystem.Init();
                }

                mArchitecture.mSystems.Clear();
                mArchitecture.mInited = true;
            }
        }

        protected abstract void init();

        public static void Register<T>(T instance)
        {
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<T>(instance);
        }
        public void RegisterUtility<T>(T instance)
        {
            mContainer.Register<T>(instance);
        }

        public T GetUtility<T>() where T : class
        {
            return mContainer.Get<T>();
        }


        public void RegisterModel<T>(T instance) where T : IModel
        {
            instance.SetArchitecture (this);
            mContainer.Register<T>(instance);
            if(mInited)
            {
                instance.Init();
            }
            else
            {
                mModels.Add(instance);
            }
        }

        public T GetModel<T>() where T : class,IModel
        {
            return mContainer.Get<T>();
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
        }
        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return mContainer.Get<T>();
        }
        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public void SendEvent<T>(T e)
        {
            mTypeEventSystem.Send<T>(e);
        }

        public void SendEvent<T>() where T : new()
        {
            mTypeEventSystem.Send<T>();
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            mTypeEventSystem.UnRegister<T>(onEvent);
        }

        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }
    }
}
