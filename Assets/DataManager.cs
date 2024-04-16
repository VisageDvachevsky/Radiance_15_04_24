using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public QuestionData questionData;

    void Start()
    {
        InitializeQuestionData();
    }

    void InitializeQuestionData()
    {
        questionData = new QuestionData
        {
            Theme = "������� � ������ ���������",
            Questions = new List<QuestionData.Question>
            {
                new QuestionData.Question
                {
                    Theme = "embankment",
                    QuestionText = "����� � ��� ��� ����� ���� � ��������������� �������������� ����������?",
                    AnswerIndex = new Dictionary<int, string>
                    {
                        { 1, "������ I � 1722 ���� �� ����� ��� ���������� � ������ ��������� � ������������ ������ ���������������" },
                        { 2, "� 1834 ���� �������� I ����� ��������� ������" },
                        { 3, "� 1935 ���� �. ��������, ��� ��� ������ � ������ ��� ����������" }
                    },
                    CorrectAnswer = 2,
                    Explanation = "��, ������ � ������ ���������, ������������ ������������ ����� ������ � �������� ��� ����������� � ������� � ��������� �����. ���������� ���� �������� ���������� � �����������, � ����� ��������� ��������� ������ �� �������� ������ ������������."
                },
                new QuestionData.Question
                {
                    Theme = "chkalov",
                    QuestionText = "���������� ��������. ������ � �������� �������� ����� ����� �� ����� ������?",
                    AnswerIndex = new Dictionary<int, string>
                    {
                        { 1, "����� � ������� ��� ������ ����� ������" },
                        { 2, "�������� ������� � ���� ���, ����� ������������ ���� ������" },
                        { 3, "����� ��������� � ������ � ������ � ����� �� 40-�����" }
                    },
                    CorrectAnswer = 3,
                    Explanation = "��� ����� ������ �������� ���� � �������� �������������� �� ����� � 1918-1919 ����� � ��������� ������� � ��������� ��� ������������ �� ������ ������� �����. � 1967 ���� ����� ���� ������ ������ ����� � �������� �������� �������� �� �����. � 1985 ����, � ����� 40-����� ������ � ������� ������������� �����, �� ��� ���������� �� ������ � ���� ���������."
                },
                new QuestionData.Question
                {
                    Theme = "mansion",
                    QuestionText = "���-������� �. �. ��������� ������� - ������������� ��������������� �������������� �����. ��� ���������� ����� ��������� ������� ����� � ������ ��� � ��������� ���������?",
                    AnswerIndex = new Dictionary<int, string>
                    {
                        { 1, "���������� ������ � ������������ �. ����������. �� ������� 7�6 �, ������� ���������� ���������." },
                        { 2, "������-������� �. ���������. ������ ������ ���������, ����������, �� �� �� ���� �������." },
                        { 3, "����������� ����� �������� ����������. ������������ �� ����� ���� 1665 ����, �� �� ��-�� ����� � ��������� ���������." }
                    },
                    CorrectAnswer = 1,
                    Explanation = "�� �������� (7�6 �) � ���������� ��� �������� ������ �����, ������� ���������� ���������, ������� ���� ��������� ���������� ��� ���."
                },
                new QuestionData.Question
                {
                    Theme = "university",
                    QuestionText = "��� ����������� ��������� ��������������� ���������, ������ ������ ����, ������, �������?",
                    AnswerIndex = new Dictionary<int, string>
                    {
                        { 1, "��������� ����������� �����������, ��������� '����', ������� �� ��� ��� � ������������." },
                        { 2, "������������ ������ �� ���������� ������� ������� �������, ������ ���������." },
                        { 3, "��������� ����� �� ��������� ������� � ������������, �� ������������� ������������." }
                    },
                    CorrectAnswer = 3,
                    Explanation = "��������� ���������� �������� � ��������� ����������������, ��������� ����� �����, ��� ������, ������, ������ � �� ����������� �����������, ����� ��� ������."
                }
            }
        };
    }
}