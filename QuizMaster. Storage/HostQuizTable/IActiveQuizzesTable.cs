using QuizMaster.Storage.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaster.Storage.ActiveQuizzesTable
{
    public interface IActiveQuizzesTable
    {
        public Task AddQuiz(string id, string username, Difficulties difficulty);
        public Task DeleteQuiz(string id);
        public Task JoinQuiz(string id, string username);
    }
}
