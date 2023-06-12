#region C#-Repeat
//using Ado.net.Models;

//List<Student> students=new List<Student>();

//CreateStudent("Isa");
//CreateStudent("Fexri");
//CreateStudent("Samir");
//ShowStudents();
//AddStudent();
//ShowStudents();

//void CreateStudent(string name) {
//    Student student = new Student();
//    student.Name = name;
//    students.Add(student);
//}


//void AddStudent()
//{
//    Console.WriteLine("Add name");
//    Student student = new Student();
//    student.Name = Console.ReadLine();
//    students.Add(student);
//}

//void ShowStudents()
//{
//    students.ForEach(stu =>
//    {
//        Console.WriteLine(stu.Name);
//    });
//}
#endregion

using System.Data.SqlClient;

string connection = "Server=localhost;Database=CodeAcademy;User ID=SA;Password=Ahad123@;Trusted_Connection=False;TrustServerCertificate=True;";
SqlConnection sqlConnection = new SqlConnection(connection);

bool IsRun = true;
Console.WriteLine("1. create");
Console.WriteLine("2. ShowAll");
Console.WriteLine("3. Remove");
Console.WriteLine("4. Update");
Console.WriteLine("5. GetById");
Console.WriteLine("6. ShowGroups");
int.TryParse(Console.ReadLine(),out int request);
while (IsRun)
{
    switch (request)
    {
        case 1:
            Console.Clear();
            CreateStudent();
            break;
        case 2:
            Console.Clear();
            ReadStudents();
            break;
        case 3:
            Console.Clear();
            RemoveStudent();
            break;
        case 4:
            Console.Clear();
            UpdateStudent();
            break;
        case 5:
            Console.Clear();
            ReadStudentById();
            break;
        case 6:
            Console.Clear();
            ShowGroups();
            break;
        case 0:
            return;
            
        default:
            Console.WriteLine("Add valid option");
            break;
    }

    Console.WriteLine("1. create");
    Console.WriteLine("2. ShowAll");
    Console.WriteLine("3. Remove");
    Console.WriteLine("4. Update");
    Console.WriteLine("5. GetById");
    Console.WriteLine("6. ShowGroups");

    int.TryParse(Console.ReadLine(), out request);
}


void CreateStudent()
{

    Console.WriteLine("Add name");
    string name = Console.ReadLine();
    Console.WriteLine("Add group id");

    int.TryParse(Console.ReadLine(), out int groupid);

    SqlCommand cmd = new SqlCommand($"Insert into Students values('{name}', 'Soltanov','{groupid}')", sqlConnection);
    sqlConnection.Open();
    cmd.ExecuteNonQuery();
    sqlConnection.Close();
}
void ReadStudents()
{
    SqlCommand cmd = new SqlCommand("Select * from Students", sqlConnection);
    sqlConnection.Open();
    var result = cmd.ExecuteReader();
    while (result.Read())
    {
        Console.WriteLine(result["Id"] + " " + result["Name"] + " " + result["Surname"]);
    }

    sqlConnection.Close();
}
void RemoveStudent()
{
    Console.WriteLine("Add id");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd1 = new SqlCommand($"Select * from Students where Id = {Id}", sqlConnection);
    sqlConnection.Open();
    SqlDataReader reader = cmd1.ExecuteReader();

    if (reader.Read())
    {
        sqlConnection.Close();
        sqlConnection.Open();

        SqlCommand cmd = new SqlCommand($"Delete from Students where Id = {Id}", sqlConnection);
        cmd.ExecuteNonQuery();
    }
    else
    {
        Console.WriteLine("Student is not found");
    }
    sqlConnection.Close();

}
void UpdateStudent()
{
    Console.WriteLine("Add id");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd1 = new SqlCommand($"Select * from Students where Id = {Id}", sqlConnection);
    sqlConnection.Open();
    SqlDataReader reader = cmd1.ExecuteReader();

    if (reader.Read())
    {
        sqlConnection.Close();
        sqlConnection.Open();
        Console.WriteLine("Update name");
        SqlCommand cmd = new SqlCommand($"UPDATE Students SET Name = '{Console.ReadLine()}', Surname = 'bla' WHERE Id = {Id}; ", sqlConnection);
        cmd.ExecuteNonQuery();
    }
    else
    {
        Console.WriteLine("Student is not found");
    }
    sqlConnection.Close();
}
void ReadStudentById()
{
    Console.WriteLine("Add id");
    int.TryParse(Console.ReadLine(), out int Id);

    SqlCommand cmd1 = new SqlCommand($"Select * from Students where Id = {Id}", sqlConnection);
    sqlConnection.Open();
    SqlDataReader reader = cmd1.ExecuteReader();
    bool status = false;
    while (reader.Read())
    {
        Console.WriteLine(reader["Id"] + " " + reader["Name"] + " " + reader["Surname"]);
        status = true;
    }
    if (!status)
    {
        Console.WriteLine("Student is not found");
    }
    sqlConnection.Close();
}
void ShowGroups()
{
    SqlCommand cmd = new SqlCommand("select g.Id 'GroupId', g.Name 'GroupName',s.Name 'StudentName',s.Surname from Groups as G LEFT JOIN Students as S ON g.Id=S.GroupId", sqlConnection);
    sqlConnection.Open();
    var result = cmd.ExecuteReader();
    while (result.Read())
    {
        Console.WriteLine(result["GroupId"] + " " + result["GroupName"]+result["StudentName"]+" "+result["Surname"]);
    }

    sqlConnection.Close();
}