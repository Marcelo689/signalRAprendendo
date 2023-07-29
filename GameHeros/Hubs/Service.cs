namespace GameHeros.Hubs
{
    public static class Service
    {
        public static async Task<string> GetIp()
        {
            var http = new HttpClient();

            var ip = await http.GetFromJsonAsync<IpClass>("https://api.ipify.org?format=json");

            return ip.ip;
        }
    }

    public class IpClass
    {
        public string ip { get; set; }  
    }
}
