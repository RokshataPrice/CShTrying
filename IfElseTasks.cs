using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

//TODO: юнит-тестирование
//TODO: тестирование должно быть DDT

//В рамках тестирования проверить необходимость обёртки, содержащей try catch, для каждого метода, потенциально генерирующего исключения 

namespace ConsoleApplication1
{
    public class IfElseTasks
    {
//===================================================================================================================
        //год является високосным, если он делится на 4, но при этом не делится на 100
        public bool IsLeapYear(int year)
        {
            if (year <= 0)
                throw new ArgumentException("Значение года должно быть больше 0: рассматриваем только нашу эру");
            if (year % 4 == 0 && year % 100 != 0)
                return true;
            else
                return false;
        }

        //возвращает количество дней в месяце
        private int MonthDaysCount(int month, int year)
        {
            switch (month)
            {
                case 1: return 31;
                case 2: return IsLeapYear(year) ? 29 : 28;
                case 3: return 31;
                case 4: return 30;
                case 5: return 31;
                case 6: return 30;
                case 7: return 31;
                case 8: return 31;
                case 9: return 30;
                case 10: return 31;
                case 11: return 30;
                case 12: return 31;
                default: throw new System.ArgumentException("Номер месяца должен быть в диапазоне от 1 до 12");
            }
        }

        //вычисляет по текущей дате следующую 
        //Вопрос к себе: обработку исключения тут как-то добавить? Или во внещней функции? 
        //ХЗ, как тут добавить обработку: если происходит исключение, то нельзя возвращать дату.
        //Хех, а вот и этот великолепный случай, где понадобилась бы монада Maybe из хаскеля
        public DateTime NextDate(DateTime date)
        {
            int year, month, day;
            year = date.Year;
            month = date.Month;
            day = date.Day;

            if (day == MonthDaysCount(month, year))
            {
                day = 1;
                if (month == 12)
                {
                    month = 1;
                    year += 1;
                }
                else month += 1;
            }
            else day += 1;

            return new DateTime(year, month, day);
        }

