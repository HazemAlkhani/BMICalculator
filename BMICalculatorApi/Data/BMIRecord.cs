using System;

namespace BMICalculatorApi.Data
{
    public class BMIRecord
    {
        public BMIRecord() { }

        public BMIRecord(string name, double height, double weight)
        {
            Name = name;
            Height = height;
            Weight = weight;
            Bmi = CalculateBmi(height, weight);
            RecordedDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Bmi { get; set; }
        public DateTime RecordedDate { get; set; }

        private double CalculateBmi(double height, double weight)
        {
            return weight / (height * height);
        }
    }
}