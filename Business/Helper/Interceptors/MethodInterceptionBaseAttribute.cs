using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Business.Helper.Interceptors
{
    public abstract class MethodInterceptionBaseAttribute : Attribute, Castle.DynamicProxy.IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invoation)
        {

        }
    }
}
