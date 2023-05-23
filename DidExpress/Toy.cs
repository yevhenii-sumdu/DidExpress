namespace DidExpress {
    public class Toy {
        public int Id { get; }
        public string Name { get; }
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
