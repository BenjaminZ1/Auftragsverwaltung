using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Extensions;
using Newtonsoft.Json;

namespace Auftragsverwaltung.Application.Serializer
{
    public class CustomerSerializer : ISerializer<CustomerDto>
    {
        public void Serialize(CustomerDto obj, string filename)
        {
            obj.Password = SecureStringHelper.GetSecureString();

            if (filename[^4..].Equals(".xml"))
            {

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerDto));
                using Stream writer = new FileStream(filename, FileMode.Create);

                xmlSerializer.Serialize(writer, obj);
            }
            else
            {
                JsonSerializer serializer = new JsonSerializer {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};

                using StreamWriter sw = new StreamWriter(filename);
                using JsonWriter writer = new JsonTextWriter(sw);
                
                serializer.Serialize(writer, obj);
            }
        }

        public CustomerDto Deserialize(string filename)
        {
            CustomerDto deserializedObj;
            if (filename[^4..].Equals(".xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerDto));
                using Stream reader = new FileStream(filename, FileMode.Open);

                deserializedObj = (CustomerDto) xmlSerializer.Deserialize(reader);
            }
            else
            {
                using StreamReader file = File.OpenText(filename);
                JsonSerializer serializer = new JsonSerializer();

                deserializedObj = (CustomerDto)serializer.Deserialize(file, typeof(CustomerDto));
            }

            return deserializedObj;
        }
    }
}
