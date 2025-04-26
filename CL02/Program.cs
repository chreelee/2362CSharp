/**
Christine Lee 
COP 2362
Student ID 2509320
04 25 2025

Description: Create menu and class libraries for handling input/output of the
gradebook program. It will include methods to read and write data to and from
the menu options 1. Tests, and 2. Students

*/

namespace CL02;

// declare string for path of student and test file so all methods can access


/*
Possible program states
- File to store text
- Text from that file
    File: Text| !Text
    Data File | !File
    No Data

*/
[Flags] // enumeration types as bit flags, to represent combo of choices
// create enum of binary flags for the possible states
public enum PROGRAM_STATE
{
    NOTHING = 0b_0000_0000, // binary values, NOTHING is 0
    DATA_STORE = 0b_0000_0001, // A file properly config
    DATA = 0b_0000_1000, // data in the file or data ready to write
    NO_CONSOLE_INPUT = 0b_1110_0000,//Flag for an action without input
    DATA_STORE_DATA_NOT_LOADED = DATA_STORE ^ DATA, // bool checks data store OR data
    DATA_NO_DATA_STORE = DATA ^ DATA_STORE,
    READ = 0b_1000_0000,
    WRITE = 0b_1100_0000,
    READ_WRITE = READ | WRITE, // flags read or write
    NO_DATA_TO_RW = READ_WRITE ^ DATA,
    NO_DATA_STORE_DATA_TO_RW = READ_WRITE ^ DATA_STORE
}

/*
<summary>
IMenu interface holds information about the menu selection for test, students,
give all students a test, have students take all test, and exit
</summary>
*/
public interface IMenuItem
{
    public PROGRAM_STATE CurrentState { get; } // get the program state enum
    public string Name { get; }
    public bool IsSubMenu { get; }
    /// <summary>
    /// SubScreens is a Stack data structure for Last-In-First-Out (LIFO) Stack <T>
    /// </summary>
    public Stack<IMenuItem> SubScreens { get; }
    public IMenuItem? ParentItem { get; }
}

// abstract class MenuItem will implement interface IMenuItem
public abstract class MenuItem : IMenuItem
{
    // attributes
    private PROGRAM_STATE _state = PROGRAM_STATE.NOTHING; // starts with nothing
    private string _name = string.Empty; // empty string
    private bool _subMenu; // declare boolean for submenu
    private List<IMenuItem> _actions; // create a list of IMenuItem object called _actions
    private IMenuItem _parent; // declare parent
    // create constructor
    protected MenuItem(string name, bool sub, IMenuItem? parent)
    {
        _name = name;
        _subMenu = sub;
        _actions = []; // empty list
        _parent = parent!; // parent cannot be null
    }
    // public properties to return, since above is private
    public PROGRAM_STATE CurrentState => _state; // set the state

    public string Name => _name;

    public bool IsSubMenu => _subMenu;

    public List<IMenuItem> PossibleActions => _actions;

    public IMenuItem ParentItem => _parent;

    public Stack<IMenuItem> SubScreens => throw new NotImplementedException();
    // required to implement this
}

// public class ConsoleMenuItem implements IMenuItem interface
public class ConsoleMenuItem : MenuItem
{
    private TextReader _in;
    private TextWriter _out;
    const string DASHTAB = "\t-----------------\t";
    // constructor
    public ConsoleMenuItem(TextReader reader, TextWriter writer, string name, bool sub, IMenuItem? parent) : base(name, sub, parent)
    {
        _in = reader; // for reading input
        _out = writer; // for writing out
    }
    // method PrintName() will use insert the dash tab, name, then another dash tab
    public void PrintName()
    {
        _out.WriteLine($"{DASHTAB} | {Name} | {DASHTAB}");
    }

    // method MenuSelection() will print the menu details and get parent and sub choice
    public static void MenuSelection()
    {
        // Display menu with options
        Console.WriteLine($"CLI Teacher Program ");
        Console.WriteLine($"=============== MENU ===============");
        Console.WriteLine($"Enter the menu number to view further options.");
        Console.WriteLine($"[1] TESTS");
        Console.WriteLine($"[2] STUDENTS");
        Console.WriteLine($"[3] GIVE ALL STUDENTS A TEST");
        Console.WriteLine($"[4] HAVE STUDENTS TAKE ALL TESTS");
        Console.WriteLine($"[5] EXIT");

        // get user's choice as menuChoice and convert to int to use
        int menuChoice = Convert.ToInt32(Console.ReadLine());

        // then, based on their choice, create a switch for additional options
        // while endless loop for only acceptable choices
        while ((menuChoice == 1) || (menuChoice == 2) || (menuChoice == 3) || (menuChoice == 4) || (menuChoice == 5))
        {   // Create a switch statement for menuChoice
            switch (menuChoice)
            {
                case 1: // choice for tests
                    TestMenu();
                    break;

                case 2: // choice for students
                    StudentMenu();
                    break;

                case 3: // choice for give all students a test
                    GiveAllStudentsTestMenu();
                    break;

                case 4: // choice for have student take all tests
                    StudentTakeAllTestMenu();
                    break;

                case 5: // exit
                    Environment.Exit(0); // exit 
                    break;

            }

        }


    }





