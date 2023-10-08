using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInfrastructure.Model
{
    public class FeedBackWithMessages
    {
        public FeedBackWithMessages()
        {
            Messages = new List<string>();
        }
        public bool Done { get; set; }
        public List<string> Messages { get; set; }
    }
}
