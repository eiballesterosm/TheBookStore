using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBookStore.DTO;
using TheBookStore.Models;

namespace TheBookStore.App_Start
{
    public static class MapsConfig
    {
        public static void Register()
        {
            //Map the book to the bookdto
            Mapper.CreateMap<Book, BookDto>()
                   .ForMember(b => b.Authors, m => m.MapFrom(a => a.Authors));

            Mapper.CreateMap<Author, BookAuthorsDto>()
                .ForMember(b => b.FullName, m => m.MapFrom(a => a.Name + " " + a.Surname))
                .ForSourceMember(b => b.Books, m => m.Ignore());

            Mapper.CreateMap<Book, AuthorBooksDto>();

            Mapper.CreateMap<Author, AuthorDto>()
                  .ForMember(b => b.FullName, m => m.MapFrom(a => a.Name + " " + a.Surname))
                  .ForMember(a => a.Books, m => m.MapFrom(b => b.Books))
                  .ForSourceMember(b => b.Books, m => m.Ignore());

            Mapper.CreateMap<Review, ReviewDto>()
                  .ForMember(r => r.BookId, m => m.MapFrom(s => s.BookId));
        }


        #region Extension Methods to convert types

        public static T To<T>(this object source)
        {
            return Mapper.Map<T>(source);
        }

        public static IEnumerable<T> To<T>(this IEnumerable<object> source)
        {
            return Mapper.Map<IEnumerable<T>>(source);
        }

        #endregion

    }
}