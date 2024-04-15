using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct QuestionData
{
    public string Theme;
    public List<Question> Questions;

    public QuestionInfo GetQuestionInfoByTheme(string theme)
    {
        foreach (Question question in Questions)
        {
            if (question.Theme == theme)
            {
                return new QuestionInfo(question.Theme, question.QuestionText, question.AnswerIndex, question.CorrectAnswer);

            }
        }
        return new QuestionInfo();
    }

    public struct Question
    {
        public string Theme;
        public string QuestionText;
        public Dictionary<int, string> AnswerIndex;
        public int CorrectAnswer; 
    }

}

[Serializable]
public struct QuestionInfo
{
    public string Theme;
    public string QuestionText;
    public Dictionary<int, string> AnswerIndex;
    public int CorrectAnswer;

    public QuestionInfo(string theme, string questionText, Dictionary<int, string> answerIndex, int correctAnswer)
    {
        Theme = theme;
        QuestionText = questionText;
        AnswerIndex = answerIndex;
        CorrectAnswer = correctAnswer;
    }
}


