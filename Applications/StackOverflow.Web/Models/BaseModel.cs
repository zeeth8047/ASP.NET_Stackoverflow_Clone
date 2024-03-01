using Autofac;
using AutoMapper;
using DevSkill.Http;
using StackOverflow.Membership.DTOs;
using StackOverflow.Membership.Services;

namespace StackOverflow.Web.Models
{
	public abstract class BaseModel
	{
        protected UserManager? _userManager;
        protected IHttpContextAccessor? _httpContextAccessor;
        protected IMapper? _mapper;
        protected ILifetimeScope? _scope;
        public UserBasicInfoDto? UserInfo { get; private set; }
        public BaseModel()
        {

        }

        public BaseModel(UserManager userManager,
                         IHttpContextAccessor httpContextAccessor,
                         IMapper mapper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _userManager = _scope.Resolve<UserManager>();
            _httpContextAccessor = _scope.Resolve<IHttpContextAccessor>();
            _mapper = _scope.Resolve<IMapper>();
        }

        private async Task GetMemberInfoAsync()
        {
            var userName = _httpContextAccessor!.HttpContext!.User.Identity!.Name;
            var userInfo = await _userManager!.FindByEmailAsync(userName!);
            UserInfo = new UserBasicInfoDto();
            _mapper!.Map(userInfo, UserInfo);
        }

        public async Task GetUserInfoAsync()
        {
            if (!_httpContextAccessor!.HttpContext!.User!.Identity!.IsAuthenticated)
                throw new InvalidOperationException("Authenticated user required to access user information");

            if (!HasSessionKey("UserInfo"))
            {
                await GetMemberInfoAsync();
                SetSessionData<UserBasicInfoDto>("UserInfo", UserInfo!);
            }

            UserInfo = GetSessionData<UserBasicInfoDto>("UserInfo");
        }

        protected bool HasSessionKey(string key)
        {
            return _httpContextAccessor!.HttpContext!.Session.IsAvailable &&
                _httpContextAccessor.HttpContext.Session.Keys.Contains(key);
        }

        protected void SetSessionData<T>(string key, T data)
        {
            _httpContextAccessor!.HttpContext!.Session.Set(key, data);
        }

        protected T GetSessionData<T>(string key)
        {
            return _httpContextAccessor!.HttpContext!.Session.Get<T>(key);
        }
    }
}
