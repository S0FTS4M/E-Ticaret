using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ETicaret
{
    static class Validator
    {
        static public bool ValidPhone(string text, int max)
        {
            if (text.Length == 0)
                return true;
            else if (text.Length == max)
                return true;

            return false;
        }

        static public bool ValidPassWord(string text)
        {
            return text.Length <= 8 && text.Length > 3;
        }

        static public bool ValidEmail(string text)
        {
            if (text.Length < 5)
                return false;
            string[] mail = text.Split('@');
            if (mail.Length != 2)
                return false;
            else
            {
                return ValidUserName(mail[0]) && checkMailList(mail[1]);
            }
        }

        static public bool checkMailList(string v)
        {
            return true;
        }

        static public bool ValidUserName(string text)
        {
            int a=0;
            return text.Length > 4 && text.IndexOfAny((StringLiterals.SpecialChars + StringLiterals.TurkishChars).ToCharArray()) < 0 && int.TryParse(text[0].ToString(), out a)==false;
        }
    }
}