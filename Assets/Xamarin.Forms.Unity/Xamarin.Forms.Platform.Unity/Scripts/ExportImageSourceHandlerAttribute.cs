using System;

namespace Xamarin.Forms.Platform.Unity
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ExportImageSourceHandlerAttribute : HandlerAttribute
    {
        public ExportImageSourceHandlerAttribute(Type handler, Type target) : base(handler, target) { }
    }
}