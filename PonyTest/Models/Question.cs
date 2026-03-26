namespace PonyTest.DB;

public class Question
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public string QuestionText { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public char CorectOption { get; set; }
}