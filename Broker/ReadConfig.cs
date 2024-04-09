using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

public class ReadConfig
{
    public static Dictionary<string, string> ReadRabbitConfig()
    {
        JObject config = JObject.Parse(File.ReadAllText("./Broker/rabbitSetting.json"));

        Dictionary<string, string> rabbitConfig = new Dictionary<string, string>();
        rabbitConfig["RABBITMQ_DEFAULT_USER"] = config.Value<string>("RABBITMQ_DEFAULT_USER") ?? "guest";
        rabbitConfig["RABBITMQ_DEFAULT_PASS"] = config.Value<string>("RABBITMQ_DEFAULT_PASS") ?? "guest";
        rabbitConfig["HOST_RABBITMQ"] = config.Value<string>("HOST_RABBITMQ") ?? "localhost";
        rabbitConfig["PORT_RABBITMQ"] = config.Value<string>("PORT_RABBITMQ") ?? "5672";

        return rabbitConfig;
    }

}
