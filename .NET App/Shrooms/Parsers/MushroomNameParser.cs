using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static MudBlazor.CategoryTypes;

namespace Shrooms.Parsers
{
    public class MushroomNameParser
    {
        public static (string polishName, string mushroomType) GetPolishNameAndMushroomType(string latinName)
        {
            string json = "";
            using (StreamReader r = new StreamReader("mushroomsNamesMapped.json"))
            {
                json = r.ReadToEnd();
            }
            JObject namesMapped = JObject.Parse(json);

            var deadly = namesMapped[$"deadly"];
            var edible = namesMapped[$"edible"];
            var poisonous = namesMapped[$"poisonous"];

            if ((string)deadly[$"{latinName}"] is not null)
            {
                return ((string)deadly[$"{latinName}"], "śmiertelnie trujący");
            }
            else if ((string)edible[$"{latinName}"] is not null)
            {
                return ((string)edible[$"{latinName}"], "jadalny");
            }
            else if ((string)poisonous[$"{latinName}"] is not null)
            {
                return ((string)poisonous[$"{latinName}"], "trujący");
            }

            else return ("failed", "failed");
        }
    }
}
