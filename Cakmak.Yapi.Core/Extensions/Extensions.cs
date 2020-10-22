﻿using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Cakmak.Yapi.Core.Extensions
{
    public static class Extensions
    {
        #region ToUrlSlug
        /// <summary>
        /// String veriyi url formatında geçerli bir stringe dönüştürür
        /// </summary>
        /// <returns>Slug String'i Döndürür.</returns>
        public static string ToUrlSlug(this string text)
        {
            return Regex.Replace(Regex.Replace(Regex.Replace(text.Trim().ToLower().Replace(" ", "-").Replace("ö", "o").Replace(".", "").Replace("ç", "c").Replace("ş", "s").Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u"), @"\s+", " "), @"\s", ""), @"[^a-z0-9\s-]", "");
        }

        public static string ToFixed(this decimal number, uint decimals)
        {
            return number.ToString("N" + decimals);
        }

        public static string FormatImage(this string text, string mode, string resolution)
        {
            string temp = "";

            if (!string.IsNullOrWhiteSpace(mode))
                temp += mode + "/";

            if (!string.IsNullOrWhiteSpace(resolution))
                temp += resolution + "/";

            return "https://img.imageboss.me/" + temp + text;
        }

        public static List<string> GenerateSearchTerms(this List<string> currentTerms, string text)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(text))
                return result;

            text = text.Trim();

            result.Add(text.ToLower());
            result.Add(text.ToUpper(new CultureInfo("tr-TR")));
            result.Add(text.ToUpper(new CultureInfo("en-US")));
            result.Add(text.ToLower(new CultureInfo("tr-TR")));
            result.Add(text.ToLower(new CultureInfo("en-US")));
            if (text.Contains(" "))
                result.AddRange(text.Split(" "));
            result.Add(text.ToUrlSlug());

            var a = string.Join("|", result);
            result.AddRange(a.Split("|").Select(x => x.ToUrlSlug()).ToList());
            result.AddRange(a.Split("|").Select(x => x.ToUrlSlug().ToUpper()).ToList());
            result.Add(text.ToUrlSlug().Replace("-", " "));

            if (currentTerms == null)
                currentTerms = new List<string>();

            foreach (var item in result)
            {
                if (!currentTerms.Any(x => x == item))
                    currentTerms.Add(item);

            }

            return result;
        }

        public static string SplitPhoneMask(this string text)
        {
            return text.Replace("(", "").Replace(")", "").Replace(" ", "");
        }

        public static string ToMyCulture(this DateTime date)
        {
            return date.ToString("dd MMMM yyyy HH:mm", new CultureInfo("tr-TR"));
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper(new CultureInfo("tr-TR")) + input.Substring(1);
            }
        }


        public static string ToUniqueSlug(this string text)
        {
            var date = DateTime.Now;
            text = text + "-" + date.Year + date.Month + date.Day + date.Minute;
            text = text.ToUrlSlug();
            return text;
        }

        #endregion

        #region IsNumeric
        /// <summary>
        /// Bir değerin sayısal olup olmadığını kontrol eder.
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }
        #endregion

        #region ToJson
        /// <summary>
        /// Nesneyi json stringe çevirir
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Json String</returns>
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static StringContent ToStringContent(this object obj)
        {
            return new StringContent(obj.ToJson(), Encoding.UTF8, "application/json");
        }
        #endregion

        public static string FormatHour(this TimeSpan hour, string format = null, bool showSeconds = false)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                if (showSeconds)
                {
                    return hour.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    return hour.ToString(@"hh\:mm");
                }
            }


            return hour.ToString(format);
        }

        #region FromJson
        /// <summary>
        /// Json string'i nesneye çevirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsn"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string jsn)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsn);
        }
        #endregion

        #region IsValidEmailAddress
        /// <summary>
        /// Bir string'in email adresi olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>bool</returns>
        public static bool IsValidEmailAddress(this string s)
        {
            try
            {
                var temp = new MailAddress(s);

                string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                var regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(s))
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region IsValidPhoneNumber
        /// <summary>
        /// Bir string'in telfon numarası olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>bool</returns>
        public static bool IsValidPhoneNumber(this string s)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                    return false;

                string validPattern = @"\+[0-9]{4,14}$";
                var regex = new Regex(validPattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(s))
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region IsNullOrEmpty For Lists
        /// <summary>
        /// Bir listenin boş ve ya null olduğunu kontrol eder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();
        }
        #endregion

        #region Parser
        /// <summary>
        /// String türünü bir değer türüne Parse yapmayı dener yapamzsa null değer döndürür.
        /// 'Nullable' değerler geçerlidir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse<T>(this string value)
        {
            T result = default(T);
            if (!string.IsNullOrEmpty(value))
            {
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                result = (T)tc.ConvertFrom(value);
            }
            return result;
        }
        #endregion

        public static string GetEnumName<T>(this T value)
        {
            return Enum.GetName(typeof(T), value); ;
        }

        #region Age
        /// <summary>
        /// Doğum tarihinden yaşını hesaplar.
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        static public int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
            DateTime.Today.Month == dateOfBirth.Month &&
             DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Today.Year - dateOfBirth.Year;
        }
        #endregion

        #region IsValidUrl
        /// <summary>
        /// String 'URL' formatında ise true değer döndürür
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
                return rx.IsMatch(text);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Elapsed
        /// <summary>
        /// Bugüne kadar geçen süreyi verir.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime input)
        {
            return DateTime.Now.Subtract(input);
        }
        #endregion

        #region ToMD5
        /// <summary>
        /// String Veriyi MD5 Hash Değerini Üretir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMd5Hash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] originalBytes = ASCIIEncoding.Default.GetBytes(value);
                byte[] encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
            }
        }
        #endregion

        #region StripHTML
        public static string ToClearHtmlTags(this string text)
        {
            string textOnly = string.Empty;

            if (!string.IsNullOrEmpty(text))
            {
                Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                Regex compressSpaces = new Regex(@"[\s\r\n]+");
                textOnly = tagRemove.Replace(text, string.Empty);
                textOnly = compressSpaces.Replace(textOnly, " ");
                textOnly = Regex.Replace(textOnly, @"<[^>]+>|&nbsp;", "").Trim();
            }

            return textOnly;
        }
        #endregion

        #region IEnumerable Extensions
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
        #endregion

        public static string MakeLink(this string text)
        {
            if (!text.StartsWith("http://") && !text.StartsWith("https://"))
                text = "http://" + text;

            return text;
        }

    }
}