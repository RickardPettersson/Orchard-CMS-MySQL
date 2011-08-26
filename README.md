# Orchard CMS running on a MySQL database

This repository is a copy of Orchard Web 1.2.41 with fixes to get it to install with a MySQL database.

Here i going to write all steps i have done and you going to see in the repository all changes, i commit step by step. 

What i have done step by step:

* Downloaded Orchard Web 1.2.41 as zip from http://orchardproject.net/download

* Replaced the module named Orchard.Setup with the one downloaded from http://orchardproject.net/gallery/List/Modules/Orchard.Module.Orchard.Setup

* Downloaded System.Data.SQLite from http://sourceforge.net/projects/sqlite-dotnet2/files/SQLite%20for%20ADO.NET%202.0/

* Copyed the files needed from the binaries packages, all needed is named System.Data.SQLite.* and you copy the one in bin folder if you have a server running in 32-bit or the ones in bin/x64 if you running 64-bit server

* Download MySQL .Net Connector from http://www.mysql.com/downloads/connector/net/

* From MySQL .Net Connector binaries you only need the file named mysql.data.dll copied to the bin folder

* Now this is a worked version of Orchard CMS 1.2.41 with MySQL and SQLite setup module ready to be deployed on your server as a default installation of Orchard CMS

# Installation notes

To get everything to work like it should you need to set read and write permissions for IIS_IUSRS on the folders: App_Data, Media, Modules, Themes

When you put the files on the your webserver and set the read and write permissions you going to the url where you put the project exempel if you running localhost like me go to http://localhost and you get a form page that you fill in to setup Orchard as normal but there is some settings for MySQL that has been added.

Select Use an existing MySQL database and write the connectionstring, example: Data Source=localhost;Database=orchard;User Id=orchard;Password=orchard;

Dont write any Database Table Prefix (if you dont whant it but so you now its adding _ automatic between the table names and the prefix if you write anything here)

Press the button and it should now install Orchard.

# Bug with NHibernate and MySQL that gives "Column 'ReservedWord' does not belong to table ReservedWords"

If you getting this error message when you try to install the Orchard CMS project there is one line you need to add to the code for Orchard.Framework.dll.

You can find the source code on http://orchardproject.net and you need Visual Studio 2010, ASP.NET MVC 3.0 and .Net 4.0 installed.

Maybe you can fix it without all of this but its what Orchard CMS required to develop to it.

I opening the whole solution for Orchard CMS project and i finding the project called Orchard.Framework and finding Data\Providers\AbstractDataServicesProvider.cs

In that file you have a function called "BuildConfiguration" that returns Fluently.Configure()....

To that Fluently.Configure() you need to add ".ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"))" before .BuildConfiguration()

The function looks like this when i am done:

	public Configuration BuildConfiguration(SessionFactoryParameters parameters) {
		var database = GetPersistenceConfigurer(parameters.CreateDatabase);
		var persistenceModel = CreatePersistenceModel(parameters.RecordDescriptors);

		return Fluently.Configure()
			.Database(database)
			.Mappings(m => m.AutoMappings.Add(persistenceModel))
			.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"))
			.BuildConfiguration();
	}
	
Compile the project and copy Orchard.Framework.dll from the bin folder to the bin folder in Orchard CMS web that we fixed the MySQL on.

I find this solution on stackoverflow: http://stackoverflow.com/questions/1061128/mysql-nhibernate-how-fix-the-error-column-reservedword-does-not-belong-to-t

# Project information

This project was done by Rickard Pettersson (www.RickardP.se) to a work project for Transticket AB (www.Transticket.se)