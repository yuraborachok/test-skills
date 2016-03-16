namespace TC.SkillsDatabase.Core.Utils
{
    using System;
    using System.Collections.Generic;

    public static class TypeUtil
    {
        public static void ShallowConvert<TParent, TChild>(this TParent parent, TChild child)
        {
            foreach (var property in parent.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(parent, null), null);
                }
            }
        }

        public static void ShallowConvert<TParent, TChild>(this TParent parent, TChild child, IList<string> excludedProperties)
        {
            foreach (var property in parent.GetType().GetProperties())
            {
                if (!excludedProperties.Contains(property.Name) && property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(parent, null), null);
                }
            }
        }
    }
}
