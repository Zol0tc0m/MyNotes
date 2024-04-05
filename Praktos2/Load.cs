using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktos2
{
    public static class Load
    {
        public static void Serialize(Dictionary<DateTime, List<ToDo>> todo, string FileName)
        {
            string json = JsonConvert.SerializeObject(todo);
            File.WriteAllText(FileName, json);
        }

        public static Dictionary<DateTime, List<ToDo>> Deserialize(string FileName)
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                return JsonConvert.DeserializeObject<Dictionary<DateTime, List<ToDo>>>(json);
            }
            else
            {
                File.Create(FileName);
                return new Dictionary<DateTime, List<ToDo>>();
            }
        }
    }
}
