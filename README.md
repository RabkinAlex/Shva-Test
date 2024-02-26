# Shva-Test
Service use background worker to TCP listene on port 8888
Received message encrypted with AES algoritm with random key generation.
After encryption,data saved in DB(SQLEXPRESS). DDL script added to repository.
SSL local certificate added to repository.
Test case for this service maybe is:
1. Test connection and receiving the message(via websocket Postman).
2. Integration test for encryption of message and DB saving.

Deployment for this service : Docker (added docker file)
Connection string to the DB is in appsettings.json,need be change for local machine 
