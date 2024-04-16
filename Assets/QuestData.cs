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
                return new QuestionInfo(question.Theme, question.QuestionText, question.AnswerIndex, question.CorrectAnswer, question.Explanation);
            }
        }
        return new QuestionInfo();
    }

    [Serializable]
    public struct Question
    {
        public string Theme;
        public string QuestionText;
        public Dictionary<int, string> AnswerIndex;
        public int CorrectAnswer;
        public string Explanation; 
    }
}

[Serializable]
public struct QuestionInfo
{
    public string Theme;
    public string QuestionText;
    public Dictionary<int, string> AnswerIndex;
    public int CorrectAnswer;
    public string Explanation;

    public QuestionInfo(string theme, string questionText, Dictionary<int, string> answerIndex, int correctAnswer, string explanation)
    {
        Theme = theme;
        QuestionText = questionText;
        AnswerIndex = answerIndex;
        CorrectAnswer = correctAnswer;
        Explanation = explanation;
    }
}
