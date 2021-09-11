using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Scripts.QuizSystem;

public class Quiz
{
    [DataMember] public List<QuizItem> Questions { get; set; }

    //public static implicit operator Quiz(List<QuizItem> v)
    //{
    //    throw new NotImplementedException();
    //}
}