using System.Net.Http.Json;

HttpClient client = new HttpClient();
//var data = await client.GetFromJsonAsync("https://localhost:7123/",
//                                        typeof(Employee));

//if(data is Employee employee)
//    Console.WriteLine($"Employee name: {employee.Name}, age: {employee.Age}");

//Employee? data = await client.GetFromJsonAsync<Employee>("https://localhost:7123/");
//Console.WriteLine($"Employee name: {data?.Name}, age: {data?.Age}");

while(true)
{
    Console.WriteLine("1 - All employees");
    Console.WriteLine("2 - Employee from Id");
    Console.WriteLine("0 - Exit");
    Console.Write("Your select: ");
    int select = Int32.Parse(Console.ReadLine());

    if (select == 0)
        break;
    else if(select == 1)
    {
        using var response = await client.GetAsync("https://localhost:7123/");
        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            List<Employee> employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
            Console.WriteLine("Employees:");
            foreach(var e in employees)
                Console.WriteLine($"\t{e.Id} {e.Name}, {e.Age}");
        }
    }
    else
    {
        Console.Write("Input id: ");
        int id = Int32.Parse(Console.ReadLine());
        string url = "https://localhost:7123/" + id.ToString();

        using var response = await client.GetAsync(url);
        if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            Console.WriteLine($"Employee with id {id} not found");
        else
        {
            Employee e = await response.Content.ReadFromJsonAsync<Employee>();
            Console.WriteLine($"\tEmployee with id {id}: {e.Name}, {e.Age}");
        }
    }


    
}

class Employee
{
    public int Id { get; set; }
    public string Name { set; get; }
    public int Age { set; get; }
}