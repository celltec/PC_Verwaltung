using System.Text;
using System.Security.Cryptography;

namespace PC_Verwaltung
{
    /// <Zusammenfassung>
    /// Die Klasse SecureHash bietet Funktionalitäten zur Konvertierung von Klartext zu einem SHA256-Hash
    /// Quelle hierfür: https://stackoverflow.com/a/6839784
    /// </Zusammenfassung>
    static class SecureHash
    {
        // Erstellt ein SHA256-Hash und gibt es als byte array zurück
        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        // Wandelt das byte array in einen leserlichen String um
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
