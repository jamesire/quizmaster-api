using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuizMaster.Storage.Enums
{
    public enum Difficulties
    {
        [Description("Any")]
        Any = 0,
        [Description("Easy")]
        Easy = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Hard")]
        Hard = 3
    }
}
