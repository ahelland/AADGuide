<properties
	pageTitle="Home"
	description="A walkthrough of and a guide to Azure Active Directory."
	slug="home"
	keywords="Azure AD, AAD, DirSync"
/>

## Azure Active Directory Guide and Walkthrough

If you have been working with the Microsoft technology stack in the past couple of years you will have heard the Azure brand name amidst all the cloud buzzwords (one might even say "Azure" is a buzzword in itself). For the sake of this guide, and the background needed to enjoy it fully, I will assume a further presentation of Azure in general isn't needed. What I would like to do however is to drill into a specific part of Azure that I have spent countless hours working with. This part is called Azure Active Directory, or AAD for short.

I'm sure a lot of you have been exposed to this technology already, and if you use Office 365 it's an integral part of the service. I can't fit everything AAD into one page so please navigate around this site to learn more. I will add more parts as I get around to writing and coding them :) There is so much more than just "AD in the cloud".

Starting off with the basics; here are the introductory parts I'll try to cover here:
- What is AAD
- Differences between AAD and on-prem AAD
- As an identity platform for Microsoft services (Office 365, etc.)
- Free vs Basic vs Premium vs O365

There's both an ITPro and a developer side to AAD, and I'll try to dive into them both. This means that not all topics will be relevant to everyone, but hopefully there will be something for both audiences.

Disclaimer: This is not an official Microsoft guide, and while the goal is to be as accurate as possible it is not authoritative. Azure is constantly developed, and if things don't work as described here Microsoft has probably changed something on their end :)

The official docs aren't bad, though you might have to visit several sources to find what you need (blogs, TechNet, MSDN), so this is meant more as a supplement than a replacement from the perspective of someone actually using and implementing it in the world outside Microsoft HQ. The flip side is that if the official doc is as good as it gets, and I don't bring any value adding to the table by repeating it, I might just link you to the official pages instead.

### What is Azure Active Directory (AAD)

As the name implies AAD is an Active Directory that runs in Azure. This means that you have users and groups in the directory, (and to a limited extent computers), and you can authenticate users against it to provide authentication and authorization for both web-based and native apps. It is managed through an administrative portal rather than the good old "Active Directory Users and Computers" MMC console we're used to. Other than different tooling it has the same purpose as an on-prem version when it comes to handling users. You need a centralized store for objects and accounts, and AAD is one such store.

### Differences between AAD and on-prem Active Directory (AD)

Rather than focus on what AAD is, it might provide more context to highlight some of the differences between the on-prem AD we're used to. The first thing to get out of the way is that creating a tenant in Azure Active Directory is not the same as installing a domain controller in the cloud. A domain controller serves as a DNS server, exposes an LDAP interface, has the concept of group policies, and a whole lot more. AAD does not provide these services. It manages users and groups, but does not provide DNS and you can't configure group policies either. The interfaces it exposes are intended for being over the HTTP protocol which means that LDAP just isn't there. It also excludes technologies like Kerberos and Integrated Windows Authentication. If you have an app that is wired to look up users via LDAP AAD doesn't directly address your needs. If however you are writing a new web app you'll like the Graph API way better than LDAP. Know your usage scenarios before going all in :)

We'll go into more details in the subsections.

### The identity platform for Microsoft services

Microsoft has had an identity platform for many years under different names, (Passport, MSN, Live, Microsoft Account), but these have been scoped to the consumer space. Logging into your Outlook.com you're using a Microsoft account, and this makes perfect sense. Outlook.com is a consumer service. A consumer is unlikely to have an identity platform like Active Directory at home.

The challenge is that while Outlook.com is a perfectly acceptable email service for consumers, it doesn't address the needs of enterprises. Which is why Microsoft has Exchange for that crowd instead. Cloud-based email for enterprises has been around for a long time, but when Microsoft embarked on the Office 365 journey it was clear that an identity platform that was built for the cloud was needed. The back-end running the platform for the consumer services was not suited for these purposes, and AAD was built instead. Office 365 has been using AAD as the platform for managing users for a long time, and in the early phases signing up for O365 was the only way to get AAD (and only service it could be used for as well). As the service has matured it is now the back-end for Azure, Intune, and other corporate directed services from Microsoft. Additionally there are now more services included in AAD itself, and you can use it for a whole bunch of things outside O365 as well. Either way; unless you're purely in the on-prem sphere odds are you will come across it if you sign up for cloud "stuff" from Microsoft.

### Different SKUs
To complicate things a little bit there are more than one SKU for Azure Active Directory. There is a free tier, which is a great value, but excludes some of the more advanced features. There are currently four different tiers/SKUs:
- Free
- Basic
- Premium
- Office 365 (In this SKU you don't sign up for AAD specifically, but get what's needed for running Office 365 services.)

For the details I refer to the official overview:

[http://azure.microsoft.com/en-us/pricing/details/active-directory/](http://azure.microsoft.com/en-us/pricing/details/active-directory/)