/**
Christine Lee 
COP 2362
Student ID 2509320
04 06 2025

Description: Class Library that allows teachers to create multiple choice tests
called TestPaper.
TestPaper class implements ITestPaper interface with these properties:
    string Subject
    String[] MarkScheme
    string PassMark
Student class implements IStudent interface with these properties:
    string[] TestTaken
    void TakeTest(ITestPaper paper, string[] answers)

*/
namespace CL01;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello, World!");
//     }
// }

/*
<summary>
ITestPaper interface describes a teacher's master test with
the subject, markscheme (answer key), and the passmark (passing grade)
*/
public interface ITestPaper
{
    string Subject { get; }
    string[] MarkScheme { get; } // answer key
    string PassMark { get; } // denote the passing mark of the paper
}
/*
<summary>
IStudent interface describes a string of all of their tests and the results
and a method called TakeTest that takes in the paper and the answers for results
</summary>
*/
public interface IStudent
{
    string[] TestsTaken { get; }
    // void method TakeTest that takes parameters paper and array of answers
    void TakeTest(ITestPaper paper, string[] answers);
}

// public enum to hold the subjects that won't change
/*
<summary>
enum Subject holds the subjects of the test. Since they'll be constant
</summary>
*/
public enum Subject
{
    Chemistry, // implicit integer = 0
    Computing,
    Humanities,
    Mathematics
}

/*
<summary>
Class TestPaper holds the test's subject, markscheme, and passing mark as
protected so the markscheme and passing mark can't be changed easily.
Implements ITestpaper
</summary>
*/
// class TestPaper that implements ITestPaper
public class TestPaper : ITestPaper
{
    private Subject _subject; // enum Subject

    private string[] _markScheme;

    private string _passMark;

    // protected method that takes in subject markscheme and passmark
    protected TestPaper(Subject subject, string[] markScheme, string passMark)
    {
        _subject = subject;
        _markScheme = markScheme;
        _passMark = passMark; // passMark is a STRING, expect "60%" 
    }

    public string Subject => _subject.ToString(); // Subject type to string

    public string[] MarkScheme => _markScheme;

    public string PassMark => _passMark;
}

// class Student implements IStudent
/*
<summary>
Class Student takes in a student's name and studentID, and implements
IStudent. TestTaken contains a string of all the tests taken and their result
TakeTest compares the student's string of answers and compares it to the
markscheme and returns the result of the student's test in a list.
</summary>
*/
public class Student(string name, int studentID) : IStudent
{
    // student properties
    public string Name { get; set; } = name;
    public int StudentID { get; set; } = studentID;
    // public string[] TestsTaken gets all the test results in the string List array
    // and ORDERS BY enum Subject
    public string[] TestsTaken // moved up to other properties
    {
        get
        {
            // if NO tests have been taken, return saying "no tests taken"
            if (_testsTaken.Count == 0)
            {
                return ["No tests taken"]; // quick fix
            }
            // else, return the tests ordered by the enum Subject as a string array
            // Subject is before the delimiter of : before paper.Subject
            // quickfix collections statement
            return [.. _testsTaken.OrderBy(test => test.Split(':'))];
        }
    }

    // create a private list of string objects to store each return of TestsTaken()
    // and not allow it to be seen
    private List<String> _testsTaken = [];

    // TakeTest method takes parameters paper and string[] answers
    public void TakeTest(ITestPaper paper, string[] answers)
    {
        // create a counter for correctAnswers, and declare testResult fail/pass string
        int correctCount = 0;
        string testResult = "";

        // create a for loop to iterate thru the length of the smallest # of answers 
        // compared to the markScheme using Math.Min(int, int)
        // https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-9.0
        for (int i = 0; i < Math.Min(paper.MarkScheme.Length, answers.Length); i++)
        {
            // if the element of markscheme[i] EQUALS the element of answers[i], 
            // then the answer is CORRECT, and increment correctCount
            if (paper.MarkScheme[i] == answers[i])
            {
                correctCount++;
            }
        }

        // once done iterating thru answers vs keys, calculate correctCount against the
        // number of questions in the markscheme, multiply by 100 to get the percentage
        // calculate inside as double, then round and cast to an int for whole number
        int percentGrade = (int)(Math.Round((double)correctCount / paper.MarkScheme.Length * 100));

        // since PassMark is a string, you need to remove the character of % and make an int
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
        int passMark = int.Parse(paper.PassMark.Trim('%'));

        // determine the result of pass or fail with boolean for true/false
        // if percentgrade is GREATER THAN or equal to passmark, is TRUE and says pass
        // else, percentgrade is not and says fail
        testResult = (percentGrade >= passMark) ? "Passed!" :
        "Failed!";

        // add a string that contains results of the test to the _testTaken list of strings
        _testsTaken.Add($"Subject {paper.Subject}: {testResult} ({percentGrade}%)");
    }
}
