using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class AnalogClock : Clock
    {
        static int countAnalogClock = 0;
        public static int GetcountAnalogClock => countAnalogClock;

        public string clockStyle { get; set; }

        static string[] ClockStyles = { "Часы без стрелок", "Часы с минимальным набором стрелок",
            "Часы с двойным циферблатом", "Часы с прозрачным циферблатом", "Часы в стиле милитари" };

        public AnalogClock() : base()
        {
            brand = "NoBrand";
            year = 2000;
            clockStyle = "TheUsualStyle";
            countAnalogClock++;
        }

        public AnalogClock(string brand, int year, string clockStyle, int num) : base(brand, year, num)
        {
            this.brand = brand;
            this.year = year;
            this.clockStyle=clockStyle;
            countAnalogClock++;
        }

        public override void Show()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n " +
                              $"Стиль часов: {clockStyle}\n");
        }

        public void ShowAnalog()
        {
            Console.WriteLine($"Бренд часов: {brand}\n " +
                              $"Год выпуска: {year}\n " +
                              $"Стиль часов: {clockStyle}\n");
        }

        public override void Init()
        {
            string buffer;
            bool isChecked;

            Console.WriteLine("Введите бренд часов");
            brand = Console.ReadLine();

            Console.WriteLine("Введите тип дисплея");
            clockStyle = Console.ReadLine();

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
            clockStyle = ClockStyles[rnd.Next(ClockStyles.Length)];
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj is AnalogClock clock)
            {
                return this.brand == clock.brand && this.year == clock.year && this.clockStyle == clock.clockStyle;
            }
            else
            {
                return false;
            }
        }

        // Функция-запрос
        public static void UniqueStyle(Clock[] clocks)
        {
            // Массив для хранения уникальных стилей
            string[] uniqueStyles = new string[clocks.Length];
            int uniqueCount = 0;  // Счётчик уникальных стилей

            foreach (Clock clock in clocks)
            {
                if (clock is AnalogClock analogClock)
                {
                    // Проверяем, есть ли уже этот стиль в списке уникальных
                    bool isUnique = true;
                    for (int i = 0; i < uniqueCount; i++)
                    {
                        if (uniqueStyles[i] == analogClock.clockStyle)
                        {
                            isUnique = false;
                            break;
                        }
                    }

                    // Если стиль уникален, добавляем его в массив
                    if (isUnique)
                    {
                        uniqueStyles[uniqueCount] = analogClock.clockStyle;
                        uniqueCount++;
                    }
                }
            }

            // Выводим уникальные стили
            Console.WriteLine("Уникальные стили часов:");
            for (int i = 0; i < uniqueCount; i++)
            {
                Console.WriteLine(uniqueStyles[i]);
            }
        }

        public virtual void IRandomInit()
        {
            base.IRandomInit();
            clockStyle = ClockStyles[rnd.Next(ClockStyles.Length)];
        }

        public override string ToString()
        {
            return base.ToString() + "\nСтиль часов: " + clockStyle + "\n".ToString();
        }
    }
}