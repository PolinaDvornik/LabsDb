using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.MVC.DAL.Database.Repositories;
using School.MVC.DAL.Models;
using System.Linq;

try
{
    var builder = new ConfigurationBuilder();

    builder.SetBasePath(@"D:\Study\Labs\БД\№2\LabsDb\ConsoleApp\");
    builder.AddJsonFile("appsettings.json");

    var config = builder.Build();

    string connectionString = config.GetConnectionString("DefaultConnection");

    var optionsBuilder = new DbContextOptionsBuilder<SchoolDbContext>();
    var options = optionsBuilder
        .UseSqlServer(connectionString)
        .Options;

    using (SchoolDbContext dbContext = new SchoolDbContext(options))
    {
        ClassRepository classRepository = new ClassRepository(dbContext);
        ClassTypeRepository classTypeRepository = new ClassTypeRepository(dbContext);
        ScheduleRepository scheduleRepository = new ScheduleRepository(dbContext);
        StudentRepository studentRepository = new StudentRepository(dbContext);
        SubjectRepository subjectRepository = new SubjectRepository(dbContext);
        TeacherRepository teacherRepository = new TeacherRepository(dbContext);

        int menuItem = -1;

        while (menuItem != 0)
        {
            Console.WriteLine(
                "\n----------MENU----------\n" +
                "1) Выборка данных из таблицы ClassTypes\n" +
                "2) Выборка данных из таблицы ClassTypes при условии 10 < Id < 35\n" +
                "3) Выборка данных из таблицы Classes с выводом среднего результата по полю StudentsCount\n" +
                "4) Выборка данных из таблицы Classes и данных из связанных с ней таблиц\n" +
                "5) Выборка данных из таблицы Classes и данных из связанных с ней таблиц, при условии Studentscount >= 30 и CreationYear = 2014\n" +
                "6) Вставка данных в таблицу ClassTypes\n" +
                "7) Вставка данных в таблицу Classes\n" +
                "8) Удаление данных из таблицы ClassTypes\n" +
                "9) Удаление данных из таблицы Classes\n" +
                "10) Обновить объект таблицы ClassTypes\n" +
                "0) Выход\n"
                );

            if (!int.TryParse(Console.ReadLine(), out menuItem))
            {
                throw new Exception("Некорректный ввод!");
            }

            switch (menuItem)
            {
                case 1:
                    foreach (var classType in await classTypeRepository.GetAll(false))
                    {
                        Console.WriteLine(classType.ToString());
                    }
                    break;
                case 2:
                    var allClassTypes = await classTypeRepository.GetAll(false);
                    foreach (var classType in allClassTypes.Where(ct => ct.Id > 10 && ct.Id < 35))
                    {
                        Console.WriteLine(classType.ToString());
                    }
                    break;
                case 3:
                    var allClasses = await classRepository.GetAll(false);
                    Console.WriteLine("Среднее арифметическое по StudentsCount = " + allClasses.Average(c => c.StudentsCount));
                    break;
                case 4:
                    foreach (var c in await classRepository.GetAll(false))
                    {
                        Console.WriteLine(c.ToString());
                    }
                    break;
                case 5:
                    allClasses = await classRepository.GetAll(false);
                    foreach (var c in allClasses.Where(c => c.StudentsCount >= 30 && c.CreationYear == 2014))
                    {
                        Console.WriteLine(c.ToString());
                    }
                    break;
                case 6:
                    Console.WriteLine("Введите название:");
                    string classTypeName = Console.ReadLine();

                    Console.WriteLine("Введите описание:");
                    string classTypeDescription = Console.ReadLine();

                    allClassTypes = await classTypeRepository.GetAll(false);
                    int nextId = allClassTypes.Max(ct => ct.Id) + 1;

                    await classTypeRepository.Create(new ClassType
                    {
                        Id = nextId,
                        Name = classTypeName,
                        Description = classTypeDescription
                    });

                    Console.WriteLine("Объект типа ClassType создан!");
                    break;
                case 7:
                    Console.WriteLine("Введите номер:");
                    int classNumber = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите кол-во учеников:");
                    int classStudentsCount = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите год создания:");
                    int classCreationYear = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите Id учителя, являющегося классным руководителем:");
                    int classTeacherId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введите Id типа класса:");
                    int classTypeId = Convert.ToInt32(Console.ReadLine());

                    allClasses = await classRepository.GetAll(false);
                    nextId = allClasses.Max(c => c.Id) + 1;

                    await classRepository.Create(new Class
                    {
                        Id = nextId,
                        Number = classNumber,
                        StudentsCount = classStudentsCount,
                        CreationYear = classCreationYear,
                        ClassroomTeacherId = classTeacherId,
                        ClassTypeId = classTypeId
                    });

                    Console.WriteLine("Объект типа Class создан!");
                    break;
                case 8:
                    Console.WriteLine("Введите Id объекта ClassType, который хотите удалить:");
                    int idForRemoving = Convert.ToInt32(Console.ReadLine());

                    var classTypeObject = await classTypeRepository.GetById(idForRemoving, false);

                    if (classTypeObject == null)
                    {
                        Console.WriteLine($"Записи с Id = {idForRemoving} не найдено!");
                    }
                    else
                    {
                        await classTypeRepository.Delete(classTypeObject);
                        Console.WriteLine($"Объект ClassType с Id = {idForRemoving} удалён");
                    }

                    break;
                case 9:
                    Console.WriteLine("Введите Id объекта Class, который хотите удалить:");
                    idForRemoving = Convert.ToInt32(Console.ReadLine());

                    var classObject = await classRepository.GetById(idForRemoving, false);

                    if (classObject == null)
                    {
                        Console.WriteLine($"Записи с Id = {idForRemoving} не найдено!");
                    }
                    else
                    {
                        await classRepository.Delete(classObject);
                        Console.WriteLine($"Объект Class с Id = {idForRemoving} удалён");
                    }

                    break;
                case 10:
                    Console.WriteLine("Введите Id объекта ClassType, который хотите обновить:");
                    var idForUpdate = Convert.ToInt32(Console.ReadLine());

                    classTypeObject = await classTypeRepository.GetById(idForUpdate, false);

                    if (classTypeObject == null)
                    {
                        Console.WriteLine($"Записи с Id = {idForUpdate} не найдено!");
                    }
                    else
                    {
                        Console.WriteLine($"Старое название - {classTypeObject.Name}. Введите новое название:");
                        classTypeName = Console.ReadLine();

                        Console.WriteLine($"Старое описание - {classTypeObject.Description}. Введите новое описание:");
                        classTypeDescription = Console.ReadLine();

                        classTypeObject.Name = classTypeName;
                        classTypeObject.Description = classTypeDescription;

                        await classTypeRepository.Update(classTypeObject);
                        Console.WriteLine($"Объект ClassType с Id = {idForUpdate} обновлён");
                    }

                    break;
                default:
                    Console.WriteLine("Такого пункта нет!");
                    break;
            }
        }
    };
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}