using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Realworlddotnet.Infrastructure.Utils.Interfaces;

namespace Realworlddotnet.Infrastructure.Utils;

public class CertificateProvider : ICertificateProvider
{
    private readonly ILogger<CertificateProvider> _logger;

    public CertificateProvider(ILogger<CertificateProvider> logger)
    {
        _logger = logger;
    }

    public X509Certificate2 LoadFromUserStore(string thumbprint)
    {
        if (string.IsNullOrWhiteSpace(thumbprint))
        {
            throw new ArgumentNullException(nameof(thumbprint));
        }

        _logger.LogInformation("Loading certificate {Thumbprint} from store", thumbprint);

        var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

        store.Close();

        if (certCollection.Count <= 0)
        {
            throw new ArgumentException($"Unable to locate any certificate with thumbprint {thumbprint}.");
        }

        return certCollection[0];
    }

    public X509Certificate2 LoadFromFile(string filename, string password)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentNullException(nameof(filename));
        }

        _logger.LogInformation("Loading certificate {Thumbprint}", filename);
        var certCollection = new X509Certificate2Collection();
        certCollection.Import(filename, password);

        if (certCollection.Count <= 0)
        {
            throw new ArgumentException("Unable to locate any certificate");
        }

        return certCollection[0];
    }
}
