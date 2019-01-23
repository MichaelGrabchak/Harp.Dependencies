using System;
using System.Collections.Generic;

using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Harp.Dependencies
{
    public class Container
    {
        private static IUnityContainer ContainerInstance;

        static Container()
        {
            ContainerInstance = new UnityContainer();
        }

        public static object GetInstance()
        {
            return ContainerInstance;
        }

        public static void RegisterSingleton<TFrom, TTo>() where TTo : TFrom
        {
            ContainerInstance.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        public static void RegisterSingleton<TFrom, TTo>(params object[] constructorParams) where TTo : TFrom
        {
            ContainerInstance.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), new InjectionConstructor(constructorParams));
        }

        public static void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            ContainerInstance.RegisterType<TFrom, TTo>();
        }

        public static void RegisterType<TFrom, TTo>(params object[] constructorParams) where TTo : TFrom
        {
            ContainerInstance.RegisterType<TFrom, TTo>(new InjectionConstructor(constructorParams));
        }

        public static void RegisterFactory<T>(Func<T> factory)
        {
            ContainerInstance.RegisterType<T>(new InjectionFactory(c => factory()));
        }

        public static T Resolve<T>()
        {
            return ContainerInstance.Resolve<T>();
        }

        public static T Resolve<T>(Type type)
        {
            return (T)ContainerInstance.Resolve(type);
        }

        public static T Resolve<T>(Type type, string name, params KeyValuePair<string, object>[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var parameter in parameters)
            {
                overrides.Add(parameter.Key, parameter.Value);
            }

            return (T)ContainerInstance.Resolve(type, name, overrides);
        }

        public static T Resolve<T>(Type type, params KeyValuePair<string, object>[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var parameter in parameters)
            {
                overrides.Add(parameter.Key, parameter.Value);
            }

            return (T)ContainerInstance.Resolve(type, overrides);
        }

        public static T Resolve<T>(string name, params KeyValuePair<string, object>[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var parameter in parameters)
            {
                overrides.Add(parameter.Key, parameter.Value);
            }

            return ContainerInstance.Resolve<T>(name, overrides);
        }

        public static T Resolve<T>(params KeyValuePair<string, object>[] parameters)
        {
            var overrides = new ParameterOverrides();

            foreach (var parameter in parameters)
            {
                overrides.Add(parameter.Key, parameter.Value);
            }

            return ContainerInstance.Resolve<T>(overrides);
        }
    }
}
