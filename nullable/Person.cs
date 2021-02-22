using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullable
{
    public class Person
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string Name { get; set; }
        public string? Nickname { get; set; }
    }
}
