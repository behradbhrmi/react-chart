using Newtonsoft.Json;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.ViewModel
{
    public class FinitxUserVm
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }       

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
    public class FinitxEditProfileVm
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }       

        [JsonProperty("email")]
        public string Email { get; set; }    

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    } 
    public class FinitxChangePasswordVm
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }       
    } 
    public class FinitxConfirmVm
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}