using AutoMapper;
using StenfordAPI.Models;
using static Stenford.Domain.DTO;

namespace StenfordAPI.Helper.Mapper.Account
{
	public static class AccountMapper
	{
		public static LoginResponseModel ToModel(this UserJwtDTO entity)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<UserJwtDTO, LoginResponseModel>();
			});
			IMapper mapper = config.CreateMapper();
			return mapper.Map<UserJwtDTO, LoginResponseModel>(entity);
		}
	}
}
