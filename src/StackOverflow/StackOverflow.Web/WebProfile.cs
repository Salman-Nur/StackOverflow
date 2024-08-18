using AutoMapper;
using StackOverflow.Domain.Entities;
using StackOverflow.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<ProfileViewModel, User>()
                .ReverseMap();

            CreateMap<ProfileEditModel, User>()
                .ReverseMap();

            CreateMap<QuestionDetailsModel, Question>()
                .ReverseMap();
        }
    }
}
