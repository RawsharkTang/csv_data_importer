// meta-name: CSV Data Template
// meta-description: Predefined CSV data template for use in Godot projects.
// meta-default: true
// meta-space-indent: 4

using _BINDINGS_NAMESPACE_;
using System;
using Godot;
using CSVDataImporter;

public partial class _CLASS_ : _BASE_, IIndexed
{

    [ColumnName("data")][Export] public int Data { get; set; }

}