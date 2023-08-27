using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace DataAccess.General
{
    public static class GenericMethods
    {
        public static SqlParameter CreateParameter(string parameterName, object valueParameter)
        {
            SqlParameter parameter = new SqlParameter()
            {
                ParameterName = parameterName
            };

            if (valueParameter != null)
                parameter.Value = valueParameter;
            else
                parameter.Value = DBNull.Value;

            return parameter;
        }

        public static object SetDefaultParameterValue(object element)
        {
            if (element != null)
            {
                var test = element.GetType();
                switch (Type.GetTypeCode(element.GetType()))
                {
                    case TypeCode.Int32:
                        return (Convert.ToInt32(element) > 0 ? element : null);
                    case TypeCode.Decimal:
                        return (Convert.ToDecimal(element) > 0 ? element : null);
                    case TypeCode.String:
                        return (!string.IsNullOrEmpty(element.ToString()) ? element.ToString() : null);
                    case TypeCode.DateTime:
                        return (Convert.ToDateTime(element) != null && Convert.ToDateTime(element) != DateTime.MinValue ? element : null);
                    case TypeCode.Boolean:
                        return element;
                    default:
                        return element;
                }
            }
            else
                return null;
        }
    }

}
