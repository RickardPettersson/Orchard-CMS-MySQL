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

