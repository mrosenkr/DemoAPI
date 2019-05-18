using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CRM.API.Models
{
    public class Person
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public class RemovedPerson
    {
        public int id { get; set; }
    }

    public class PeopleResult
    {
        public List<Person> Data = new List<Person>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RemovedPerson> Deleted;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Int64? Version;
    }
}
