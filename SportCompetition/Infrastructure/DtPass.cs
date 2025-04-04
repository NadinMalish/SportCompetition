using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SportCompetition.Infrastructure
{
    public class DtPass
    {
        // -- Шифрование пароля --
        public static string HashPass(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(hash);
        }


        //  -- Проверка пароля --
        public static string ValidPass(string pass)
        {
            string st = "";
            string res = "";
            if (pass.Length < 8)
            {
                res = "Пароль должен быть не менее 8 символов";
                st = "8";
            }

            if (pass.Length > 32)
            {
                res = "Пароль не должен превышать 32 символа";
                st = "32";
            }


            if (res == "")
            {
                string reg0 = "[^0-9_a-zA-Z~!#*_$&%()+-`]";
                if (Regex.IsMatch(pass, reg0))
                {
                    res = "Пароль содержит недопустимые символы";
                    st = "n";
                }
            }

            if (res == "")
            {
                string reg = "^[a-zA-z]";
                if (!Regex.IsMatch(pass, reg))
                {
                    res = "Пароль должен начинаться с латинской буквы";
                    st = "1";
                }
            }

            if (res == "")
            {
                int n = 0;
                string reg1 = "[a-z]";
                string reg2 = "[A-Z]";
                string reg3 = "[0-9]";
                string reg4 = "[^a-zA-Z0-9]";
                if (Regex.IsMatch(pass, reg1)) { n++; }
                if (Regex.IsMatch(pass, reg2)) { n++; }
                if (Regex.IsMatch(pass, reg3)) { n++; }
                if (Regex.IsMatch(pass, reg4)) { n++; }
                if (n < 4)
                {
                    res = "Пароль должен содержать прописные и заглавные буквы, цифры и спецсимволы";
                    st = "4";
                }
            }

            //return res;
            return st;
        }


        public static string KodEr(string st)
        {
            string res = "";

            if (st == "8")
            { res = "Пароль должен быть не менее 8 символов"; }

            if (st == "32")
            { res = "Пароль не должен превышать 32 символа"; }

            if (st == "n")
            { res = "Пароль содержит недопустимые символы"; }

            if (st == "1")
            { res = "Пароль должен начинаться с латинской буквы"; }

            if (st == "4")
            { res = "Пароль должен содержать прописные и заглавные буквы, цифры и спецсимволы"; }

            return res;
        }


    }
}
