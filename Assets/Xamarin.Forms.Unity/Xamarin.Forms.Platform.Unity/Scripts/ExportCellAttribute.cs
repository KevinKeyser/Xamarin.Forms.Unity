using System;

namespace Xamarin.Forms.Platform.Unity
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ExportCellAttribute : HandlerAttribute
    {
        public ExportCellAttribute(Type handler, Type target) : base(handler, target) { }
    }
}