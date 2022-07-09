using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    var studentRepository = new StudentRepository(databaseConfig);

    if(modelAction == "New")
    {
        var registration = args[2];
        if(studentRepository.ExistsById(registration))
        {
            Console.WriteLine($"Estudante com Id {registration} já existe");
        }
        else
        {
            var name = args[3];
            var city = args[4];

            var student = studentRepository.Save(new Student(registration, name, city));

            Console.WriteLine($"Estudante {student.Name} cadastrado com sucesso");
        }
    }

    if(modelAction == "Delete")
    {
        var registration = args[2];
        if(studentRepository.ExistsById(registration))
        {
            studentRepository.Delete(registration);
            Console.WriteLine($"Estudante {registration} removido com sucesso");
        }
        else
        {
            Console.WriteLine($"Estudante {registration} não encontrado");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        var registration = args[2];
        if(studentRepository.ExistsById(registration))
        {
            studentRepository.MarkAsFormed(registration);
            Console.WriteLine($"Estudante {registration} definido como formado");
        }
        else
        {
            Console.WriteLine($"Estudante {registration} não encontrado");
        }
    }

    if(modelAction == "List")
    {
        var students = studentRepository.GetAll();

        if(students.Any())
        {
            foreach(var student in students)
            {
                var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {former}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        }
    }

    if(modelAction == "ListFormed")
    {
       var students = studentRepository.GetAllFormed();

        if(students.Any())
        {
            foreach(var student in students)
            {
                var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {former}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        } 
    }

    if(modelAction == "ListByCity")
    {
        var city = args[2];
        var students = studentRepository.GetAllStudentByCity(city);

        if(students.Any())
        {
            foreach(var student in students)
            {
                var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {former}");
            }
        }
        else
        {

            Console.WriteLine("Nenhum estudante cadastrado");
        }
    }

    if(modelAction == "ListByCities")
    {
        var cities = new string[args.Length - 2];

        for(int i = 2; i < args.Length; i++)
        {
            cities[i-2] = args[i];
        }
        
        var students = studentRepository.GetAllByCities(cities);

        if(students.Any())
        {
            foreach(var student in students)
            {
                var former = student.Former ? "formado" : "não formado";
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, {former}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        }
    }

    if(modelAction == "Report")
    {
        var countBy = args[2];
        if(countBy == "CountByCities")
        {
            var groups = studentRepository.CountByCities();
            if(groups.Any())
            {
                foreach(var group in groups)
                {
                    Console.WriteLine($"{group.AttributeName}, {group.StudentNumber}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum estudante cadastrado");
            }
        }

        if(countBy == "CountByFormed")
        {
            var groups = studentRepository.CountByFormed();
            if(groups.Any())
            {
                foreach(var group in groups)
                {
                    var former = group.AttributeName == "1" ? "Formados" : "Não formados";
                    Console.WriteLine($"{former}, {group.StudentNumber}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum estudante cadastrado");
            }
        }
    }
}