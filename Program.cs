using LibraryForStruct;
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
// Подразделение с наибольшим профицитом

namespace Homework_6._3
{
   internal class Program
   {
      static void Main()
      {
         string fileEnter = "hardstructure.txt";
         string pathStruct = Path.GetFullPath(fileEnter);
         string writeStruct = "hardstructure.bin";
         string pathWrite = Path.GetFullPath(writeStruct);
         string fileInput = "finish.txt";
         string pathInput = Path.GetFullPath(fileInput);

         // Создание массива структур
         VariousMethodsForStructBusiness.Business[] firm =
         {
            new VariousMethodsForStructBusiness.Business
            {
               Company = "СтройГрад", Department = "Строительство", Profit = 125.589
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "АгроПром", Department = "Животноводство", Profit = 280.789
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "Сбер", Department = "Акции", Profit = 910.258
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "РосГвардия", Department = "Охрана", Profit = -538.462
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "АлмазАнтей", Department = "Вооружение", Profit = 830.489
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "ТехноСервис", Department = "Ремонт", Profit = -28.823
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "ФинансИнвест", Department = "Кредитование", Profit = -200.921
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "Быстроход", Department = "Такси", Profit = 387.773
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "НефтьСиб", Department = "Добыча", Profit = 875.537
            },
            new VariousMethodsForStructBusiness.Business
            {
               Company = "ТрансЭнерго", Department = "Производство", Profit = -420.634
            }
         };
         // Запись массива структур в текстовый файл
         VariousMethodsForStructBusiness.WriteStructFileTxt(pathStruct, firm);
         // Чтение массива структур из текстового файла
         VariousMethodsForStructBusiness.Business[] readFirm = VariousMethodsForStructBusiness.ReadStructFileTxt(pathStruct, "hardstructure.txt");
         // Вывод прочитанных данных
         Console.WriteLine("Прочитанные данные из текстового файла:");
         int i = 0;
         while (i < readFirm.Length)
         {
            VariousMethodsForStructBusiness.Business biz = readFirm[i];
            Console.WriteLine("{0} {1} {2}",
               biz.Company, biz.Department, biz.Profit);
            i++;
         }

         Console.WriteLine();
         // Запись массива структур в бинарный файл
         VariousMethodsForStructBusiness.WriteStructFileBin(pathWrite, readFirm);
         // Чтение массива структур из бинарного файла
         VariousMethodsForStructBusiness.Business[] readOrganization = VariousMethodsForStructBusiness.ReadStructFileBin(pathWrite);
         // Вывод прочитанных данных
         Console.WriteLine("Прочитанные данные из бинарного файла:");
         int j = 0;
         while (j < readOrganization.Length)
         {
            VariousMethodsForStructBusiness.Business concern = readOrganization[j];
            Console.WriteLine("{0} {1} {2}", concern.Company, concern.Department, concern.Profit);
            j++;
         }
         Console.WriteLine();

         // Поиска прибыльных и убыточных подразделений
         string analysis = VariousMethodsForStructBusiness.ProfitAnalysis(readOrganization);
         VariousMethodsForStructBusiness.FileWriteArrayString(pathInput, analysis);
         Console.WriteLine();

         string anal = VariousMethodsForStructBusiness.ProfitMax(readOrganization);
         VariousMethodsForStructBusiness.FileAppendStringArray(pathInput, anal);

         Console.ReadKey();
      }
   }
}