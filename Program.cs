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
      public string Company;
      public string Department;
      public double Profit;
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
         Business[] firm =
         {
            new Business
            {
               Company = "СтройГрад", Department = "Строительство", Profit = 125.589
            },
            new Business
            {
               Company = "АгроПром", Department = "Животноводство", Profit = 280.789
            },
            new Business
            {
               Company = "Сбер", Department = "Акции", Profit = 910.258
            },
            new Business
            {
               Company = "РосГвардия", Department = "Охрана", Profit = 538.462
            },
            new Business
            {
               Company = "АлмазАнтей", Department = "Вооружение", Profit = 830.489
            },
            new Business
            {
               Company = "ТехноСервис", Department = "Ремонт", Profit = 28.823
            },
            new Business
            {
               Company = "ФинансИнвест", Department = "Кредитование", Profit = 200.921
            },
            new Business
            {
               Company = "Быстроход", Department = "Такси", Profit = 387.773
            },
            new Business
            {
               Company = "НефтьСиб", Department = "Добыча", Profit = 875.537
            },
            new Business
            {
               Company = "ТрансЭнерго", Department = "Производство", Profit = 420.634
            }
         };
         // Запись массива структур в текстовый файл
         VariousMethods.WriteStructFileTxt(pathStruct, firm);
         // Чтение массива структур из текстового файла
         Business[] readStudents = VariousMethods.ReadStructFileTxt(pathStruct, "hardstructure.txt");
         // Вывод прочитанных данных
         Console.WriteLine("Прочитанные данные из текстового файла:");
         int i = 0;
         while (i < readStudents.Length)
         {
            Business student = readStudents[i];
            Console.WriteLine("{0} {1} {2}",
               student.Company, student.Department, student.Profit);
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
            Console.WriteLine("{0} {1} {2}", cadet.Company, cadet.Department, cadet.Profit);
            j++;
         }
         // Расчет среднего балла всех студентов по всем предметам
         Console.WriteLine();
         double average = VariousMethods.AverageScore(firm);
         // Поиск студентов средний балл которых выше, чем общий средний балл
         Console.WriteLine();
         VariousMethods.AverageHigherScore(pathRead, firm, average);
         // Поиск несовершеннолетнего студента с худшим средним баллом
         Console.WriteLine();
         VariousMethods.MinorStudentWorstAverage(fileInput, firm);
      }
   }
}