using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktos2
{
    public class ToDo
    {
        public string Heading { get; set; }
        public string DescrText { get; set; }
        public DateTime Date { get; set; }

        public ToDo(string heading, string desctext, DateTime date)
        {
            Heading = heading;
            DescrText = desctext;
            Date = date;

        }
    }
}
