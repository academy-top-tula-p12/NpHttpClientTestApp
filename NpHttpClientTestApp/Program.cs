List<Employee> employees = new List<Employee>()
{
    new(2, "Bobby", 28),
    new(5, "Timmy", 36),
    new(11, "Sammy", 24),
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => { return Results.Json(employees); });

app.MapGet("/{id}", (int id) =>
{
    Employee? e = null;
    foreach(var employee in employees)
        if (employee.Id == id)
        {
            e = employee;
            break;
        }

    if (e is not null)
        return Results.Json(e);
    else
        return Results.NotFound();
});

app.Run();




class Employee
{
    public int Id { get; }
    public string Name { set; get; }
    public int Age { set; get; }

    public Employee(int id, string name, int age)
    {
        this.Id = id;
        this.Name = name;
        this.Age = age;
    }
}