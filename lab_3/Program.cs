using System;
using System.Dynamic;
using System.Runtime.Loader;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;

namespace lab_3
{
    public partial class Abiturient
    {
        private readonly int id;//generate
        private string lastName;//input
        private string name;//input
        private string fatherName;//input
        private string adres;//input
        private int phNumber;//input
        private int[] marks;//generate
        private const string uO = "BSTU";
        private static int amout;

        //public Abiturient()    //конструктор по умолчанию, создается если в проге нет прописанны конструкторы не имеет ни параметров ни тела
       // { }

        public Abiturient()
        {
            id = 0000;
            lastName = "No lastname";
            name = "No name";
            fatherName = "No father's name";
            adres = "Unknown";
            phNumber = 0000000;
            marks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            amout++;
        }

        public Abiturient(string n)
        {
            id = 0000;
            lastName = "No lastname";
            name = n;
            fatherName = "No father's name";
            adres = "Unknown";
            phNumber = 0000000;
            marks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            amout++;
        }

        private Abiturient(string n, string ln) //закрытый конструктор? можно вызвать из дочерних классов
        {
            id = 0000;
            lastName = ln;
            name = n;
            fatherName = "No father's name";
            adres = "Unknown";
            phNumber = 0000000;
            marks = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            amout++;
        }

        public Abiturient(string ln, string n, string fn, string a, int tel)
        {
            id = (ln.Length + n.Length + fn.Length + a.Length + tel) % 2;
            lastName = ln;
            name = n;
            fatherName = fn;
            adres = a;
            phNumber = tel;
            marks = new int[13];
            Random rnd = new Random();
            for (int i = 0; i < marks.Length; i++)
            {
                marks[i] = rnd.Next(0,10);
            }
            amout++;
        }

        static Abiturient() //статический конструктор
        {
            Console.WriteLine("Первый абитуриент добавлен в базу данных");
        }
    }

    public partial class Abiturient { 
        public string LastName
        {
            set 
            {
                lastName = value;
            }
            get
            {
                return lastName;
            }
        }

        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }

        public string FatherName
        {
            set
            {
                fatherName = value;
            }
            get
            {
                return fatherName;
            }
        }

        public string Adres
        {
            set
            {
                adres = value;
            }
            get
            {
                return adres;
            }
        }

        public int PhNumber
        {
            set
            {
                if(value<1000000 || value>9999999)
                {
                    Console.WriteLine("Некорректно введен номер( номер должен содержать 7 цифр");
                }
                else
                {
                    phNumber = value;
                }
            }
            get
            {
                return phNumber;
            }
        }

        public string UO
        {
            get
            {
                return uO;
            }
        }

        public void Info()
        {
            Console.WriteLine($"Идентификационный номер: {id}");
            Console.WriteLine($"Учреждение образования:{uO}");
            Console.WriteLine($"Ф.И.О.:{lastName} {name} {fatherName}");
            Console.WriteLine($"Адрес: {adres}");
            Console.WriteLine($"Телефон: {phNumber}");
            foreach (int mark in marks)
                Console.Write(mark+"; ");
            Console.WriteLine("  ");
        }

        public static void StaticInfo()
        {
            Console.WriteLine($"Количество абитуриентов: {amout}");
        }
        public void MiddleScore(out double midScore)
        {
            int sum = 0;
            for(int i=0; i<marks.Length; i++)
            {
                sum += marks[i];
            }
            midScore = sum / marks.Length;
        }
        public (int, int) MinMax(ref int min, ref int max)
        {
            Array.Sort(marks);
            min = marks[0];
            max = marks[marks.Length - 1];
            return (min, max);
        }

        public override string ToString()
        {
            return "Type"+" "+base.ToString()+lastName;
        }

        public override Boolean Equals(Object obj)
        {
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            Abiturient ab = (Abiturient)obj;
            if (this.id == ab.id) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            int hash = 47;
            hash = string.IsNullOrEmpty(lastName) ? 0 : lastName.GetHashCode();
            hash = (hash * 19) + phNumber.GetHashCode();
            return hash;
        }

        public int TotalScore()
        {
            int sum=0;
            foreach (int mark in marks)
                sum += mark;
            return sum;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Abiturient Ab1 = new Abiturient();
            Ab1.Info();
            double mid = 0;
            int min = 0;
            int max = 0;
            Ab1.MiddleScore(out mid);
            Console.WriteLine($"Средняя отметка:{mid}");
            Console.WriteLine(Ab1.ToString());
            Console.WriteLine(Ab1.GetHashCode());
            Console.WriteLine(Ab1.GetType());


            Abiturient Ab3 = new Abiturient("Ogngfi");
            Ab3.Info();
            Console.WriteLine(Ab3.ToString());
            Console.WriteLine(Ab3.GetHashCode());
            Console.WriteLine(Ab3.GetType());

            Abiturient Ab4 = new Abiturient("Mann", "Ivan", "Fedorof", "K. Marks street 47", 3764908);
            Ab4.Info();
            Ab4.MiddleScore(out mid);
            (int, int) mm = Ab4.MinMax(ref min, ref max);
            Console.WriteLine($"наименьшая отметка: {mm.Item1}; наибольшая отметка: {mm.Item2}");

            Console.WriteLine($"Средняя отметка:{mid}");
            Console.WriteLine(Ab4.ToString());
            Console.WriteLine(Ab4.GetHashCode());
            Console.WriteLine(Ab4.GetType());
            Console.WriteLine(Ab4.Equals(Ab1));
            Console.WriteLine(Ab4.ToString());
            Console.WriteLine(Ab4.GetHashCode());
            Console.WriteLine(Ab4.GetType());

            Abiturient[] Abs = new Abiturient[5];
            Abs[0]=new Abiturient("Иванов", "Иван", "Иванович", "Ленина 29", 9083432);
            Abs[1] = new Abiturient("Фдедоров", "Алексей", "Иванович", "Свердлова 9", 239587);
            Abs[2] = new Abiturient("Петровов", "Александр", "Иванович", "Ленина 12", 1846372);
            Abs[3] = new Abiturient("Лавров", "Артем", "Игорьев", "Ленина 24", 00264738);
            Abs[4] = new Abiturient("Синев", "Мяун", "Барсикович", "Немига 4", 94536275);

            int count = 0;
            for(int i=0; i<Abs.Length; i++)
            {
                mm =Abs[i].MinMax(ref min,ref max);
                if (mm.Item1 <= 4)
                {
                    count++;
                    Console.WriteLine($"{count}. {Abs[i].LastName} {Abs[i].Name}");                    
                }
            }
            if(count==0) Console.WriteLine("Абитуриентов с неудовлетворительными отметками нет");
            count = 0; Console.WriteLine(" ");
            for (int i=0; i<Abs.Length; i++)
            {                
                if (Abs[i].TotalScore() > 55)
                {
                    count++;
                    Console.WriteLine($"{count}. {Abs[i].LastName} {Abs[i].Name}");
                }            
            }
            if(count==0) Console.WriteLine("Абитуриентов с суммой баллов больше 55 нет");

            Abiturient.StaticInfo();

            var someType = new { ln = "Lokk", n = "Ian", a = "Backer street 221b", tel = 8763542 };
            Console.WriteLine(someType.GetType());

        }
    }
}
