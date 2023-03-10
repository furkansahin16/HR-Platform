using Castle.DynamicProxy;
using System.Reflection;

namespace Business.Helper.Interceptors
{
    internal class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            return (IInterceptor[])classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
