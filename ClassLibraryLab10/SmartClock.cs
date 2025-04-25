using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class SmartClock : Clock
    {
        static int countSmartClock = 0;
        public static int GetcountSmartClock => countSmartClock;

        public string operatingSystem { get; set; }
        public bool pulseSensor { get; set; }

        static string[] OperatingSystem = { "Wear OS", "WatchOS", "Tizen", "Fitbit OS", "LiteOS" };
        static bool[] PulseSensor = { true, false };

        public SmartClock() : base()
        {
            brand = "NoBrand";
            year = 2000;
            operatingSystem = "NoNameOperatingSystem";
            pulseSensor = false;
            countSmartClock++;
        }

        public SmartClock(string brand, int year, string operatingSystem, bool pulseSensor, int num) : base(brand, year, num)
        {
            this.brand = brand;
            this.year = year;
            this.operatingSystem = operatingSystem;
            this.pulseSensor = pulseSensor;
            countSmartClock++;
        }

        public override void Show()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n " +
                              $"ОС: {operatingSystem}\n " +
                              $"Датчик измерения пульса {pulseSensor}");
        }

        public void ShowSmart()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n " +
                              $"ОС: {operatingSystem}\n " +
                              $"Датчик измерения пульса {pulseSensor}");
        }

        public override void Init()
        {
            string buffer;
            bool isChecked;

            Console.WriteLine("Введите бренд часов");
            brand = Console.ReadLine();

            Console.WriteLine("Введите ОС");
            operatingSystem = Console.ReadLine();

            do
            {
                Console.WriteLine("В часах есть датчик измерения пульса?");
                buffer = Console.ReadLine();
                if (buffer == "да" | buffer == "Да" | buffer == "есть" | buffer == "Есть")
                {
                    pulseSensor = true;
                    isChecked = true;
                }
                else if (buffer == "нет" | buffer == "Нет" | buffer == "Нету" | buffer == "нету")
                {
                    pulseSensor = false;
                    isChecked = true;
                }
                else
                {
                    isChecked = false;
                }
            } while (!isChecked);

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
            operatingSystem = OperatingSystem[rnd.Next(OperatingSystem.Length)];
            pulseSensor = PulseSensor[rnd.Next(PulseSensor.Length)];
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj is SmartClock clock)
            {
                return this.brand == clock.brand && this.year == clock.year
                    && this.operatingSystem == clock.operatingSystem
                    && this.pulseSensor == clock.pulseSensor;
            }
            else
            {
                return false;
            }
        }

        // Функция-запрос
        public static void CountOfSmartClocksWithPulseSensor(Clock[] clocks)
        {
            // Подсчитываем кол-во часов с измерителем пульса
            int countOfClocks = 0;
            foreach (Clock clock in clocks)
            {
                if (clock is SmartClock smartClock)
                {
                    if (smartClock.pulseSensor)
                    {
                        countOfClocks++;
                    }
                }
            }

            // Выводим ответ
            Console.WriteLine($"{countOfClocks} умных часов имеют измеритель пульса");
        }

        public virtual void IRandomInit()
        {
            base.IRandomInit();
            operatingSystem = OperatingSystem[rnd.Next(OperatingSystem.Length)];
            pulseSensor = PulseSensor[rnd.Next(PulseSensor.Length)];
        }

        public override string ToString()
        {
            return base.ToString() + "OC: " + operatingSystem + "\nДатчик измерения пульса: " + pulseSensor + "\n".ToString();
        }

    }
}