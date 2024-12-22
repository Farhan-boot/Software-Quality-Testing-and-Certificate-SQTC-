using System;
using System.Data;

namespace PTSL.GENERIC.Api.Helpers;

public static class DataRowHelper
{
    public static long GetLongValue(DataRow row, string key)
    {
        var type = row.Table.Columns[key]?.DataType;

        if (type == typeof(string))
        {
            var stringValue = row.Field<string>(key);
            _ = long.TryParse(stringValue, out var longValue);

            return longValue;
        }

        if (type == typeof(double))
        {
            var doubleValue = row.Field<double?>(key) ?? 0;
            if (double.IsNaN(doubleValue) == false && double.IsInfinity(doubleValue) == false)
            {
                _ = long.TryParse(doubleValue.ToString(), out var longValue);

                return longValue;
            }
        }

        if (type == typeof(long))
        {
            var longValue = row.Field<long?>(key) ?? 0;
            return longValue;
        }

        return 0;
    }

    public static double GetDoubleValue(DataRow row, string key)
    {
        var type = row.Table.Columns[key]?.DataType;

        if (type == typeof(string))
        {
            var stringValue = row.Field<string?>(key);
            if (string.IsNullOrEmpty(stringValue))
            {
                return 0;
            }

            _ = double.TryParse(stringValue, out var doubleValue);

            return doubleValue;
        }

        if (type == typeof(double))
        {
            var doubleValue = row.Field<double?>(key) ?? 0;
            if (double.IsNaN(doubleValue) == false && double.IsInfinity(doubleValue) == false)
            {
                return doubleValue;
            }
        }

        return 0;
    }

    public static string GetStringValue(DataRow row, string key)
    {
        var type = row.Table.Columns[key]?.DataType;

        if (type == typeof(string))
        {
            return row.Field<string>(key);
        }

        if (type == typeof(double))
        {
            var doubleValue = row.Field<double?>(key) ?? 0;
            if (double.IsNaN(doubleValue) == false && double.IsInfinity(doubleValue) == false)
            {
                return doubleValue.ToString();
            }
        }

        return string.Empty;
    }

    public static int GetIntValue(DataRow row, string key)
    {
        var type = row.Table.Columns[key]?.DataType;

        if (type == typeof(string))
        {
            var stringValue = row.Field<string>(key);
            _ = int.TryParse(stringValue, out var doubleValue);

            return doubleValue;
        }

        if (type == typeof(double))
        {
            var doubleValue = row.Field<double?>(key) ?? 0;
            if (double.IsNaN(doubleValue) == false && double.IsInfinity(doubleValue) == false)
            {
                return (int)doubleValue;
            }
        }

        return 0;
    }

    public static DateTime? GetDateTimeValue(DataRow row, string key)
    {
        var type = row.Table.Columns[key]?.DataType;

        if (type == typeof(string))
        {
            var stringValue = row.Field<string>(key);
            if (string.IsNullOrEmpty(stringValue))
            {
                return null;
            }

            _ = DateTime.TryParse(stringValue, out var dateTimeValue);

            return dateTimeValue;
        }

        if (type == typeof(DateTime))
        {
            return row.Field<DateTime?>(key);
        }

        return null;
    }
}
