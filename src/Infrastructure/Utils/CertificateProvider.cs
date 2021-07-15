using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;

namespace realworlddotnet.Infrastructure.Utils
{
    public class CertificateProvider : ICertificateProvider
    {
        private readonly ILogger _logger;

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

            _logger.LogInformation($"Loading certificate {thumbprint} from store");

            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

            store.Close();

            if (certCollection.Count <= 0)
            {
                throw new Exception($"Unable to locate any certificate with thumbprint {thumbprint}.");
            }

            return certCollection[0];
        }
    }
}