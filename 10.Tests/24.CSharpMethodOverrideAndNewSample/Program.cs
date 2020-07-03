using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMethodOverrideAndNewSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCars1();
            TesStatic1();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public static void TestCars1()
        {
            System.Console.WriteLine("\nTestCars1");
            System.Console.WriteLine("----------");

            Car car1 = new Car();
            car1.DescribeCar();
            System.Console.WriteLine("----------");

            // Notice the output from this test case. The new modifier is  
            // used in the definition of ShowDetails in the ConvertibleCar  
            // class.
            ConvertibleCar car2 = new ConvertibleCar();
            car2.DescribeCar();
            System.Console.WriteLine("----------");

            Minivan car3 = new Minivan();
            car3.DescribeCar();
            System.Console.WriteLine("----------");
        }

        public static void TesStatic1()
        {
            System.Console.WriteLine("\nStatic method new keyword");
            A.Print();
            B.Print();
        }
    }

    // Define the base class, Car. The class defines two virtual methods,  
    // DescribeCar and ShowDetails. DescribeCar calls ShowDetails, and each derived  
    // class also defines a ShowDetails method. The example tests which version of  
    // ShowDetails is used, the base class method or the derived class method.  
    class Car
    {
        public virtual void DescribeCar()
        {
            System.Console.WriteLine("Car: Four wheels and an engine.");
            this.ShowDetails();
        }

        public virtual void ShowDetails()
        {
            System.Console.WriteLine("Car: Standard transportation.");
        }
    }

    // Define the derived classes.  

    // Class ConvertibleCar uses the new modifier to acknowledge that ShowDetails  
    // hides the base class method.  
    class ConvertibleCar : Car
    {
        public new void ShowDetails()
        {
            System.Console.WriteLine("ConvertibleCar: A roof that opens up.");
        }
    }

    // Class Minivan uses the override modifier to specify that ShowDetails  
    // extends the base class method.  
    class Minivan : Car
    {
        public override void ShowDetails()
        {
            System.Console.WriteLine("Minivan: Carries seven people.");
        }
    }

    class A
    {
        public static void Print() { System.Console.WriteLine("A.Print"); }
    }

    class B : A
    {
        public static new void Print() { System.Console.WriteLine("B.Print"); }
        /*
        public static void CallPrint()
        {
            Print();    // Will call B.Print
            A.Print();  // Will call hidden static method on parent
        }
        */
    }
}