        //по номеру дня недели выводит сообщение, является ли день рабочим или это суббота или воскресенье;
        //Коммент: не считаю, что это нужно делать через IF. Switch здесь подходит больше, если выводить подробности о рабочих днях недели
        public string WhatIsADayOfWeek(int dayOfWeek)
        {
            string info = "";
            switch (dayOfWeek)
            {
                case 1: { info = "Рабочий деь. Понедельник."; break; }
                case 2: { info = "Рабочий деь. Вторник."; break; }
                case 3: { info = "Рабочий деь. Среда."; break; }
                case 4: { info = "Рабочий деь. Четверг."; break; }
                case 5: { info = "Рабочий деь. Пятница."; break; }
                case 6: { info = "Выходной деь. Суббота."; break; }
                case 7: { info = "Выходной деь. Воскресенье."; break; }
                default: throw new System.ArgumentException("Номер дня недели должен находиться в диапазоне от 1 до 7");
            }
            return info+"\n";
        }

//===================================================================================================================
        //вычислить оптимальную для пользователя массу. Сравнить ее
        //с реальной массой и вывести в консоль рекомендацию поправиться
        //или похудеть на определенное количество килограммов.Расчет оптимальной
        //массы производится по формуле: рост(см)–100. Например:
        //Ваш рост(см) 175
        //Ваш вес(кг): 95
        //Вам необходимо похудеть на 20 кг;
        public string CalcOptimalMass(int mass, int height)
        {
            int minmass = 10, maxmass = 450;
            int minheight = 80, maxheight = 300;
            if (mass < minmass || mass > maxmass)
                throw new ArgumentException("Ваша масса должна быть в диапазоне от "+minmass.ToString()+" до "+maxmass.ToString()+" кг");
            else if (height < minheight || height > maxheight)
                throw new ArgumentException("Ваш рост должен быть в диапазоне от "+minheight.ToString()+" до "+maxheight.ToString()+" см");
            else
            {
                string info = "";
                int optmass = height - 100;
                int difference = optmass - mass;
                if (difference < 0)
                    info = "Вам нужно сбросить " + (-difference).ToString();
                else if (difference == 0)
                    info = "Ваш вес оптимален";
                else 
                    info = "Вам нужно набрать " + difference.ToString();
                return info+"\n";
            }
        }

//===================================================================================================================
        //вычислить стоимость разговора по телефону с учетом стоимости
        //одной минуты 3 р.и 15% скидки, которая предоставляется по выходным.
        //Например:
        //Телефонный разговор.
        //Количество минут разговора (целое количество минут): 5
        //День недели(1 — понедельник, ..., 7 — воскресенье): 3
        //Скидка не предоставляется.
        //Стоимость разговора: 15 р.;
        public double CalculateCallPrice(int minutescount, int dayofweek)
        {
            int minuteprice = 3;
            int discount = 15;
            if (minutescount <= 0)
                throw new ArgumentException("Количество минут должно быть положительным числом");
            if (dayofweek < 1 || dayofweek > 7)
                throw new ArgumentException("Номер дня недели должен быть в диапазоне от 1 до 7");
            int pricewithoutdiscount = 3 * minutescount;
            if (dayofweek < 6)
                return pricewithoutdiscount;
            else
                return pricewithoutdiscount * 0.85;
        }

//===================================================================================================================
        //написать программу, которая находит сумму двух данных чисел
        //(если оба числа четные) или произведение(если хотя бы одно из чисел нечетное);
        public int SummOrMultForEvensAndOdds(int num1, int num2)
        {
            if (num1 % 2 == 0 && num2 % 2 == 0)
                return num1 + num2;
            else
                return num1 * num2;
        }

//===================================================================================================================
        //написать программу, которая переводит время, указанное в секундах,
        //в минуты и секунды.Например:
        //Укажите время в секундах: 380
        //380 с = 6 мин 20 с
        //Укажите время в секундах: 12
        //12 с = 0 мин 12 с;
        public void SecondsToMinutesAndSeconds(int secondscount, out int minutes, out int seconds)
        {
            if (secondscount < 0)
                throw new ArgumentException("Количетсво секунд должно быть положительным числом");
            minutes = secondscount / 60;
            seconds = secondscount % 60;
        }

//===================================================================================================================
        //написать программу решения квадратного уравнения (коэффициент
        //при второй степени переменной считать отличным от нуля).
        //В случае, когда дискриминант меньше нуля, вывести соответствующее сообщение;
        private float Discriminant(float a, float b,  float c)
        {
            if (a == 0.0)
                throw new ArgumentException("Коэффициент при 2й степени должен быть отличным от нуля");
            return b * b - 4 * a * c;
        }

        private Pair<float, float> QuadraticEquation(float a, float b, float c)
        {
            float d = Discriminant(a, b, c);
            if (d < 0)
                return null;
            float x1 = (-b + (float)Math.Sqrt(d)) / 2 / a;
            float x2;
            if (d == 0)
                x2 = x1;
            else
                x2 = (-b - (float)Math.Sqrt(d)) / 2 / a;
            return new Pair<float, float>(x1, x2);
        }

        public string QuadraticEquationInfo(float a, float b, float c)
        {
            Pair<float, float> pair = QuadraticEquation(a, b, c);
            if (pair == null)
                return "Дискрименант меньше 0";
            else
                return "Корни уравнения: " + pair.ToString();
        }

//===================================================================================================================
        //написать программу сложения двух обыкновенных дробей
        //(числители и знаменатели дробей — параметры ввода). Предусмотреть
        //случай, когда знаменатель дроби равен нулю;
        private int GCD(int a, int b)
        {
            return (b!=0 ? GCD(b, a % b) : a);
        }

