using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KursKPO
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
    /*!
    *   @brief Класс TextFileReader предназначен для чтения числовых значений из текстового файла.
    */
    public class TextFileReader
    {
        /*!
        *   @brief Метод ReadSample читает числовые значения из указанного файла.
        *   @param filePath Путь к файлу, содержащему числовые значения.
        *   @return Список числовых значений, прочитанных из файла.
        */
        public List<double> ReadSample(string filePath)
        {
            List<double> sample = new List<double>();

            try
            {
                string text = File.ReadAllText(filePath);
                string[] values = text.Split(' ');

                foreach (string value in values)
                {
                    if (double.TryParse(value, out double number))
                    {
                        sample.Add(number);
                    }
                    else
                    {
                        Console.WriteLine($"Неверное значение: {value}. Пропуск...");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Путь не найден!: {ex.Message}");
            }

            return sample;
        }
    }
    /*!
    *   @brief Класс SampleMean предназначен для вычисления среднего значения выборки.
    */
    class SampleMean
    {
        /*!
        *   @param Sample Список числовых значений выборки.
        */
        private readonly List<double> Sample;

        /*!
        *   @brief Конструктор класса SampleMean.
        */
        public SampleMean(List<double> sample)
        {
            Sample = sample;
        }

        /*!
        *   @brief Метод FindMean вычисляет среднее значение выборки.
        *   @return Среднее значение выборки.
        */
        public double FindMean()
        {
            int n = Sample.Count;
            double mean = Sample.Sum();
            return mean / n;
        }
    }
    /*!
    *   @brief Класс SampleVar предназначен для вычисления дисперсии выборки.
    */
    class SampleVar
    {
        /*!
        *   @param Sample Список числовых значений выборки.
        */
        private readonly List<double> Sample;
        /*!
        *   @brief Конструктор класса SampleVar.
        */
        public SampleVar(List<double> sample)
        {
            Sample = sample;
        }
        /*!
        *   @brief Метод FindVar вычисляет дисперсию выборки.
        *   @return Дисперсия выборки.
        */
        public double FindVar()
        {
            int n = Sample.Count;
            SampleMean mean = new SampleMean(Sample);
            double variance = 0;
            foreach (var element in Sample)
            {
                variance += Math.Pow(element - mean.FindMean(), 2);
            }
            return variance / (n - 1);
        }
    }
    /*!
    *   @brief Класс GiniDifference предназначен для вычисления разности Джини выборки.
    */
    class GiniDifference
    {
        /*!
        *   @param Sample Список числовых значений выборки.
        */
        private readonly List<double> Sample;
        /*!
         * @param N Объем выборки.
         */
        private readonly int N;
        /*!
        *   @brief Конструктор класса GiniDifference.
        */
        public GiniDifference(List<double> sample)
        {
            Sample = sample;
            N = sample.Count;
        }
        /*!
        *   @brief Метод FindDifference вычисляет разность Джини выборки.
        *   @return Разность Джини выборки.
        */
        public double FindDifference()
        {
            double diff = 0;
            for (int i = 0; i < N - 1; i++)
            {
                double temp = 0;
                for (int j = i + 1; j < N; j++)
                {
                    temp += Math.Abs(Sample[i] - Sample[j]);
                }
                diff += temp;
            }
            return (2 * diff) / (N * (N - 1));
        }
    }
    /*!
    *   @brief Класс SampleCentralMoment предназначен для вычисления центрального момента выборки.
    */
    class SampleCentralMoment
    {
        /*!
        *   @param Sample Список числовых значений выборки.
        */
        private readonly List<double> Sample;
        /*!
         * @param K Порядок центрального момента.
         */
        private readonly int K;
        /*!
        *   @brief Конструктор класса SampleCentralMoment.
        */
        public SampleCentralMoment(List<double> sample, int k)
        {
            Sample = sample;
            K = k;
        }
        /*!
        *   @brief Метод FindCentralMoment вычисляет центральный момент выборки.
        *   @return Значение центрального момента выборки.
        */
        public double FindCentralMoment()
        {
            int n = Sample.Count;
            SampleMean mean = new SampleMean(Sample);
            double centralMoment = 0;
            foreach (var element in Sample)
            {
                centralMoment += Math.Pow(element - mean.FindMean(), K);
            }
            return centralMoment / n;
        }
    }
}
