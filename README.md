# Orchard CMS running on a MySQL database

This repository is a copy of Orchard Web with latest code to 4 Januari 2012 from my fork of Orchard CMS source code with fixes to get it to install with a MySQL database.

See my fork and what changes i have done in http://orchard.codeplex.com/SourceControl/network/Forks/RickardP/rickardporchard

# Installation notes

To get everything to work like it should you need to set read and write permissions for IIS_IUSRS on the folders: App_Data, Media, Modules, Themes

When you put the files on the your webserver and set the read and write permissions you going to the url where you put the project exempel if you running localhost like me go to http://localhost and you get a form page that you fill in to setup Orchard as normal but there is some settings for MySQL that has been added.

Select Use an existing SQL database and select MySQL as database then write the connectionstring, example: Server=localhost;Port=3306;User Id=orchard;Password=orchard;Persist Security Info=True;Database=orchard;

Dont write any Database Table Prefix (if you dont whant it but so you now its adding _ automatic between the table names and the prefix if you write anything here)

Press the button and it should now install Orchard.

# Project information

This project was done by Rickard Pettersson (www.RickardP.se) to a work project for Transticket AB (www.Transticket.se)