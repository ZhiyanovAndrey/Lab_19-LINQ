using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_19_LINQ
{
    /* Модель компьютера  характеризуется кодом  и названием  марки компьютера, типом  процессора,  
     * частотой работы  процессора,  объемом оперативной памяти, объемом жесткого диска, 
     * объемом памяти видеокарты, стоимостью компьютера в условных единицах и количеством экземпляров,
     * имеющихся в наличии.
     * Создать список, содержащий 6-10 записей с различным набором значений характеристик.

 Определить:
 - все компьютеры с указанным процессором. Название процессора запросить у пользователя;
 - все компьютеры с объемом ОЗУ не ниже, чем указано.Объем ОЗУ запросить у пользователя;
 - вывести весь список, отсортированный по увеличению стоимости;
 - вывести весь список, сгруппированный по типу процессора;
 - найти самый дорогой и самый бюджетный компьютер;
 - есть ли хотя бы один компьютер в количестве не менее 30 штук?*/

    class PC
    {

        public int CodPC { get; set; }
        public string NamePC { get; set; }
        public string Processor { get; set; }
        public float Frequency { get; set; }
        public int RAM { get; set; }
        public int HDD { get; set; }
        public int Video { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

    }
        class Program
    {
        static void Main(string[] args)
        {
            List<PC> pc = new List<PC>()
            {
                new PC(){CodPC=1, NamePC="MSI GF65", Processor="Intel Core i5", Frequency=2.5F, HDD=512, RAM=8, Video=6144, Price=89999, Stock=5},
                new PC(){CodPC=2, NamePC="MSI GF63 Thin", Processor="Intel Core i5", Frequency=2.7F, HDD=1024, RAM=16, Video=4096, Price=94390, Stock=51},
                new PC(){CodPC=3, NamePC="MSI GF63 Thin", Processor="Intel Core i7", Frequency=2.3F, HDD=512, RAM=16, Video=4096, Price=96089, Stock=18},
                new PC(){CodPC=4, NamePC="ASUS Vivobook Pro 15", Processor="AMD Ryzen 7", Frequency=3.2F, HDD=512, RAM=16, Video=4096, Price=93190, Stock=4},
                new PC(){CodPC=5, NamePC="ASUS VivoBook Pro 15", Processor="Intel Core i5", Frequency=3.1F, HDD=512, RAM=8, Video=4096, Price=85999, Stock=1},
                new PC(){CodPC=6, NamePC="ASUS TUF Dash F15", Processor="Intel Core i5", Frequency=2.7F, HDD=512, RAM=16, Video=4096, Price=91999, Stock=47},
                new PC(){CodPC=7, NamePC="DELL G3 3500", Processor="Intel Core i7", Frequency=2.6F, HDD=512, RAM=8, Video=6144, Price=93290, Stock=144},
                new PC(){CodPC=8, NamePC="DELL G3 3500", Processor="Intel Core i7", Frequency=2.6F, HDD=512, RAM=8, Video=6144, Price=93290, Stock=556},
                new PC(){CodPC=9, NamePC="Acer Predator Helios 300", Processor="Intel Core i5", Frequency=2.5F, HDD=512, RAM=8, Video=4096, Price=93170, Stock=1}
            };

            Console.WriteLine("Введите компьютеры с указанным процессором");
            string processor = Console.ReadLine();
            List<PC> pc1 = pc.Where(x => x.Processor == processor).ToList(); //синтаксис на основе методов расширения
                                                                             //NamePC c большой буквы свойство, namePC переменная
                                                                             //=> лямбда выражение. Для всех x внутри списка
                                                                             //вызываем метод ToList, т.к. where возвращает коллекцию
            Print(pc1);

            Console.WriteLine("Показать объем ОЗУ не ниже, чем указано");
            int ram = Convert.ToInt32(Console.ReadLine());
            List<PC> pc2 = pc.Where(x => x.RAM >= ram).ToList();
            Print(pc2);

            Console.WriteLine("Вывести отсортированный по цене список");
            List<PC> pc3 = pc.OrderBy(x => x.Price).ToList();
            Print(pc3);

            IEnumerable<IGrouping<string, PC>> pc4 = pc.GroupBy(x => x.Processor);
            foreach (IGrouping<string, PC> gr in pc4)
            {
                Console.WriteLine(gr.Key); //вывод ключа круппировки
                foreach (PC e in gr)
                {
                    Console.WriteLine($"{e.CodPC} {e.NamePC}, {e.Processor}, {e.Frequency} ГГц, {e.RAM} ГБ," +
                    $" {e.HDD} ГБ, {e.Video} ГБ, {e.Price} у.е."); //вывод всего остального
                }
            }

            PC pc5 = pc.OrderByDescending(x => x.Price).FirstOrDefault(); //вывод первой строки сортировки в отличии от First() вернет 0 при пустом списке
            Console.WriteLine("Самый дорогой компьютер: ");
            Console.WriteLine($"{pc5.CodPC} {pc5.NamePC}, {pc5.Processor}, {pc5.Frequency} ГГц, {pc5.RAM} ГБ," +
                    $" {pc5.HDD} ГБ, {pc5.Video} ГБ, {pc5.Price} у.е. \nв наличии {pc5.Stock} шт.");

            PC pc6 = pc.OrderByDescending(x => x.Price).LastOrDefault(); //вывод последней строки сортировки
            Console.WriteLine("Самый бюджетный компьютер: ");
            Console.WriteLine($"{pc6.CodPC} {pc6.NamePC}, {pc6.Processor}, {pc6.Frequency} ГГц, {pc6.RAM} ГБ," +
                    $" {pc6.HDD} ГБ, {pc6.Video} ГБ, {pc6.Price} у.е. \nв наличии {pc6.Stock} шт.");


            Console.WriteLine("Есть компьютер в количестве не менее 30 штук: " + pc.Any(x => x.Stock >= 30)); //проверяет есть ли хоть один

            Console.ReadKey();

        }

        static void Print(List<PC> pc) //метод для вывода всей коллекции
        {
            foreach (PC e in pc)
            {
                Console.WriteLine($"{e.CodPC} {e.NamePC}, {e.Processor}, {e.Frequency} ГГц, {e.RAM} ГБ," +
                    $" {e.HDD} ГБ, {e.Video} ГБ, {e.Price} у.е. \nв наличии {e.Stock} шт.");
            }
            Console.WriteLine();
        }
    }
}
