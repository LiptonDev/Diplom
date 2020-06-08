using System;

namespace Diplom
{
    /// <summary>
    /// Указывает, что св-во нужно пропустить при работе метода ChangeAllFields.SetAllFields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ChangeIgnoreAttribute : Attribute
    {
    }
}
