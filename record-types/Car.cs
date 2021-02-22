namespace record_types
{
    public record Car
    {
        public Car(string color, string model, int horsepower)
        {
            Color = color;
            Model = model;
            Horsepower = horsepower;
        }

        public string Color { get; }

        public string Model { get; }

        public int Horsepower { get; }
    }

    //public record Car(string Color, string Model, int Horsepower);
}
