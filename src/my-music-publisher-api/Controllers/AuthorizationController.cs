using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi1.Controllers
{
    public class AuthorizationController : Controller
    {
        [HttpPost("~/connect/token"), Produces("application/json; charset=UTF-8")]
        public IActionResult Exchange(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                // Validate the user credentials.

                // Note: to mitigate brute force attacks, you SHOULD strongly consider
                // applying a key derivation function like PBKDF2 to slow down
                // the password validation process. You SHOULD also consider
                // using a time-constant comparer to prevent timing attacks.
                //if (request.Username != "alice@wonderland.com" ||
                //    request.Password != "P@ssw0rd")
                //{
                //    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                //}
                if (request.Username != "test" ||
                    request.Password != "test")
                {
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }

                // Create a new ClaimsIdentity holding the user identity.
                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name, null);

                // Add a "sub" claim containing the user identifier, and attach
                // the "access_token" destination to allow OpenIddict to store it
                // in the access token, so it can be retrieved from your controllers.
                identity.AddClaim(OpenIdConnectConstants.Claims.Subject,
                    "71346D62-9BA5-4B6D-9ECA-755574D628D8",
                    OpenIdConnectConstants.Destinations.AccessToken);

                identity.AddClaim(OpenIdConnectConstants.Claims.Name, "Alice",
                    OpenIdConnectConstants.Destinations.IdentityToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.Username, request.Username,
                    OpenIdConnectConstants.Destinations.IdentityToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.GivenName, "Test",
                    OpenIdConnectConstants.Destinations.IdentityToken);
                identity.AddClaim(OpenIdConnectConstants.Claims.FamilyName, "Testić",
                    OpenIdConnectConstants.Destinations.IdentityToken);

                // ... add other claims, if necessary.

                var principal = new ClaimsPrincipal(identity);

                // Ask OpenIddict to generate a new token and return an OAuth2 token response.
                return SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        [HttpPost("~/connect/userinfo"), Produces("application/json")]
        public IActionResult GetUserInfo(OpenIdConnectRequest request)
        {
            if (request.IsUserinfoRequest())
            {
                
                // Validate the user credentials.

                // Note: to mitigate brute force attacks, you SHOULD strongly consider
                // applying a key derivation function like PBKDF2 to slow down
                // the password validation process. You SHOULD also consider
                // using a time-constant comparer to prevent timing attacks.
                //if (request.Username != "alice@wonderland.com" ||
                //    request.Password != "P@ssw0rd")
                //{
                //    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                //}
                if (request.Username != "test" ||
                    request.Password != "test")
                {
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }

                // Create a new ClaimsIdentity holding the user identity.
                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name, null);

                // Add a "sub" claim containing the user identifier, and attach
                // the "access_token" destination to allow OpenIddict to store it
                // in the access token, so it can be retrieved from your controllers.
                identity.AddClaim(new Claim(OpenIdConnectConstants.Claims.Subject,
                    "71346D62-9BA5-4B6D-9ECA-755574D628D8").SetDestinations(OpenIdConnectConstants.Destinations.AccessToken, OpenIdConnectConstants.Destinations.IdentityToken));
                identity.AddClaim(new Claim(OpenIdConnectConstants.Claims.Name, "Alice").SetDestinations(OpenIdConnectConstants.Destinations.IdentityToken));

                // ... add other claims, if necessary.

                var principal = new ClaimsPrincipal(identity);
               
                // Ask OpenIddict to generate a new token and return an OAuth2 token response.
                return SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }
    }
}
