using System;

namespace TODO.Model.Common
{
    public class TableValueParam : Attribute
    {
        public string Name { get; }
        public int Order { get; set; }
        public TableValueParam(string name)
        {
            Name = name;
        }
    }
}
