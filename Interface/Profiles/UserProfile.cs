using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models;
using Business.DTO;
using AutoMapper;

namespace Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();

            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
        }
    }
}
