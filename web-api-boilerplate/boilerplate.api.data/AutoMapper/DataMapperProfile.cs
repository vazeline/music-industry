using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using System;
using System.Linq;

namespace boilerplate.api.data.AutoMapper
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            #region Musician
            CreateMap<MusicianCreateModel, Musician>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(dest => dest.DateCreated, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<MusicianUpdateModel, Musician>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForAllOtherMembers(opt => opt.Ignore());
            #endregion

            #region MusicLabel
            CreateMap<MusicLabelCreateModel, MusicLabel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(dest => dest.DateCreated, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<MusicLabelUpdateModel, MusicLabel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForAllOtherMembers(opt => opt.Ignore());
            #endregion

            #region Platform
            CreateMap<PlatformCreateModel, Platform>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(dest => dest.DateCreated, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PlatformUpdateModel, Platform>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForAllOtherMembers(opt => opt.Ignore());
            #endregion

            #region
            CreateMap<ContactCreateModel, Contact>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(s => s.FirstName))
                .ForMember(dest => dest.DateCreated, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(s => s.LastName))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(s => s.Title))
                .ForMember(dest => dest.Company, opts => opts.MapFrom(s => s.Company))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.Email))
                .ForMember(dest => dest.PhoneCell, opts => opts.MapFrom(s => s.PhoneCell))
                .ForMember(dest => dest.PhoneBusiness, opts => opts.MapFrom(s => s.PhoneBusiness))
                .ForMember(dest => dest.Fax, opts => opts.MapFrom(s => s.Fax))
                .ForMember(dest => dest.AddressLine1, opts => opts.MapFrom(s => s.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opts => opts.MapFrom(s => s.AddressLine2))
                .ForMember(dest => dest.City, opts => opts.MapFrom(s => s.City))
                .ForMember(dest => dest.State, opts => opts.MapFrom(s => s.State))
                .ForMember(dest => dest.Zip, opts => opts.MapFrom(s => s.Zip))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(s => s.IsActive))
                .ForMember(dest => dest.MusicianContacts, opts => opts.MapFrom(s => s.MusicianContacts.Select(x=>new MusicianContact() { MusicianId = x, DateCreated = DateTime.Now }).ToArray()))
                .ForMember(dest => dest.MusicLabelContacts, opts => opts.MapFrom(s => s.MusicLabelContacts.Select(x=>new MusicLabelContact() { MusicLabelId = x, DateCreated = DateTime.Now }).ToArray()))
                .ForMember(dest => dest.PlatformContacts, opts => opts.MapFrom(s => s.PlatformContacts.Select(x=>new PlatformContact() { PlatformId = x, DateCreated = DateTime.Now }).ToArray()))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ContactUpdateModel, Contact>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(s => s.FirstName))
                .ForMember(dest => dest.DateModified, opts => opts.MapFrom(s => DateTimeOffset.Now))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(s => s.LastName))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(s => s.Title))
                .ForMember(dest => dest.Company, opts => opts.MapFrom(s => s.Company))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.Email))
                .ForMember(dest => dest.PhoneCell, opts => opts.MapFrom(s => s.PhoneCell))
                .ForMember(dest => dest.PhoneBusiness, opts => opts.MapFrom(s => s.PhoneBusiness))
                .ForMember(dest => dest.Fax, opts => opts.MapFrom(s => s.Fax))
                .ForMember(dest => dest.AddressLine1, opts => opts.MapFrom(s => s.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opts => opts.MapFrom(s => s.AddressLine2))
                .ForMember(dest => dest.City, opts => opts.MapFrom(s => s.City))
                .ForMember(dest => dest.State, opts => opts.MapFrom(s => s.State))
                .ForMember(dest => dest.Zip, opts => opts.MapFrom(s => s.Zip))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(s => s.IsActive))
                .ForMember(dest => dest.MusicianContacts, opts => opts.MapFrom(s => s.MusicianContacts.Select(x => new MusicianContact() { MusicianId = x, DateCreated = DateTime.Now }).ToArray()))
                .ForMember(dest => dest.MusicLabelContacts, opts => opts.MapFrom(s => s.MusicLabelContacts.Select(x => new MusicLabelContact() { MusicLabelId = x, DateCreated = DateTime.Now }).ToArray()))
                .ForMember(dest => dest.PlatformContacts, opts => opts.MapFrom(s => s.PlatformContacts.Select(x => new PlatformContact() { PlatformId = x, DateCreated = DateTime.Now }).ToArray()))
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion
        }
    }
}
