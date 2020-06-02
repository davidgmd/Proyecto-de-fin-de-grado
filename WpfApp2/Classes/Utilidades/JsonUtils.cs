using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElEscribaDelDJ.Classes.Utilidades
{
    public static class JsonUtils
    {
        public static string DeJsonAString(JObject json)
        {
            return json.ToString();
        }

        public static JObject DeUserAJsonObject(Usuario usuario)
        {
            return JObject.FromObject(usuario);
        }

        public static Usuario DeJsonAUserObject(string json, Usuario usuario) 
        {
            usuario = JsonConvert.DeserializeObject <Usuario>(json);         
            return usuario;
        }
    }
}
