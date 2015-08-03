<properties
	pageTitle="Samples Index"
	description="A description of the code samples in this section."
	slug="samplesindex"
    order="400"
	keywords="Azure AD, AAD, Integration, Identity, Web App, AAD App Registration"
/>

**HelloAzureAD**

This app is a Windows Universal app (built for Windows 10) that shows how to authenticate a user against an Azure Active Directory tenant. Subsequently the acquired token is used to execute a query against the Graph API to extract the user object. 

It is a very simple app, but it serves as a basic unit it's easy to build upon to get started using Azure AD in the "Universal world". It uses the Active Directory Authentication Library (ADAL) NuGet package to do the authentication. The calls to the Graph API are made using "raw" calls without helper libraries to show how this API works on the lower levels.

GitHub:
[https://github.com/ahelland/AADGuide-CodeSamples/tree/master/HelloAzureAD](https://github.com/ahelland/AADGuide-CodeSamples/tree/master/HelloAzureAD)

**GraphTreeView**

This app is a Windows Forms app, or "classic" app if you will, which shows how to interact with Azure AD for the purpose of getting a list of groups and users. 

It demonstrates the use of both the Active Directory Authentication Library (ADAL), and the Graph Client NuGet packages.

Client id and secret is used to authenticate as a trusted client. (Intended for server side usage scenarios, but the demo shows how to do it client side in the context of a developer utility.)

GitHub:
[https://github.com/ahelland/AADGuide-CodeSamples/tree/master/GraphTreeView](https://github.com/ahelland/AADGuide-CodeSamples/tree/master/GraphTreeView)

**WebAPISingleTenant**

This guide consists of a server side web app and a client side Windows Universal app. Azure AD authentication is added to both the Web API endpoint and the Web App itself.

The point of the exercise is to combine the templates for MVC-based apps using Azure AD for authentication with the Web API equivalent. In addition it makes for a more complete sample as the client part is also demonstrated.

Code on GitHub:
[https://github.com/ahelland/AADGuide-CodeSamples](https://github.com/ahelland/AADGuide-CodeSamples)

Project name: WebAPIServerSingleTenant and WebAPIClientSingleTenant