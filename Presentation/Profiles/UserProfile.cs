using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using BusinessLogic.DTO;
using AutoMapper;

namespace Presentation.Profiles
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
