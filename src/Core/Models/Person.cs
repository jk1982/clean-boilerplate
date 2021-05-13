using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
