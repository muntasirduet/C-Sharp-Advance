using System;

namespace C_AdvanceLearning
{
    public abstract class Car
    {
        public int id { get; set; }
        public string Name { get; set; }
        public virtual string Start()
        {
            return $"{id} - {Name}";
        }
    }

    public class ElectricCar : Car
    {
        public override string Start()
        {
            return $"{base.Start()} is starting silently";
        }
    }
    public class GasCar : Car
    {
        public override string Start()
        {
            return $"{base.Start()} is starting with a roar";
        }
    }

    public static class CarFactory
    {
        public static Car ReturnGasCar(int id, string name)
        {
            return new GasCar { id = id, Name = name };
        }
        public static Car ReturnElectricCar(int id, string name)
        {
            return new ElectricCar { id = id, Name = name };
        }
    }
    class Program
    {
        delegate Car CarFactoryDel(int id, string name);
        static void Main(string[] args)
        {
            CarFactoryDel carFactory = CarFactory.ReturnGasCar;
            Car car = carFactory(1, "Toyota");
            Console.WriteLine(car.Start());

            CarFactoryDel carFactory2 = CarFactory.ReturnElectricCar;
            Car car2 = carFactory2(2, "Tesla");
            Console.WriteLine(car2.Start());
        }
    }
}
