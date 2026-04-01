
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace CSVDataImporter
{

	public static class Extensions
	{

		public static void SetPropertyValueFromVariant<T>(T row, System.Reflection.PropertyInfo colmun_property, Variant value)
		{
			var propType = colmun_property.PropertyType;
			if (propType == typeof(int))
			{
				colmun_property.SetValue(row, value.AsInt16());
			}
			else if (propType.IsEnum)
			{
				var enum_name = value.AsString();
				if (!string.IsNullOrEmpty(enum_name) && Enum.TryParse(propType, enum_name, out var enumValue))
				{
					colmun_property.SetValue(row, enumValue);
				}
			}
			else if (propType == typeof(bool))
			{
				colmun_property.SetValue(row, value.AsBool());
			}
			else if (propType == typeof(string))
			{
				colmun_property.SetValue(row, value.AsString());
			}
			else if (propType == typeof(StringName))
			{
				colmun_property.SetValue(row, value.AsStringName());
			}
			else if (propType == typeof(float))
			{
				colmun_property.SetValue(row, value.AsSingle());
			}
			else
			{
				colmun_property.SetValue(row, value);
			}
		}

		public static T[] ParseAllRows<T>(this CSVData csv_data) where T : IIndexed, new()
		{
			var rows = new List<T>();
			foreach (var entry in csv_data)
			{
				var row = new T()
				{
					ID = entry.Key
				};
				var cols = entry.Value;
				var columnProperties = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ColumnNameAttribute)));
				foreach (var colmun_property in columnProperties)
				{
					if (colmun_property.GetCustomAttributes(typeof(ColumnNameAttribute), false).FirstOrDefault() is ColumnNameAttribute attr)
					{
						var columnName = attr.ColumnName;
						if (cols.TryGetValue(columnName, out var value))
						{
							SetPropertyValueFromVariant(row, colmun_property, value);
						}
					}
				}
				rows.Add(row);
			}
			return rows.ToArray();
		}

		public static T1 ParseRow<T1, T2>(this CSVData csv_data, T2 key) where T1 : IIndexed<T2>, new() where T2 : Enum
		{
			var id = Enum.GetName(typeof(T2), key);
			if (!string.IsNullOrEmpty(id) && csv_data._data.TryGetValue(id, out var cols))
			{
				var row = new T1()
				{
					Key = key
				};
				var columnProperties = typeof(T1).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ColumnNameAttribute)));
				foreach (var colmun_property in columnProperties)
				{
					if (colmun_property.GetCustomAttributes(typeof(ColumnNameAttribute), false).FirstOrDefault() is ColumnNameAttribute attr)
					{
						var columnName = attr.ColumnName;
						if (cols.TryGetValue(columnName, out var value))
						{
							SetPropertyValueFromVariant(row, colmun_property, value);
						}
					}
				}
				return row;
			}
			else
			{
				throw new KeyNotFoundException($"Key '{key}' not found in CSVData.");
			}
		}

		public static T ParseRow<T>(this CSVData csv_data, StringName id) where T : IIndexed, new()
		{
			if (csv_data._data.TryGetValue(id, out var cols))
			{
				var row = new T()
				{
					ID = id
				};
				var columnProperties = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ColumnNameAttribute)));
				foreach (var colmun_property in columnProperties)
				{
					if (colmun_property.GetCustomAttributes(typeof(ColumnNameAttribute), false).FirstOrDefault() is ColumnNameAttribute attr)
					{
						var columnName = attr.ColumnName;
						if (cols.TryGetValue(columnName, out var value))
						{
							SetPropertyValueFromVariant(row, colmun_property, value);
						}
					}
				}
				return row;
			}
			else
			{
				throw new KeyNotFoundException($"Key '{id}' not found in CSVData.");
			}
		}
	}

}