using System;
using System.IO;

// Обработка данных сложной структуры
// Заданы структура данных и требуемый результат
// Решить поставленную задачу
// Описать структуру данных средствами языка программирования (массив или список), организовать ввод данных и решение задачи
// В первой программе создать файл для представления данных
// Во второй программе решить поставленную задачу при условии, что все данные одновременно во внутреннюю память не поместятся
// Использовать для представления данных стандартный класс, предварительно описав данные структурой или классом
// Обеспечить загрузку данных в класс и решение задачи
// Решить поставленную задачу с использованием стандартных алгоритмов
// Фирма - подразделение - прибыль/убыток за последний месяц (+ или-)
// Каких подразделений больше - прибыльных или убыточных?
// Департамент с наибольшим профицитом

namespace Homework_6._3
{
   // Определяем структуру
   public struct Business
   {
      public string Group;
      public string Surname;
      public string Name;
      public string Dadsname;
      public int Year;
      public char Gender;
      public int Physics;
      public int Math;
      public int Inf;
      public double Grant;
   }

   internal class Program
   {
      static void Main(string[] args)
      {
         string fileEnter = "hardstructure.txt";
         string pathStruct = Path.GetFullPath(fileEnter);
         string writeStruct = "writestruct.bin";
         string pathWrite = Path.GetFullPath(writeStruct);
         string readStruct = "readstruct.bin";
         string pathRead = Path.GetFullPath(readStruct);
         string fileInput = "finish.txt";

         // Создание массива структур
         Business[] students =
         {
            new Business
            {
               Group = "IP-21", Surname = "Иванов", Name = "Иван", Dadsname = "Иванович",
               Year = 2000, Gender = 'М', Physics = 4, Math = 5, Inf = 3, Grant = 5000
            },
            new Business
            {
               Group = "IP-21", Surname = "Петрова", Name = "Анна", Dadsname = "Сергеевна",
               Year = 2001, Gender = 'Ж', Physics = 5, Math = 4, Inf = 5, Grant = 6000
            },
            new Business
            {
               Group = "IP-22", Surname = "Смирнов", Name = "Алексей", Dadsname = "Викторович",
               Year = 1999, Gender = 'M', Physics = 3, Math = 4, Inf = 4, Grant = 4000
            },
            new Business
            {
               Group = "Fiz-21", Surname = "Кузнецова", Name = "Мария", Dadsname = "Павловна",
               Year = 2000, Gender = 'Ж', Physics = 5, Math = 5, Inf = 5, Grant = 7000
            },
            new Business
            {
               Group = "Phys-22", Surname = "Сидоров", Name = "Дмитрий", Dadsname = "Андреевич",
               Year = 2001, Gender = 'M', Physics = 4, Math = 3, Inf = 4, Grant = 4500
            },
            new Business
            {
               Group = "IP-22", Surname = "Васильева", Name = "Екатерина", Dadsname = "Николаевна",
               Year = 2008, Gender = 'Ж', Physics = 3, Math = 3, Inf = 3, Grant = 3000
            },
            new Business
            {
               Group = "Fiz-21", Surname = "Орлов", Name = "Сергей", Dadsname = "Владимирович",
               Year = 2000, Gender = 'M', Physics = 4, Math = 4, Inf = 3, Grant = 3000
            },
            new Business
            {
               Group = "IP-21", Surname = "Лебедева", Name = "Светлана", Dadsname = "Александровна",
               Year = 2001, Gender = 'Ж', Physics = 5, Math = 5, Inf = 5, Grant = 8000
            },
            new Business
            {
               Group = "Fiz-22", Surname = "Николаев", Name = "Андрей", Dadsname = "Сергеевич",
               Year = 2008, Gender = 'M', Physics = 3, Math = 2, Inf = 3, Grant = 2500
            },
            new Business
            {
               Group = "IP-22", Surname = "Сергеева", Name = "Дарья", Dadsname = "Викторовна",
               Year = 2008, Gender = 'Ж', Physics = 2, Math = 2, Inf = 2, Grant = 2000
            }
         };
         // Запись массива структур в текстовый файл
         VariousMethods.WriteStructFileTxt(pathStruct, students);
         // Чтение массива структур из текстового файла
         Business[] readStudents = VariousMethods.ReadStructFileTxt(pathStruct, "hardstructure.txt");
         // Вывод прочитанных данных
         Console.WriteLine("Прочитанные данные из текстового файла:");
         int i = 0;
         while (i < readStudents.Length)
         {
            Business student = readStudents[i];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
               student.Group, student.Surname, student.Name, student.Dadsname, student.Year,
               student.Gender, student.Physics, student.Math, student.Inf, student.Grant);
            i++;
         }

         Console.WriteLine();
         // Запись массива структур в бинарный файл
         VariousMethods.WriteStructFileBin(pathWrite, readStudents);
         // Чтение массива структур из бинарного файла
         Business[] readCadets = VariousMethods.ReadStructFileBin(pathWrite);
         // Вывод прочитанных данных
         Console.WriteLine("Прочитанные данные из бинарного файла:");
         int j = 0;
         while (j < readCadets.Length)
         {
            Business cadet = readCadets[j];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
               cadet.Group, cadet.Surname, cadet.Name, cadet.Dadsname, cadet.Year,
               cadet.Gender, cadet.Physics, cadet.Math, cadet.Inf, cadet.Grant);
            j++;
         }
         // Расчет среднего балла всех студентов по всем предметам
         Console.WriteLine();
         double average = VariousMethods.AverageScore(students);
         // Поиск студентов средний балл которых выше, чем общий средний балл
         Console.WriteLine();
         VariousMethods.AverageHigherScore(pathRead, students, average);
         // Поиск несовершеннолетнего студента с худшим средним баллом
         Console.WriteLine();
         VariousMethods.MinorStudentWorstAverage(fileInput, students);
      }
   }
}