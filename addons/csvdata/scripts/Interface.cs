

using System;
using Godot;
using Godot.Collections;

namespace CSVDataImporter
{

    public interface IParsed
    {
        public void Parse(Dictionary data);
    }

    public interface IIndexed
    {
        public StringName ID { get; set; }
    }
    public interface IIndexed<T> where T : Enum
    {
        public T Key { get; set; }

    }

    public interface IGroupPared
    {
        public void Parse(Dictionary[] data);
    }
}