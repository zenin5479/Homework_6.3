using System;
using System.IO;
using System.Text;

namespace Homework_6._3
{
   public class VariousMethods
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
               // 10 количество полей в структуре
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
      public static void WriteStructFileBin(string path, Business[] students)
      {
         FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
         BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
         writer.Write(students.Length);
         int i = 0;
         while (i < students.Length)
         {
            Business person = students[i];
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
         Business[] persons = new Business[length];
         int i = 0;
         while (i < length)
         {
            string group = reader.ReadString();
            string surname = reader.ReadString();
            string name = reader.ReadString();
            persons[i] = new Business
            {
               Company = group,
               Department = surname,
               Profit = Convert.ToDouble(name)
              
            };

            i++;
         }

         stream.Close();
         reader.Close();
         return persons;
      }

      // Метод расчета среднего балла всех студентов по всем предметам
      public static double AverageScore(Business[] students)
      {
         double medium;
         double allSubjects = 0;
         int i = 0;
         while (i < students.Length)
         {
            double bySubjects = ((students[i].Profit + students[i].Profit + students[i].Profit) / 3.0f);
            allSubjects += bySubjects;
            //Console.WriteLine("Cредний балл: {0} {1} - {2:f2}",
            //   students[i].Surname, students[i].Name, bySubjects);
            Console.WriteLine("Cредний балл: {0} {1} - {2:f}",
               students[i].Department, students[i].Profit, bySubjects);
            i++;
         }

         medium = allSubjects / students.Length;
         Console.WriteLine("Средний балл всех студентов по всем предметам: {0:f}", medium);
         return medium;
      }

      // Метод поиска студентов средний балл которых выше, чем общий средний балл
      public static void AverageHigherScore(string path, Business[] student, double medium)
      {
         Console.WriteLine("Студенты, средний балл которых выше, чем общий средний балл:");
         // Определяем количество студентов удовлетворяющих условию для расчета размера массива структур
         int count = 0;
         int i = 0;
         while (i < student.Length)
         {
            double bySubjects = ((student[i].Profit + student[i].Profit + student[i].Profit) / 3.0f);
            if (bySubjects > medium)
            {
               count++;
            }

            i++;
         }

         Business[] averageHigher = new Business[count];
         int j = 0;
         int k = 0;
         while (j < student.Length)
         {
            double bySubjects = ((student[j].Profit + student[j].Profit + student[j].Profit) / 3.0f);
            if (bySubjects > medium)
            {
               averageHigher[k] = student[j];
               Console.WriteLine("{0} {1}", student[j].Department, student[j].Profit);
               k++;
            }

            j++;
         }

         // Запись массива структур в бинарный файл
         FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
         BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
         writer.Write(averageHigher.Length);
         int m = 0;
         while (m < averageHigher.Length)
         {
            // Запись строки в UTF-8 с предварительной длиной
            writer.Write(averageHigher[m].Department);
            writer.Write(averageHigher[m].Profit);
            m++;
         }

         stream.Close();
         writer.Close();
      }

      // Метод поиска несовершеннолетнего студента с худшим средним баллом
      public static void MinorStudentWorstAverage(string path, Business[] student)
      {
         Console.WriteLine("Несовершеннолетние студенты:");
         // Возраст совершеннолетнего студента
         int underage = 18;
         int currentDate = DateTime.Now.Year;
         // Определяем количество студентов удовлетворяющих условию для расчета размера массива структур
         int count = 0;
         int i = 0;
         while (i < student.Length)
         {
            double minorStudent = (currentDate - student[i].Profit);
            if (minorStudent < underage)
            {
               count++;
            }

            i++;
         }

         Business[] minor = new Business[count];
         int j = 0;
         int k = 0;
         while (j < student.Length)
         {
            double minorStudent = (currentDate - student[j].Profit);
            if (minorStudent < underage)
            {
               minor[k] = student[j];
               Console.WriteLine("{0} {1} {2} {3}",
                  student[j].Company, student[j].Department, student[j].Profit);
               k++;
            }

            j++;
         }

         // Рассчитываем средний балл несовершеннолетних студентов для добавления в массив структур и расчета худшего среднего балла
         int l = 0;
         double[] average = new double[count];
         double bySubjects;
         while (l < minor.Length)
         {
            bySubjects = ((minor[l].Profit + minor[l].Profit + minor[l].Profit) / 3.0f);
            average[l] = bySubjects;
            l++;
         }

         // Cчитаем, что минимум - это первый элемент массива
         double min = average[0];
         int m = 0;
         while (m < average.Length)
         {
            if (min > average[m])
            {
               min = average[m];
            }

            m++;
         }

         Console.WriteLine("Худший средний балл: {0:f}", min);
         //Console.WriteLine("Худший средний балл: {0:f2}", min);

         // Поиск индекса минимума массива
         int n = 0;
         int counter = 0;
         bool flag = false;
         while (n < average.Length && flag == false)
         {
            // Сравниваем значения double используя метод CompareTo(Double) 
            if (average[n].CompareTo(min) == 0)
            {
               counter = n;
               flag = true;
            }

            // Сравниваем значения double используя метод Equals(Double)
            //if (average[n].Equals(min))
            //{
            //   counter = n;
            //   flag = true;
            //}

            n++;
         }

         if (flag)
         {
            Console.WriteLine("Индекс худшего среднего балла: {0}", counter);
         }

         Console.WriteLine("Несовершеннолетний студент с худшим средним баллом:");
         Business worstAverage = minor[counter];
         Console.WriteLine("{0} {1} {2}",
            worstAverage.Company, worstAverage.Department, worstAverage.Profit);

         // Запись структуры в текстовый файл
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Write);
         StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
         writer.WriteLine("{0} {1} {2}",
            worstAverage.Company, worstAverage.Department, worstAverage.Profit);
         writer.Close();
      }

      // Метод сортировки массива структур по возрасту
      public static void BubbleSortByAge(Business[] students)
      {
         Console.WriteLine("Отсортированный массив структур по возрасту:");
         // Если нужно сортировать по другим критериям изменяем условие в сортировке:
         // используем string.Compare
         // if (string.Compare(arr[j].Name, arr[j + 1].Name) > 0)
         int n = students.Length;
         int i = 0;
         while (i < n - 1)
         {
            int j = 0;
            while (j < n - i - 1)
            {
               // Сравниваем соседние элементы
               if (students[j].Profit > students[j + 1].Profit)
               {
                  // Меняем местами структуры
                  Business temp = students[j];
                  students[j] = students[j + 1];
                  students[j + 1] = temp;
               }

               j++;
            }

            i++;
         }

         int index = 0;
         while (index < students.Length)
         {
            Business person = students[index];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
               person.Company, person.Department, person.Profit);
            index++;
         }
      }
   }
}