    // will print the submenu details and get sub choice
    public static void TestMenu()
    {
        

        Console.WriteLine($"=============== TEST MENU ===============");
        Console.WriteLine($"Enter the menu number.");
        Console.WriteLine($"[1] ADD TEST");
        Console.WriteLine($"[2] EDIT TEST");
        Console.WriteLine($"[3] DELETE TEST");
        Console.WriteLine($"[4] VIEW TEST");
        Console.WriteLine($"[5] EXIT");

        int menuChoice = Convert.ToInt32(Console.ReadLine()); // get choice

        while ((menuChoice == 1) || (menuChoice == 2) || (menuChoice == 3) || (menuChoice == 4) || (menuChoice == 5))
        {   // Create a switch statement
            switch (menuChoice)
            {
                case 1: // choice for add test, MINIMUM REQUIREMENT
                    AddTestPaper();
                    break;

                case 2: // choice for edit test, not yet via assignment
                    break;

                case 3: // choice for delete test, not yet via assignment

                    break;

                case 4: // choice for view test, MINIMUM REQUIREMENT
                    ViewTestPaper();
                    break;

                case 5: // exit
                    Environment.Exit(0); // exit 
                    break;

            }

        }
    }

    // method addTestPaper will add a TestPaper object to the data structure
    private static void AddTestPaper()
    {
        
    }

    // method ViewTestPaper will view a TestPaper object
    private static void ViewTestPaper()
    {
        
    }
    // method that displays the Students submenu and get choice
    private static void StudentMenu()
    {
        Console.WriteLine($"=============== STUDENT MENU ===============");
        Console.WriteLine($"Enter the menu number.");
        Console.WriteLine($"[1] ADD STUDENT");
        Console.WriteLine($"[2] EDIT STUDENT");
        Console.WriteLine($"[3] DELETE STUDENT");
        Console.WriteLine($"[4] GIVE TEST TO STUDENT");
        Console.WriteLine($"[5] VIEW SCORES");
        System.Console.WriteLine($"[6] EXIT");

        int menuChoice = Convert.ToInt32(Console.ReadLine()); // get choice

        while ((menuChoice == 1) || (menuChoice == 2) || (menuChoice == 3) || (menuChoice == 4) || (menuChoice == 5) || (menuChoice == 6))
        {   // Create a switch statement
            switch (menuChoice)
            {
                case 1: // choice for add student, MINIMUM REQUIREMENT
                    AddStudent();
                    break;

                case 2: // choice for edit student, not yet via assignment
                    break;

                case 3: // choice for delete student, not yet via assignment

                    break;

                case 4: // choice for give test, not yet via assignment

                    break;

                case 5: // choice for view scores, REQUIRED
                    // from Student class, uses string[] TestsTaken to view tests
                    ViewStudentTests();
                    break;

                case 6: // exit
                    Environment.Exit(0); // exit 
                    break;
            }

        }
    }
// AddStudent will create a new Student object and write to data structure
    private static void AddStudent()
    {
        
    }

// viewStudentsTests will display the string[] TestsTaken
    private static void ViewStudentTests()
    {
        throw new NotImplementedException();
    }

    // this program will give all students a test
    private static void GiveAllStudentsTestMenu()
    {
        // not required for program yet
        System.Console.WriteLine($"Select a Test...");
        // will read a set of TestPaper objects and write out to console
    }

// this program will have a student take all available tests
        private static void StudentTakeAllTestMenu()
    {
        System.Console.WriteLine($"Select a student to take all tests...");
    }
}

//// test methods
// class FilePath will hold the file paths for all methods to access
class FilePath
{
    public static string StudentsFilePath = "students.txt";
    public static string TestsFilePath = "tests.txt";
}

// class StudentsMenuMethods will hold methods for read writing student
class StudentsMenuMethods
{
    // store students in a list
    private static List<Student> _students = [];

    // method LoadStudent loads the student file, if it exists
    public static void LoadStudents()
    {
        if (!File.Exists(FilePath.StudentsFilePath)) // if file doesn't exist, return
        {
            return;
        }

    }

}


//////////////////////// BELOW IS CODE FROM ASSIGNMENT 1

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