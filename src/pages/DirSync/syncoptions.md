<properties
	pageTitle="Options"
	description="Synchronizing your cloud directory with your non-cloud directory."
	slug="syncoptions"
    order="100"
	keywords="Azure AD, AAD, DirSync, AAD Connect"
/>

An Azure Active Directory tenant has no problems living in a stand-alone mode where everything is purely in the cloud. It is not a less feature-rich or constrained setup, but the odds are that a lot of those who have already invested in Windows Server has an Active Directory on-premises. Stand-alone AAD is good. Stand-alone on-prem AD is good. Stand-alone AAD combined with stand-alone AD equals not so good.

What you usually want to do when you have a directory running on your own servers is to combine this data source with the directory you have in Azure. Conceptually this is straight forward, but you still have to do it the right way. Let's try to cover a few of the options available.

So, two stand-alones are no good, right? The short answer is yes. With a cloud-only setup things are simple. You have a username of "andreas@contoso.com", and use this along with a password to sign in to services. In the same manner you can sign into your intranet with a username and a password. The username you use locally can come in different forms though. It could be "andreas@contoso.com", "andreas@contoso.local" or "contoso\andreas" depending on how your Active Directory is configured. So far, so good. But as long as the two directories don't have a mechanism for exchanging this information between each other the users can easily get confused. The average user doesn't understand or care where the identies are stored, and can't be expected to figure all of this out on their own. And even for more technical users it can get confusing and less practical at times.

This means we need to figure out a synchronization scenario, and make a couple of choices.
1. Same username, different password.
2. Same username, same password through sync.
3. Same username, same password through federation.

### Option 1 - Username sync
With this option you sync the identies of directory objects, but not passwords. This makes things somewhat easier since the username stays the same across the locations, but remembering different passwords isn't optimal. If you change the password on different schedules too no doubt the users will forget them. Doable, but not recommended.

### Option 2 - Password sync
This option lets you sync both the username and the password. It's a very user-friendly configuration having the same credentials wherever they sign in, (within the corporate sphere at least), but not necessarily the favorite of the security department since they don't always trust the cloud to keep a copy of their passwords. Mind you, it's not like your password is sent in clear text to Microsoft. There's hashing and encryption involved, but nonetheless "something" is sent over the wire, and if you have a security policy you might not be able to use this option.

### Option 3 - Federation
If you want to keep your passwords on-prem, or you have a complex authentication process involving client certificates, keyfobs or something similar that isn't supported in Azure AD this might the option for you. The identities are synced, but not the password. Instead when you attempt to sign in to the cloud you are redirected on-prem for the actual login, and then get redirected back to the cloud with a token afterwards. The default product used for this is ADFS which is bundled into Windows Server as an optional feature, but other third-party products can also be used since it is standards-based. A trust needs to be setup between Azure AD and the federation server, but once configured the user shouldn't have problems using this. 

### Tools of the trade

After you decide which one to use that's where things traditionally have gotten complicated. The tool available from Microsoft called DirSync has long been the primary piece of software to use. The challenge with this is that DirSync has a very limited scope, and hasn't worked for all the scenarios one might see with complex multidomain forests with many users. For these more complicated setup Forefront Identity Manager (FIM) has been used, but that's not something most people get running in a half hour, and it doesn't make "the cloud" seem as simple as it the marketing says it is. In addition DirSync installation was unfriendly in the sense that you had to go out on your own and install the prereqs for DirSync to install adding onto the install time needed for that as well.

Things have improved though, and while you can still use DirSync it's probably better to go for AAD Connect if you're planning a new install. While AAD Connect hasn't hit RTM yet, and is in a preview currently, you should be able to use this in most cases. (Disclaimer: if you have a complicated Active Directory counting domains and subdomains across forests and ten thousand users I'd might want to have someone from Microsoft supporting you in case something goes wrong before installing preview software in such a setting.)

If you have been searching the net for tips on how to sync you might also have run across a utility called AAD Sync. This is also a working tool, but it has less features than AAD Connect and you can skip this.