using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Auftragsverwaltung.Application.Dtos;

namespace Auftragsverwaltung.Application.Serializer
{
    public class CustomerSerializer : ISerializer<CustomerDto>
    {
        public void Serialize(CustomerDto obj, string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerDto));
            using StreamWriter file = File.CreateText(filename);
            xmlSerializer.Serialize(file, obj);
        }

        public CustomerDto Deserialize(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerDto));
            using Stream reader = new FileStream(filename, FileMode.Open);

            var deserializedObj = (CustomerDto)xmlSerializer.Deserialize(reader);

            return deserializedObj;
        }
    }
}