        public Pair<int, int> FractionsSumm(int numerator1, int denominator1, int numerator2, int denominator2)
        {
            if (denominator1 * denominator2 == 0)
                throw new ArgumentException("Заменатель должен быть отличным от нуля");

            int gcd = GCD(denominator1, denominator2);
            int newdenominator = denominator1 * denominator2 / gcd, newnumerator;
            newnumerator = numerator1 * newdenominator/denominator1 + numerator2 * newdenominator/denominator2 ;

            return new Pair<int, int>(newnumerator, newdenominator);
        }

//===================================================================================================================
        //написать программу умножения двух обыкновенных дробей (числители и знаменатели дробей — параметры ввода). 
        //Предусмотреть случай, когда знаменатель дроби равен нулю;
        public Pair<int, int> FractionsMult(int numerator1, int denominator1, int numerator2, int denominator2)
        {
            if (denominator1 * denominator2 == 0)
                throw new ArgumentException("Заменатель должен быть отличным от нуля");
            int newdenominator = denominator1 * denominator2, newnumerator = numerator1 *numerator2;

            int gcd = GCD(newnumerator, newdenominator);
            newnumerator = newnumerator / gcd;
            newdenominator = newdenominator / gcd;

            return new Pair<int, int>(newnumerator, newdenominator);
        }

//===================================================================================================================
        //написать программу, которая проверяет, является ли введенное пользователем число четным;
        public bool Even(int num)
        {
            return num % 2 == 0;
        }

//===================================================================================================================
        //написать программу, которая переводит время, указанное в минутах, в часы и минуты.Например:
        //Укажите время в минутах: 126
        //126 мин = 2 ч 6 мин
        //Укажите время в минутах: 15
        //15 мин = 0 ч 15 мин;
        public void MinutesToHoursAndMinutes(int minutesount, out int hours, out int minutes)
        {
            if (minutesount < 0)
                throw new ArgumentException("Количетсво минут должно быть положительным числом");
            hours = minutesount / 60;
            minutes = minutesount % 60;
        }

//===================================================================================================================
        //переведите расстояние (указанное в метрах) в километры и метры.Например:
        //Укажите расстояние в метрах: 3640
        //3640 м = 3 км 640 м
        //Укажите расстояние в метрах: 70
        //70 м = 0 км 70 м;
        public void MetersToKilometersAndMeters(int meterscount, out int kilometers, out int meters)
        {
            if (meterscount < 0)
                throw new ArgumentException("Количетсво метров должно быть положительным числом");
            kilometers = meterscount / 1000;
            meters = meterscount % 1000;
        }

//===================================================================================================================
        //запросить у пользователя номер текущего месяца. Ввести с клавиатуры в консоль соответствующее название времени года.
        //Если введенное значение большее 12, в консоль вывести ошибку «Введите корректный номер (от 1 до 12)»;

        private string GetSeasonByMonth(int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Номер месяца должен быть в диапазоне от 1 до 12");
            if (month < 4)
                return "Зима";
            if (month < 7)
                return "Весна";
            if (month < 10)
                return "Лето";
            else
                return "Осень";
        }

        public void SeasonInfoByUserMonth()
        {
            const int tries = 5;
            int month;
            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Введите номер месяца: число от 1 до 12");
                if (int.TryParse(Console.ReadLine(), out month))
                {
                    try
                    {
                        Console.WriteLine(GetSeasonByMonth(month));
                        return;
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Введено неверное значение: {0}", e.Source);
                    }
                }
                else Console.WriteLine("Введено некорректное значение: вводимое вами значение должно быть целым числом в диапазоне от 1 до 12");
                Console.WriteLine("Осталось {0} попыток", tries - 1 - i);
            }
            Console.WriteLine("Количество попыток исчерпано");
        }

//===================================================================================================================
        //написать программу, реализующую простейший тест. Программа должна вывести вопрос и три варианта ответа, один из которых
        //правильный.Пользователь вводит номер варианта, после чего программа сообщает: «Вы ответили правильно» или «Вы ошиблись»;
        public void SimpleTest()
        {
            string question = "Как звали няню Пушкина?";
            string[] answers = {
                "Ирина Лареоновна",
                "Арина Радионовна",
                "Выпьем, няня!"
            };
            int rightanswer = 2; // с точки зрения пользователя
            int useranswer;
            const int tries = 5;

            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine(question);
                for (int j = 0; j < answers.Length; j++)
                    Console.WriteLine("{0}: {1}", j + 1, answers[j]);
                if (int.TryParse(Console.ReadLine(), out useranswer))
                {
                    if (useranswer == rightanswer)
                        Console.WriteLine("Верный ответ!");
                    else
                        Console.WriteLine("К сожалению, ответ неверный.");
                }
                else Console.WriteLine("Введено некорректное значение: вводимое вами значение должно быть целым числом в диапазоне от 1 до " + answers.Length.ToString());
                Console.WriteLine("Осталось {0} попыток", tries - 1 - i);
            }
            Console.WriteLine("Количество попыток исчерпано");
        }

