using RedCorners.Forms.Services;

using System;
using System.Collections.Generic;

using Xamarin.Forms;

[assembly: Dependency(typeof(ContainerService))]
namespace RedCorners.Forms.Services
{

    public class ContainerService
    {
        public Dictionary<Type, object> Children { get; private set; } =
            new Dictionary<Type, object>();

        public T Get<T>() where T : class, new()
        {
            return Children.TryGetValue(typeof(T), out var vm) ? (T)vm : default;
        }

        public object Get(Type type)
        {
            return Children.TryGetValue(type, out var vm) ? vm : default;
        }

        public void Set<T>(T vm) where T : class, new()
        {
            Set(typeof(T), vm);
        }

        public void Set(object vm)
        {
            Set(vm.GetType(), vm);
        }

        public void Set(Type type, object vm)
        {
            Children[type] = vm;
        }

        public T GetOrCreate<T>() where T : class, new()
        {
            return GetOrCreate(() => Activator.CreateInstance<T>());
        }

        public T GetOrCreate<T>(Func<T> factory) where T : class, new()
        {
            return (T)GetOrCreate(typeof(T), () => (T)factory());
        }

        public object GetOrCreate(Type type)
        {
            return GetOrCreate(type, () => Activator.CreateInstance(type));
        }

        public object GetOrCreate(Type type, Func<object> factory)
        {
            var vm = Get(type);
            if (vm == default)
            {
                vm = factory();
                Set(vm);
            }
            return vm;
        }
    }
}