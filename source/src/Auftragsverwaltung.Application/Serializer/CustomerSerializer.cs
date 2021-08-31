using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Auftragsverwaltung.Application.Serializer
{
    public class CustomerSerializer
    {
        private readonly IAppRepository<Customer> _repository;

        public CustomerSerializer(IAppRepository<Customer> repository)
        {
            _repository = repository;
        }
        public void Serialize()
        {
            var customers = _repository.GetAll();

            //string filePath = Environment.ExpandEnvironmentVariables("C:\\Users\\%USERPROFILE%\\Roaming\\Auftragsverwaltung\\serializedCustomers.json");

            using (StreamWriter file = File.CreateText(@"C:\\Users\\%USERPROFILE%\\Roaming\\Auftragsverwaltung\\serializedCustomersJson.json"))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(file, customers);
            }

            using (StreamWriter file = File.CreateText(@"C:\\Users\\%USERPROFILE%\\Roaming\\Auftragsverwaltung\\serializedCustomersXml.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Customer>));
                xmlSerializer.Serialize(file, customers);
            }
        }
    }
}
