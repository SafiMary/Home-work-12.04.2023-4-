using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Home_work_12._04._2023_4_
{
    enum Month
    {
        Январь = 1,
        Февраль,
        Март,
        Апрель,
        Май,
        Июнь,
        Июль,
        Август,
        Сентябрь,
        Октябрь,
        Ноябрь,
        Декабрь
    }

    class MeterReaderCold
    {
        int ColdwaterCount;
        public int ColdWaterCount
        {
            get
            {
                return ColdwaterCount;
            }
            set
            {
                ColdwaterCount = value;
            }
        }
        public MeterReaderCold(int _ColdwaterCount)
        {
            ColdwaterCount = _ColdwaterCount;
        }
        public string convertStrCold()
        {
            string _tmp = ColdwaterCount.ToString();
            while (_tmp.Length < 8)
            {
                _tmp = "0" + _tmp;
            }
            return _tmp;
        }
    }
    class MeterReaderHot
    {
        int HotwaterCount;
        public int HotWaterCount
        {
            get
            {
                return HotwaterCount;
            }
            set
            {
                HotwaterCount = value;
            }
        }
        public MeterReaderHot(int _HotWaterCount)
        {
            HotWaterCount = _HotWaterCount;
        }
        public string convertStrHot()
        {
            string _tmp = HotwaterCount.ToString();
            while (_tmp.Length < 8)
            {
                _tmp = "0" + _tmp;
            }
            return _tmp;
        }
    }
    struct MeterReader
    {
        public MeterReaderCold cold;
        public MeterReaderHot hot;

    }
    class myCounter
    {
        int _min = 0, _max = 99999999;
        List<MeterReader> myList = new List<MeterReader>();
        
        public myCounter(int _cold, int _hot)
        {
            if (_cold >= _min || _cold <= _max)
            {
                if (_hot >= _min || _hot <= _max)
                {
                    MeterReader mystruct;
                    mystruct.cold = new MeterReaderCold(_cold);
                    mystruct.hot = new MeterReaderHot(_hot);
                    myList.Add(mystruct);
                }
            }
        }
        public bool addMetric(int numcold, int numhot)
        {
            bool result = false;
            int _lastElement = myList.Count;
            if (myList[_lastElement - 1].cold.ColdWaterCount <= numcold)
            {
                if (myList[_lastElement - 1].hot.HotWaterCount <= numhot)
                {
                    MeterReader mystruct;
                    mystruct.cold = new MeterReaderCold(numcold);
                    mystruct.hot = new MeterReaderHot(numhot);
                    myList.Add(mystruct);
                    result = true;
                }

            }
            else
            {
                Console.WriteLine("Показания не могут быть меньше предыдущих!!");
            }
            return result;
        }
        public List<MeterReader> getValues()
        {
            return myList;
        }
       

    }
    internal class Program
    {
        static void addFromArr()
        {
            

            int x = 12;
            int y = 2;
            int _cold = 0, _hot = 0;
            int sumcold = 0, sumhot = 0;
            int[,] mas = new int[x, y];
            myCounter _meterReader = new myCounter(0, 0);
            Console.WriteLine("Добро пожаловать в личный кабинет!\n");
            StreamWriter f = new StreamWriter("test.txt", true);
            for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {

                        if (j % 2 == 0)
                        {
                        try
                        {
                            Console.Write("холодная =  ");
                            mas[i, j] = int.Parse(Console.ReadLine());
                            _cold = mas[i, j];
                            sumcold += mas[i, j];
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный формат ввода!попробуйте заново");
                            Console.Write("холодная =  ");
                            mas[i, j] = int.Parse(Console.ReadLine());
                            _cold = mas[i, j];
                            sumcold += mas[i, j];
                            Console.Write(mas[i, j]);
                            f.WriteLine(mas[i, j]);
                        }

                    }
                        else
                        {
                        try
                        {
                            Console.Write("горячая =  ");
                            mas[i, j] = int.Parse(Console.ReadLine());
                            _hot = mas[i, j];
                            sumhot += mas[i, j];
                           
                        }
                        catch (Exception)
                    {
                        Console.WriteLine("Неверный формат ввода!попробуйте заново");
                        Console.Write("горячая =  ");
                        mas[i, j] = int.Parse(Console.ReadLine());
                        _hot = mas[i, j];
                        sumhot += mas[i, j];
                            Console.Write(mas[i, j]);
                            f.WriteLine(mas[i, j]);
                    }
                    }
                    }
                
                _meterReader.addMetric(_cold, _hot);
                    
            }
                for (int i = 0; i < x; i++)
                {
                    f.WriteLine();
                    for (int j = 0; j < y; j++)
                    {
                        f.Write(mas[i, j] + " "); // запись в файл массива
                }
                }
            DateTime now = DateTime.Now;
            f.WriteLine($"\nВремя создания: {now}");
            f.Close();
           
            Console.WriteLine();
            int _month = 1;
            string myMonth;
            _meterReader.getValues().RemoveAt(0);
            
            foreach (var item in _meterReader.getValues())
            {
                myMonth = Enum.GetName(typeof(Month), _month);
                Console.WriteLine($"За {myMonth} \t холодная = {item.cold.convertStrCold()} горячая = {item.hot.convertStrHot()}");
                _month++;
            }
            Console.WriteLine($"\nПоказания  за год составили\nхолодная  вода: {sumcold}  горячая вода: {sumhot}");
            f.Close();
        }

        static void Main(string[] args)

        {
            addFromArr();


        }
    }
}


