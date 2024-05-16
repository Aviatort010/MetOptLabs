using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR
{
    /// <summary>
    /// Класс, для работы с файлом.
    /// </summary>
    class FileWork
    {
        private string filenameData;
        private string filenameResult;

        public FileWork(string filenameData, string filenameResult)
        {
            this.filenameData = filenameData;
            this.filenameResult = filenameResult;
        }

        public void WriteData(params object[] values)
        {
            // using Чтобы само закрылось всё по окончанию работы.
            using (StreamWriter writer = new StreamWriter(filenameResult, true))
            {
                // Преобразуем каждое значение в строку
                // и записываем их в заранее указанный файл.
                foreach (object value in values)
                {
                    writer.WriteLine(value.ToString());
                }
            }
        }

        public List<double> ReadData()
        {
            List<double> doubles = new List<double>();

            using (StreamReader reader = new StreamReader(filenameData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Пытаемся преобразовать строку в значение типа double
                    if (double.TryParse(line, out double result))
                    {
                        // Если преобразование удалось, добавляем значение в список
                        doubles.Add(result);
                    }
                    else
                    {
                        // Если преобразование не удалось, выводим сообщение об ошибке
                        Console.WriteLine($"Ошибка при чтении строки: '{line}' не является числом типа double.");
                    }
                }
            }
            return doubles;
        }
    }

    internal class Experiments
    {
    }
}
