using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Xml.Linq;

namespace DAL
{
    public class DefaultXMLModule:IStudentOperations
    {
        private XDocument _document;
        private string _location;

       
        public DefaultXMLModule()
        {            
            _location = Directory.GetCurrentDirectory() + "\\data.xml";
            //check if specified file exists, if no - create new
            if (File.Exists(_location))
                _document = XDocument.Load(_location);
            else
                _document = new XDocument(new XElement("studentList"));
        }

        public void SaveChanges()
        {
            _document.Save(_location);
        }

        public void SaveStudent(IStudent student)
        {
            var res = _document.Root.Elements("student").FirstOrDefault(e => e.Element("name").Value == student.Name);
            if (res != null)
                return;

            XElement node = new XElement("student");

            node.Add(new XElement("name", student.Name));
            node.Add(new XElement("email", student.Email));
            node.Add(new XElement("startdate", student.StartDate));
            node.Add(new XElement("enddate", student.EndDate));

            _document.Root.Add(node);

            SaveChanges();
        }

        public IStudent GetByName(string studentName)
        {
            var res =  _document.Root.Elements("student").FirstOrDefault(e => e.Element("name").Value == studentName);
            return res != null ? new StudentDAL
            {
                Name = res.Element("name").Value,
                Email = res.Element("email").Value,
                StartDate = DateTime.Parse(res.Element("startdate").Value),
                EndDate = DateTime.Parse(res.Element("enddate").Value)

            } : null;
        }

        public void EditStudent(IStudent student)
        {
            var res = _document.Root.Elements("student").FirstOrDefault(e => e.Element("name").Value == student.Name);

 
            res.Element("email").Value = student.Email;
            res.Element("startdate").Value = student.StartDate.ToString();
            res.Element("enddate").Value = student.EndDate.ToString();
            SaveChanges();
            _document.Root.Add(res);
        }
    }
}
