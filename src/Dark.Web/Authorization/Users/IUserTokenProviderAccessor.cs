
using Dark.Core.DI;

namespace Abp.Authorization.Users
{

    public class NullUserTokenProviderAccessor : IUserTokenProviderAccessor, ISingletonDependency
    {
        public IUserTokenProvider GetUserTokenProviderOrNull()
        {
            return null;
        }
    }

    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider GetUserTokenProviderOrNull();
    }


    public interface IUserTokenProvider
    {

    }

}