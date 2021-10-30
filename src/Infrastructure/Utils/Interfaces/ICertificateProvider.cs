using System.Security.Cryptography.X509Certificates;

namespace Realworlddotnet.Infrastructure.Utils.Interfaces;

public interface ICertificateProvider
{
    X509Certificate2 LoadFromUserStore(string thumbprint);
}
