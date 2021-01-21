using SimpleAPI2.Models;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using SimpleAPI2.Models.Constants;

namespace SimpleAPI2.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = "";
            string clientSecret = "";
            context.TryGetBasicCredentials(out clientId, out clientSecret);
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserMasterRepository _repo = new UserMasterRepository();
            var responseData = _repo.ValidateUser(context.UserName, context.Password);
            if (responseData == null)
            {
                context.SetError("error", "Failure");
                return;
            }

            var user = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(responseData));

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user[DBField.ROLE_ID]));
            identity.AddClaim(new Claim(ClaimTypes.Name, user[DBField.USER_ID]));
            //identity.AddClaim(new Claim("Email", user.UserEmailID));
            context.OwinContext.Set(VMField.USER, user);
            context.Validated(identity);
        }

        public override async Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            context.AdditionalResponseParameters.Add(VMField.USER, JsonConvert.SerializeObject(context.OwinContext.Get<Dictionary<string,string>>(VMField.USER)));
        }
        

    }
}