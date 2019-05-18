using CRM.API.Models;
using System;
using System.Collections.Generic;

namespace CRM.API.Services
{
    public interface IPersonService
    {
        PeopleResult GetPeopleChanges(Int64 version);
        PeopleResult GetPeopleAll();
        
    }
}
