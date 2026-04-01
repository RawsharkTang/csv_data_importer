using System;
namespace CSVDataImporter
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ColumnNameAttribute : Attribute
	{
		public string ColumnName;

		public ColumnNameAttribute(string name)
		{
			ColumnName = name;
		}
	}
}