//===================================================================================================================
        //сравнить два введенных с клавиатуры числа. Необходимо определить, какое число меньше.Если числа равны, вывести в консоль
        //сообщение: «Числа равны»;
        public void CompareTwoNumbers()
        {
            int num1, num2;
            const int tries = 5;
            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Введите два целых числа");
                if (int.TryParse(Console.ReadLine(), out num1))
                {
                    if (int.TryParse(Console.ReadLine(), out num2))
                    {
                        string mess = "Меньшее число: {0}";
                        if (num1 < num2)
                            Console.WriteLine(mess, num1);
                        else if (num2 < num1)
                            Console.WriteLine(mess, num2);
                        else
                            Console.WriteLine("Числа равны");


                    }
                    else Console.WriteLine("Введено некорректное значение: вводимое вами значение должно быть целым числом");
                }
                else Console.WriteLine("Введено некорректное значение: вводимое вами значение должно быть целым числом");
                Console.WriteLine("Осталось {0} попыток", tries - 1 - i);
            }
            Console.WriteLine("Количество попыток исчерпано");
        }
        
//===================================================================================================================
        //Написать программу деления двух обыкновенных дробей (числители и знаменатели дробей — параметры ввода). 
        //Предусмотреть случай, когда знаменатель дроби равен нулю.

        //Первый УПОРОТЫЙ (ТАК ДЕЛАТЬ НЕЛЬЗЯ) вариант с регулярками и сплитом, чтоб вспомнить работу с регулярками и сплитом
        private bool StringIsFraction(string str)
        {
            return Regex.IsMatch(str, @"\d+/\d+", RegexOptions.None);
        }

        public void BadFractionsDivision()
        {
            int tries = 5;
            string frac1, frac2;
            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Пожалуйста, введите две дроби. Формат записи \"число/число\". После ввода кажждой дроби нажмите Enter.");
                try
                {
                    frac1 = Console.ReadLine();
                    if (StringIsFraction(frac1))
                    {
                        frac2 = Console.ReadLine();
                        if (StringIsFraction(frac2))
                        {
                            string [] fracparts1 = frac1.Split('/');
                            string [] fracparts2 = frac2.Split('/');
                            try
                            {
                                Pair<int, int> divfrac = FractionsMult(int.Parse(fracparts1[0]), int.Parse(fracparts1[1]), int.Parse(fracparts2[1]), int.Parse(fracparts2[0]));
                                Console.WriteLine("Результат деления дробей: {0}/{1}", divfrac.First, divfrac.Second);
                            }
                            catch {
                                Console.WriteLine("Произошла ошибка парсинга строки в целое число");
                            }
                        }
                        else
                            Console.WriteLine("Введено неверное значение.");
                    }
                    else
                        Console.WriteLine("Введено неверное значение.");
                }
                catch {
                    Console.WriteLine("Произошла ошибка проверки валидности введённых значений");
                }
            }
            Console.WriteLine("Количество попыток исчерпано");
        }

        public void FractionsDivision()
        {

        }

    }
}
