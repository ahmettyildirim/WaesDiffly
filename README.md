# WaesDiffly
Asp.net Web Api project that provides 2 http points that accepts JSON base64 encoded binary data and compare them.


How it works
----------
It provides 2 endpoints for getting
 - _{host}/v1/diff/{ID}/left_
 - _{host}/v1/diff/{ID}/right_
 These are using with POST operation. The body is expected to be JSON document with a property named `Content`, which contains Base64 encoded bytes for diffing, e.g. `{ "Content": "AeK7qqvcV6qqpQ==" }`
 

 The values are saved in static list. But there is also an option with saved the records to any database. For doing this, all you have to do, change the derived abstract database
 factory from StaticLayer to MongoLayer or SQLLayer or etc in DatabaseHelperFactory located in CBL. These two are also defined but didn't implemented.'

The third endpoint is comparing values provided both sides, if already provided. If there is an error, it returns error messages.
 - GET _{host}/v1/diff/{ID}/_

----------

Architecture
-------------

Project has 4 layer. 
 - WebApi
 - Cbl
 - Model
 - DataLayer

 Model layer is added to all other layers as a reference. It includes just models.
 DataLayer can only accesible from CBL. All database operations (or for now, static list operations)  doing in this layer.
 CBL can accessible from WebApi. All business operations are done in this layer. And it also calls database layer for insert, update and get operations.
 WebApi it provides HTTP operations, GET and POST. And calls CBL for operations. 

----------

Usage examples
-------------

With the curl request like below, you can post left and right values, and save 

	curl -H "Content-Type: application/json" --data "{ \"Content\": \"AeK7qqvcV6qqpQ==\" }"  http://localhost:52383/v1/diff/1/left
    
	curl -H "Content-Type: application/json" --data "{ \"Content\": \"AeK7qqvcV6qqpQ==\" }"  http://localhost:52383/v1/diff/1/right
    
	curl -H "Content-Type: application/json" http://localhost:52383/v1/diff/1/


	
----------

Suggestions
-------------

 - Instead of using 3 endpoints, we could do this operations with just one request, taking left and right values and returns result
 - Adding logging mechanism for all request and response, and save values with this info like IP adress. For example, if for the Id number 1, 
 when one part is set we can allow the set other part just with that Ip. Also update the value
 - If we add logging, we can add another endpoint, that returns log result in csv format. And also we can filter the log with the given json object in the request.
 -We can also create another logging mechanism for save error and infos. We can use log4net or custom log mechanism.






