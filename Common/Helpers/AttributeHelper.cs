using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class AttributeHelper
    {
        public static AttributeType GetAttribute<ClassType, AttributeType>(string fieldName) where AttributeType : class
        {
            try
            {
                return (AttributeType)(typeof(ClassType).GetMember(fieldName)[0].GetCustomAttributes(typeof(AttributeType), true)[0]);
            }
            catch
            {
                return null;
            }
        }
        public static AttributeType GetAttribute<ClassType, AttributeType>() where AttributeType : class
        {
            try
            {
                return (AttributeType)(typeof(ClassType).GetCustomAttributes(typeof(AttributeType), true)[0]);
            }
            catch
            {
                return null;
            }
        }
    }
}
