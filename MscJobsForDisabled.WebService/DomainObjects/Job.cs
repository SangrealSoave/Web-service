using System;
using System.Collections.Generic;
using System.Text;

namespace MscJobsForDisabled.DomainObjects
{
    public class Job : DomainObject 
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string WorkingHours { get; set; }
    }
}
