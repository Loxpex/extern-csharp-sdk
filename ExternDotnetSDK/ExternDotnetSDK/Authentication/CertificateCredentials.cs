using System.Security.Cryptography.X509Certificates;

namespace Kontur.Extern.Client.Authentication
{
    public class CertificateCredentials
    {
        /// <summary>
        /// Публичный ключ пользователя
        /// </summary>
        public X509Certificate2 PublicKey { get; set; }

        /// <summary>
        /// Пропускать валидацию сертификата пользователя
        /// </summary>
        public bool Free { get; set; }
    }
}