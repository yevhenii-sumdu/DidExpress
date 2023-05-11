using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidExpress {
    class Toy {
        public int Id { get; }
        public string Name { get; set; }
        public string Color { get; }
        public int Age { get; set; }
        public int Bag { get; set; }

        public Toy(int id, string name, string color, int age, int bag) {
            Id = id;
            Name = name;
            Color = color;
            Age = age;
            Bag = bag;
        }
    }
}
