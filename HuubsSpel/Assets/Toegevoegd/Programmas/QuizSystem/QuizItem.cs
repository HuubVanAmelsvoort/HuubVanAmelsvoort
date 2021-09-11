using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scripts.QuizSystem
{
    [DataContract]
    public class QuizItem
    {
        public string Question { get; set; }


        public IList<string> Answers { get; set; }


        public string CorrectAnswer { get; set; }

        public string Info { get; set; }

        public string Reference { get; set; }

        public string Category { get; set; }

        public string Hint { get; set; }
        public override string ToString()
        {
            string res = Question;
            foreach (var item in Answers)
            {
                res = res + "\n  " + item;
            }

            return res;
        }

        public string getAnswerTextjabadabadooooooo()
        {
            foreach (string answer in Answers)
            {
                if (char.ToUpper(answer[0]) == char.ToUpper(CorrectAnswer[0]))
                {
                    return answer;
                }
            }
            return "";
        }
    }
}