This project is the Implementation of Identity server 4 in Asp.net core written in .net core 3.
All IS4 settings are fetched from the SQL server database and no hard code values are used.
follow instructions below to launch the project:
1. Create a database named IdentityServer(if you want to change the name of the database, you should edit the connection string value in the appsettings.json in the IdentityServerApi project)
2. Execute the SQL generated script(schema.sql) to restore the schema to your database)
3. Execute the SQL data backup (seed_data.sql) to use the initialized data ( if you want to create your scope, client, etc. you can ignore this step.
4. Use Postman collection(identity-server.postman_collection.json) to create user.
5. Use Postman collection(identity-server.postman_collection.json) to test token generation and authorization.
