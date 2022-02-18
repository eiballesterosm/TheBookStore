using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TheBookStore.DTO;

namespace TheBookStore.Infrastructure
{
    public static class Extensions
    {
        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };

        public static string Flatten(this BookDto bookDto)
        {
            var flatBook = new
            {
                Id = bookDto.Id,
                Title = bookDto.Title.Escaped(),
                Authors = bookDto.Authors.AsString().Escaped()
            };

            return string.Format("{0},{1},{2}", flatBook.Id, flatBook.Title, flatBook.Authors);
        }

        public static string AsString(this IEnumerable<BookAuthorsDto> authors)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < authors.Count(); i++)
            {
                sb.Append(authors.ElementAt(i).FullName);
                if (i < authors.Count() - 1)
                {
                    sb.Append(" and ");
                }
            }

            return sb.ToString();
        }

        private static string Escaped(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            string field = obj.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                return string.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else
            {
                return field;
            }
        }
    }
}