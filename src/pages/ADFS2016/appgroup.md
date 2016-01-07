<properties
	pageTitle="Application Groups"
	description="Setting up Application Groups and Apps in ADFS 2016"
	slug="appgroups"
	order="200"
	keywords="Azure AD, AAD, ADFS 2016"
/>

In this walkthrough we will attempt to replicate the scenario described in the WebAPISingleTenant walkthrough using ADFS instead of Azure AD. The purpose is to show the differences, while also highlighting how much of the code is similar between the two configurations.  
For reference:  
[http://aadguide.azurewebsites.net/integration/webapisingletenant](http://aadguide.azurewebsites.net/integration/webapisingletenant)

This is part 1 (of 2) - configuring the ADFS Server.  
Part 2 covers the code.  
It is assumed that ADFS 2016 is already installed on a server.

**Note: ADFS 2016 is still in beta. The following walkthroughs are based on Technical Preview 4 (Build 10586).**

Open the ADFS Management Console, right-click on "Application Groups", and click "Add Application Group":  
![ADFS 2016 - Application Group](_assets/AppGroup_01.PNG)

Select  "Server Application or Website", and hit "Next":  
![ADFS 2016 - Application Group](_assets/AppGroupServer_01.PNG)

Enter https://localhost:44320 as the redirect URI and "Add it". Copy the "Client Identifier", and hit "Next":  
![ADFS 2016 - Server App](_assets/AppGroupServer_02.PNG)

Check "Generate a shared secret", and copy it to the clipboard (and preferably into a text file afterwards):  
![ADFS 2016 - Server App](_assets/AppGroupServer_03.PNG)

Confirm that things look right:  
![ADFS 2016 - Server App](_assets/AppGroupServer_04.PNG)

Hit "Close" if it was successful:  
![ADFS 2016 - Server App](_assets/AppGroupServer_05.PNG)

Next we need to add a Web API:  
![ADFS 2016 - Web API](_assets/AppGroupWebAPI_01.PNG)

We need to create an identifier for the app - as long as it's unique you're good:  
![ADFS 2016 - Web API](_assets/AppGroupWebAPI_02.PNG)

We need to assign a policy. For simplicity we choose "Permit everyone":  
![ADFS 2016 - Web API](_assets/AppGroupWebAPI_03.PNG)

Make sure "openid" is checked as the scope:  
![ADFS 2016 - Web API](_assets/AppGroupWebAPI_04.PNG)

Confirm that things look right:  
![ADFS 2016 - Web API](_assets/AppGroupWebAPI_05.PNG)

And success:  
![ADFS 2016 - Web API](_assets/AppGroupWebAPI_06.PNG)

We should also add a "Native application" for use in a Universal Windows Platform app:  
![ADFS 2016 - Native App](_assets/AppGroupNativeApp_01.PNG)

For the moment we fill in a placeholder value for the redirect URI as we have not built the app yet. When we have the id we need to revisit this page:  
![ADFS 2016 - Native App](_assets/AppGroupNativeApp_02.PNG)

Confirm that things look right:  
![ADFS 2016 - Native App](_assets/AppGroupNativeApp_03.PNG)

And success:  
![ADFS 2016 - Native App](_assets/AppGroupNativeApp_04.PNG)

You should now have the following applications in the application group "AADGuide":  
![ADFS 2016 - Application Group](_assets/AppGroup_02.PNG)

Afterwards you need to grant access to the Web API for both the Native and the Server App:  
![ADFS 2016 - Web API](_assets/AppGroup_03.PNG)

You should also have a couple of future variables written down:  
```cs
var serverapp_ClientId = "a1b2c3";
var serverapp_RedirectURI = "https://localhost:44320";
var serverapp_ClientSecret = "a1b2c3";
var webAPI_Id = "https://aadguide.azurewebsites.net/WebAPI";
var uwpApp_ClientId = "a1b1c3";
var uwpApp_RedirectURI = "ms-app://xyz";
```

You will need these when you head over to the next part.
