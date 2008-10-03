﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XRDS_Simple.Net
{
    [XmlType(Namespace = Constants.XRD_Namespace)]
    [XmlRoot(ElementName = "XRD", Namespace = Constants.XRD_Namespace)]
    public class XRDElement
    {
        private List<string> typeElements = new List<string>();
        private List<ServiceElement> serviceElements = new List<ServiceElement>();

        private string xrdsVersion = Constants.XRDS_Version;

        #region XRDElement Properties
        
        [XmlAttribute(AttributeName = "id", Namespace="http://www.w3.org/XML/1998/namespace")]
        public string ID
        {
            get;
            set;
        }        

        [XmlAttribute(AttributeName = "version", Namespace = Constants.XRD_Namespace)]
        public string Version
        {
            get { return xrdsVersion; }
            set 
            {
                xrdsVersion = value;
            }
        }

        [XmlElement(DataType = "anyURI", ElementName = "Type", Namespace = Constants.XRD_Namespace)]
        public string[] Types
        {
            get { return typeElements.ToArray(); }
            set 
            {
                typeElements.Clear();
                if (value != null)
                    typeElements.AddRange(value);
            }
        }

        [XmlElement(DataType = "dateTime", ElementName = "Expires", IsNullable = true, Namespace = Constants.XRD_Namespace)]
        public DateTime? Expires
        {
            get;
            set;
        }

        [XmlElement(ElementName = "Service", Namespace = Constants.XRD_Namespace)]
        public ServiceElement[] Services
        {
            get { return serviceElements.ToArray(); }
            set
            {
                serviceElements.Clear();
                if (value != null)
                    serviceElements.AddRange( value );
            }
        }

        #endregion

        /// <summary>
        /// Returns an enumeration of services for this XRD Element in 
        /// a valid order based on each services priority.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ServiceElement> FindServices()
        {
            return FindServices((Predicate<ServiceElement>)null);
        }

        /// <summary>
        /// Returns an enumeration of services for this XRD Element in 
        /// a valid order based on each services priority.
        /// </summary>
        /// <returns></returns
        public IEnumerable<ServiceElement> FindServices(string type)
        {
            return FindServices(service => Array.IndexOf<string>(service.Types, type) > -1);
        }        

        public IEnumerable<ServiceElement> FindServices(Predicate<ServiceElement> filter)
        {
            ///Need to implement some filters.. but in the bottom we need to sort based on priority.

            List<ServiceElement> filteredList = serviceElements;

            if (filter != null)
                filteredList = serviceElements.FindAll(filter);

            if (filteredList.Count > 1)
            {
                List<IPriority> sortedList = filteredList.ConvertAll<IPriority>(element => (IPriority)element);

                PriorityComparer comparer = new PriorityComparer();
                sortedList.Sort(comparer);

                //Expose this only as an IEnumerable.
                return sortedList.ConvertAll<ServiceElement>(element => (ServiceElement)element); ;
            }
            else
                return filteredList;
        }

    }
}