using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMadeEasy.Com.Models
{
    public static class Extension
    {
        public static DateTime IndiaDateTime(this DateTime dateTime)
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30);
        }
    }
    public enum EnumQuestionType
    {
        Single = 1,
        Multiple = 2,
        Integer = 3,
        Match = 4,
        PassageOrAssertion = 5
    }
    public enum EnumDificultyLevel
    {
        Easy = 1,
        Medium = 2,
        Hard = 3,
        VeryHard = 4
    }

    public class CheckBoxModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public bool IsChecked
        {
            get;
            set;
        }
    }
}