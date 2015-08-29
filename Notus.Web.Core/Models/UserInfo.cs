using System.IO;
using System.Xml.Serialization;

namespace Notus.Web.Core.Models
{
    public class UserInfo
    {
        public string DisplayName { get; set; }
        public string UserIdentifier { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }

        public override string ToString()
        {
            var serializer = new XmlSerializer(typeof (UserInfo));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, this);
                return stream.ToString();
            }
        }

        public static UserInfo FromString(string userContextData)
        {
            var serializer = new XmlSerializer(typeof (UserInfo));
            using (var stream = new StringReader(userContextData))
            {
                return serializer.Deserialize(stream) as UserInfo;
            }
        }
    }
}