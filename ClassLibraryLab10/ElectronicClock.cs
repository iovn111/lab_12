using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class ElectronicClock : Clock
    {
        static int countElectronicClock = 0;
        public static int GetcountElectronicClock => countElectronicClock;

        // Вводим поле данного класса
        public string displayType { get; set; }

        static string[] DisplayType = { "Liquid Crystal Display", "Organic Light Emitting Diode",
            "Active Matrix Organic Light Emitting Diode", "E-Ink", "Thin Film Transistor"};

        // Прописываем конструкторы 
        public ElectronicClock() : base()
        {
            brand = "NoBrend";
            year = 2000;
            displayType = "UnknownType";
            countElectronicClock++;
        }

        public ElectronicClock(string brand, int year, string displatType, int num) : base(brand, year, num)
        {
            this.brand = brand;
            this.year = year;
            this.displayType = displatType;
            countElectronicClock++;
        }

        public override void Show()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n " +
                              $"Тип дисплея: {displayType}\n");
        }

        public void ShowElectronic()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n " +
                              $"Тип дисплея: {displayType}\n");
        }

        public override void Init()
        {
            string buffer;
            bool isChecked;

            Console.WriteLine("Введите бренд часов");
            brand = Console.ReadLine();

            Console.WriteLine("Введите тип дисплея");
            displayType = Console.ReadLine();

            do
            {
                Console.WriteLine("Введите год выпуска часов");
                buffer = Console.ReadLine();
                isChecked = int.TryParse(buffer, out Year);
            } while (!isChecked);
        }

        public override void RandomInit()
        {
            brand = Brand[rnd.Next(Brand.Length)];
            year = YearRelease[rnd.Next(YearRelease.Length)];
            displayType = DisplayType[rnd.Next(DisplayType.Length)];
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj is ElectronicClock clock)
            {
                return this.brand == clock.brand && this.year == clock.year && this.displayType == clock.displayType;
            }
            else
            {
                return false;
            }
        }

        public virtual void IRandomInit()
        {
            base.IRandomInit();
            displayType = DisplayType[rnd.Next(DisplayType.Length)];
        }

        public override string ToString()
        {
            return base.ToString() + "Тип дисплея: " + displayType + "\n".ToString();
        }

    }
}