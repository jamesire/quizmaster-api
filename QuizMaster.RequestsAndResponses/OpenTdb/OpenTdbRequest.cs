using QuizMaster.RequestsAndResponses.Enums;

namespace QuizMaster.RequestsAndResponses.OpenTdb
{
    public class OpenTdbRequest
    {
        public int NumberOfQuestionsToGenerate { get; set; } = 1;
        public Categories Category { get; set; } = Categories.AnyCategory;
        public Difficulties Difficulty { get; set; } = Difficulties.Any;
        public QuestionTypes Type { get; set; } = QuestionTypes.Any;
    }
}
