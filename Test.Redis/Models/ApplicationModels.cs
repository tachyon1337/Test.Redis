using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RevStack.Pattern;

namespace Test.Redis.Models
{

    public class SampleModel : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}