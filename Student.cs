// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography.X509Certificates;

Dictionary<string,List<string>>   StudentCourse = new Dictionary<string,List<string>>();

(string RollNo, string Course)[] data =
{
    ("A","Math"),
    ("A","Science"),
    ("B","Math"),
    ("B","Social Science")
};

foreach(var item in data)
{
    string RollNumber = item.RollNo;
    string Course = item.Course;

    if(!StudentCourse.ContainsKey(RollNumber))
    {
        StudentCourse[RollNumber] = [];
    }
    StudentCourse[RollNumber].Add(Course);

}
var RollNumbers= StudentCourse.Keys.ToList();



Dictionary<string,List< string>> dict = new Dictionary<string,List< string>>();

for(int i=0;i<RollNumbers.Count;i++)
{
    for(int j=i+1;j< RollNumbers.Count;j++)
    {
        string first = RollNumbers[i];
        string second = RollNumbers[j];

        var Students = first + "," + second;
        var FirstStudentCourse = StudentCourse[first].ToList();

        var SecondStudentCourse = StudentCourse[second].ToList();

        var commonCourse = FirstStudentCourse.Intersect(SecondStudentCourse).ToList();

        dict[Students]= commonCourse;

    }
}


foreach (var item in dict)
{
    Console.WriteLine(item.Key+"->"+ string.Join(",",item.Value));
    

}


Console.ReadLine();
Console.WriteLine("Hello, World!");
