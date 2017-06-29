<properties
	pageTitle="ADFSWebAPI"
	description="Implementing OAuth and OpenId Connect in ADFS 2016"
	slug="adfswebapi"
	order="300"
	keywords="Azure AD, AAD, ADFS 2016"
/>

In this walkthrough we will attempt to replicate the scenario described in WebAPISingleTenant using ADFS instead of Azure AD. The purpose is to show the differences, while also highlighting how much of the code is similar between the two configurations.

This is part 2 (of 2) - writing/editing sample code.  
Part 1 covers the ADFS Server configuration.  
It is assumed that ADFS 2016 is already installed on a server.

ADFS exposes a number of protocols that you can use from a developer's perspective. Whether it be WS-*, SAML, or a number of other acronyms that you have required, you have been able to integrate .Net apps in some way. If you are starting an app from scratch now you are more likely to look into OAuth and OpenId Connect. Azure AD has supported OAuth for a while, and technically ADFS in Windows Server 2012 R2 has some limited support too. There have been some differences in the implementation details however, so there has been a couple of pain points if you want to write an app that requires support for on-prem/cloud/hybrid in one package. Yes, you can make a web app work with both AAD and ADFS by implementing more than one protocol. But for obvious reasons the less protocols the easier.

In the guide for setting up a web app and api for a single AAD tenant the authentication methods were implemented using OAuth and OpenId Connect. This guide will be based on the same code, but using ADFS instead of AAD.  
For reference:  
[http://aadguide.azurewebsites.net/integration/webapisingletenant](http://aadguide.azurewebsites.net/integration/webapisingletenant)

The steps below show how to work from the existing code, and modify it, but there is a separate Visual Studio solution as well that shows the result. The finished solution has also seen some renaming to make the naming reflect that ADFS is being used. For a solution supporting both you'd probably use more generic terms, and some logic in the code to wire things up correctly for the specific scenario.

Code on GitHub:  
[https://github.com/ahelland/AADGuide-CodeSamples](https://github.com/ahelland/AADGuide-CodeSamples)

Project name: ADFSWebAPIServer and ADFSWebAPIClient


### Modifying WebAPIServerSingleTenant ###

_web.config_
```
<!--Replace 'Contoso' with your tenant name-->
<add key="ida:Tenant" value="contoso.onmicrosoft.com" />
<add key="ida:ResourceId" value="https://contoso.onmicrosoft.com/WebAPIServerSingleTenant" />
<add key="ida:ClientID" value="copy-from-Azure-Portal" />
<add key="ida:ClientSecret" value="copy-from-Azure-Portal" />
<!--For web-based login-->
<add key="ida:AADInstance" value="https://login.microsoftonline.com/" />
<!--
	The tenant id can be retrieved from the login Federation Metadata end point:             
	https://login.microsoftonline.com/contoso.onmicrosoft.com/FederationMetadata/2007-06/FederationMetadata.xml
	Replace "contoso.onmicrosoft.com" with any domain owned by your organization
	The returned value from the first xml node "EntityDescriptor", will have a STS URL
	containing your TenantId 
-->
<add key="ida:TenantId" value="get-from-federation-endpoint" />
<add key="ida:PostLogoutRedirectUri" value="https://localhost:44300/" />
```
Modify _"ida:ResourceId" => "https://aadguide.azurewebsites.net/WebAPI"_  
Modify _"ida:ClientID" => value of serverapp_ClientId = "a1b2c3";_  
Modify _"ida:PostLogoutRedirectUri" => serverapp_RedirectURI_  
Delete _"ida:ClientSecret"_  
Delete _"ida:AADInstance"_  
Delete _"ida:TenantId"_  
Delete _"ida:Tenant"_  
Add _"ida:ADFSServer" => "https://adfs.contoso.local/adfs/"_ (replace with FQDN of your ADFS Server)  

The resulting _web.config_ will look roughly like this:  
```        
<add key="ida:ResourceId" value="https://aadguide.azurewebsites.net/WebAPI" />
<add key="ida:ClientID" value="serverapp_ClientId" />
<add key="ida:ADFSServer" value="https://adfs.contoso.local/adfs/" /> 
<!--For web-based login-->            
<add key="ida:PostLogoutRedirectUri" value="https://localhost:44320/" />
```

_App_Start/Startup.Auth.cs_

```
private static string clientId = ConfigurationManager.AppSettings["ida:ClientID"];
private static string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
private static string resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];

public static readonly string Authority = aadInstance + tenantId;       

// This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
string graphResourceId = "https://graph.windows.net";
```

Delete _private static string appKey_   
Delete _private static string aadInstance_  
Delete _private static string tenantId_  
Delete _private static string tenant_  
Delete _public static readonly string Authority_   
Delete _string graphResourceId_  

Add _private static string ADFSServer = ConfigurationManager.AppSettings["ida:ADFSServer"];_  
Add _private static string ADFSDiscoveryDoc = ADFSServer + "adfs/.well-known/openid-configuration";_  
Add _private static string FederationMetadata = ADFSServer + "federationmetadata/2007-06/federationmetadata.xml";_  

This should give something like this:
```
private static string clientId = ConfigurationManager.AppSettings["ida:ClientID"];       
private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];        
private static string resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];
private static string metadataAddress = ConfigurationManager.AppSettings["ida:ADFSDiscoveryDoc"];
private static string ADFSServer = ConfigurationManager.AppSettings["ida:ADFSServer"];
private static string ADFSDiscoveryDoc = ADFSServer + "adfs/.well-known/openid-configuration";
private static string FederationMetadata = ADFSServer + "federationmetadata/2007-06/federationmetadata.xml";               
```

Next thing is to reconfigure which authenticationmethods are available. Locate the section starting with
_app.UseWindowsAzureActiveDirectoryBearerAuthentication_
and delete it.

Instead we will add the following:  
```
app.UseActiveDirectoryFederationServicesBearerAuthentication(
	new ActiveDirectoryFederationServicesBearerAuthenticationOptions
	{
		MetadataEndpoint = FederationMetadata,
		TokenValidationParameters = new TokenValidationParameters
		{
			ValidAudience = resourceId,
			AuthenticationType = "OAuth2Bearer"
		}
	}
);
```  

This authentication mechanism enables the server/web api to validate the token presented by the native app. It also ensures that you are not able to call the API directly in a web browser by restricting it to the type "OAuthBearer".

We still need to be able to login through the browser to view the documentation page for the API, and we do this by using OpenId Connect. This is already present in the
_app.UseOpenIdConnectAuthentication_ section, but we need to make some minor modifications.

```
app.UseOpenIdConnectAuthentication(
	new OpenIdConnectAuthenticationOptions
	{
		ClientId = clientId,
		Authority = Authority,
		PostLogoutRedirectUri = postLogoutRedirectUri,

		Notifications = new OpenIdConnectAuthenticationNotifications()
		{
			// If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
			AuthorizationCodeReceived = (context) =>
			{
				var code = context.Code;
				ClientCredential credential = new ClientCredential(clientId, appKey);
				string signedInUserID = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
				Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = 
				new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(Authority, new ADALTokenCache(signedInUserID));
				AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(
				code, new System.Uri(System.Web.HttpContext.Current.Request.Url.GetLeftPart(System.UriPartial.Path)), credential, graphResourceId);

				return System.Threading.Tasks.Task.FromResult(0);
			}
		}
	});
```

Delete the _Authority_ variable  
Add _MetadataAddress_  
Add _RedirectURI_  
Add _Resource_  

We'll also "flip" the logic for the Notifications bits. We will end up with the following:  
```
app.UseOpenIdConnectAuthentication(
	new OpenIdConnectAuthenticationOptions
	{
		ClientId = clientId,                    
		MetadataAddress = ADFSDiscoveryDoc,
		RedirectUri = postLogoutRedirectUri,
		Resource = resourceId,
		PostLogoutRedirectUri = postLogoutRedirectUri,

		Notifications = new OpenIdConnectAuthenticationNotifications()
		{                        
			AuthenticationFailed = context =>
			{
				context.HandleResponse();
				context.Response.Redirect("/Error?message=" + context.Exception.Message);
				return System.Threading.Tasks.Task.FromResult(0);
			}
		}
	});
```

This takes care of the server part of the equation.

We also need to make a couple of adjustments to the native app. 

### Modifying WebAPIClientSingleTenant ###

Double-click the _Package.appxmanifest_, and check "Private Networks (Client & Server)".

A couple of minor adjustments to _MainPage.xaml.cs_:
```
const string aadInstance = "https://login.microsoftonline.com/";
const string ResourceId = "https://contoso.onmicrosoft.com/WebAPIServerSingleTenant";
const string tenant = "contoso.onmicrosoft.com";
const string clientId = "copy-from-Azure-Portal";
const string baseApiUrl = "localhost:44300";
```

Modify _aadInstance_ => "https://adfs.contoso.local/adfs"  
Modify _ResourceId_ => webAPI_Id  
Delete _tenant_  
Modify _clientId_ => uwpApp_ClientId  
Modify _baseApiUrl_ => "localhost:44320"  

In the _GetToken_ method modify _var authority = $"{aadInstance}{tenant}";_ => _var authority = $"{aadInstance}";_

And that should be it basically.

There is one minor snag. The first time you run the app you will get an error that ms-app://xyz isn't allowed to authenticate. Remember that uwpApp_RedirectURI from part 1? You need to copy the id from the error to the ADFS server (as shown in part 1). Afterwards your app should be allowed to sign you in.

The functionality, and the visual appearance will be the same as in the AAD-based walkthrough, but ADFS is doing the OAuth dance instead. (Note that the theming of ADFS will be slightly different, but the experience will be the same.)

While the two cases require modifying a few lines more than just the _web.config_ you can see that we're really only touching one file to convert from AAD to ADFS.  
Throw in an extra setting or two in web.config, and a few more lines in _Startup.auth.cs_, and you have a web app and api that can work with and without a cloud as per your choosing!