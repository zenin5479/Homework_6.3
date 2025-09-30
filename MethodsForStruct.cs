using System;
using System.IO;
using System.Text;

namespace Homework_6._3
{
   public class MethodsForStruct
   {
      // Метод записи массива структур в текстовый файл
      public static void WriteStructFileTxt(string path, Business[] firm)
      {
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Write);
         StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
         int i = 0;
         while (i < firm.Length)
         {
            Business person = firm[i];
            writer.WriteLine("{0} {1} {2}", person.Company, person.Department, person.Profit);
            i++;
         }

         writer.Close();
      }

      // Метод чтения массива структур из текстового файла
      public static Business[] ReadStructFileTxt(string path, string nameFile)
      {
         Business[] arrayFirm = { };
         // Чтение файла за одну операцию
         string[] allLines = File.ReadAllLines(path, Encoding.UTF8);
         if (allLines == null || allLines.Length == 0)
         {
            Console.WriteLine("Ошибка содержимого файла для чтения {0}", nameFile);
            //Console.WriteLine("Ошибка содержимого файла для чтения {0}. Файл пуст", nameFile);
         }
         else
         {
            // Разделение строки на подстроки по пробелу для определения количества столбцов в строке
            arrayFirm = new Business[allLines.Length];
            int[] сolumnArray = new int[allLines.Length];
            char symbolSpace = ' ';
            int countRow = 0;
            int countSymbol = 0;
            int countСolumn = 0;
            while (countRow < allLines.Length)
            {
               string line = allLines[countRow];
               while (countSymbol < line.Length)
               {
                  if (symbolSpace == line[countSymbol])
                  {
                     countСolumn++;
                  }

                  if (countSymbol == line.Length - 1)
                  {
                     countСolumn++;
                  }

                  countSymbol++;
               }

               сolumnArray[countRow] = countСolumn;
               // 3 количество полей в структуре
               if (countСolumn != 3)
               {
                  Console.WriteLine("Неверный формат строки {0}", countRow);
               }

               countRow++;
               countСolumn = 0;
               countSymbol = 0;
            }

            // Поиск максимального и минимального элемента массива
            // Cчитаем, что максимум - это первый элемент массива
            int max = сolumnArray[0];
            // Cчитаем, что минимум - это первый элемент массива
            int min = сolumnArray[0];
            int columns = 0;
            while (columns < сolumnArray.Length)
            {
               if (max < сolumnArray[columns])
               {
                  max = сolumnArray[columns];
               }

               if (min > сolumnArray[columns])
               {
                  min = сolumnArray[columns];
               }

               columns++;
            }

            //Console.WriteLine("Максимум равен: {0}", max);
            //Console.WriteLine("Минимум равен: {0}", min);

            // Разделение строки на подстроки по пробелу и конвертация подстрок в структуру
            string[] lineArray = new string[max];
            StringBuilder stringModified = new StringBuilder();
            char spaceCharacter = ' ';
            int row = 0;
            int column = 0;
            int countCharacter = 0;
            while (row < allLines.Length)
            {
               string line = allLines[row];
               while (column < сolumnArray[row])
               {
                  while (countCharacter < line.Length)
                  {
                     if (spaceCharacter == line[countCharacter])
                     {
                        string subLine = stringModified.ToString();
                        lineArray[column] = subLine;
                        stringModified.Clear();
                        column++;
                     }
                     else
                     {
                        stringModified.Append(line[countCharacter]);
                     }

                     if (countCharacter == line.Length - 1)
                     {
                        string subLine = stringModified.ToString();
                        lineArray[column] = subLine;
                        stringModified.Clear();
                        column++;
                     }

                     countCharacter++;
                  }

                  arrayFirm[row].Company = lineArray[0];
                  arrayFirm[row].Department = lineArray[1];
                  arrayFirm[row].Profit = Convert.ToDouble(lineArray[2]);

                  countCharacter = 0;
               }

               row++;
               column = 0;
            }
         }

         return arrayFirm;
      }

      // Метод записи массива структур в бинарный файл
      public static void WriteStructFileBin(string path, Business[] firm)
      {
         FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
         BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
         writer.Write(firm.Length);
         int i = 0;
         while (i < firm.Length)
         {
            Business person = firm[i];
            // Запись строки в UTF-8 с предварительной длиной
            writer.Write(person.Company);
            writer.Write(person.Department);
            writer.Write(person.Profit);
            i++;
         }

         stream.Close();
         writer.Close();
      }

      // Метод чтения массива структур из бинарного файла
      public static Business[] ReadStructFileBin(string path)
      {
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
         BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
         int length = reader.ReadInt32();
         Business[] firm = new Business[length];
         int i = 0;
         while (i < length)
         {
            string company = reader.ReadString();
            string department = reader.ReadString();
            double profit = reader.ReadDouble();
            firm[i] = new Business { Company = company, Department = department, Profit = profit };
            i++;
         }

         stream.Close();
         reader.Close();
         return firm;
      }

      // Метод поиска прибыльных и убыточных подразделений
      public static string ProfitAnalysis(Business[] firm)
      {
         // Определяем количество прибыльных и убыточных подразделений
         int profitHigher = 0;
         int profitLow = 0;
         int i = 0;
         while (i < firm.Length)
         {
            if (firm[i].Profit > 0)
            {
               profitHigher++;
            }
            else
            {
               profitLow++;
            }

            i++;
         }

         string result = null;
         if (profitHigher > profitLow)
         {
            string higher = "Прибыльных подразделений " + profitHigher + " больше чем убыточных " + profitLow;
            Console.WriteLine(higher);
            result = higher;
         }
         if (profitHigher < profitLow)
         {
            string low = "Убыточных подразделений " + profitLow + " больше чем прибыльных " + profitHigher;
            Console.WriteLine(low);
            result = low;
         }
         if (profitHigher == profitLow)
         {
            string equally = "Прибыльных " + profitHigher + " и убыточных " + profitLow + " подразделений поровну";
            Console.WriteLine(equally);
            result = equally;
         }

         return result;
      }

      // Метод поиска подразделения с наибольшим профицитом 
      public static string ProfitMax(Business[] firm)
      {
         // Определяем подразделение с наибольшим профицитом 
         // Cчитаем, что максимум - это первый элемент структуры
         double max = firm[0].Profit;
         string company = firm[0].Company;
         int row = 0;
         while (row < firm.Length)
         {
            if (max < firm[row].Profit)
            {
               max = firm[row].Profit;
               company = firm[row].Company;
            }

            row++;
         }

         string higher = "Подразделение с наибольшим профицитом: " + max + " - " + company;
         Console.WriteLine(higher);
         return higher;
      }

      // Метод записи строки в текстовый файл
      public static void FileWriteArrayString(string path, string line)
      {
         // Создание одномерного массива строк string[] для записи в файл строки
         string[] stringArray = { line };
         // Запись массива строк в файл
         File.WriteAllLines(path, stringArray);
      }

      // Метод добавления строки в текстовый файл
      public static void FileAppendStringArray(string path, string line)
      {
         // Создание одномерного массива строк string[] для записи в файл строки
         string[] stringArray = { line };
         // Добавление массива строк в файл
         File.AppendAllLines(path, stringArray);
      }
   }
}