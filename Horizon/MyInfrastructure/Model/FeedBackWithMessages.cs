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
        public object? Data { get; set; }
        public bool Done { get; set; }
        public List<string> Messages { get; set; }
        public void SetData<T>(T data)
        {
            Data = (T?)Convert.ChangeType(data, typeof(T));
            Done = true;
            Messages.Add("Operation completed successfully.");
        }
        public T? GetData<T>()
        {
            return (T?)Convert.ChangeType(Data, typeof(T));
        }

    }
}
