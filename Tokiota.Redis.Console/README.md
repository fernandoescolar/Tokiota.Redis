<h1 id="console">Tokiota.Redis.Console</h1>
<p>To use the redis client console tool you could specify the connection settings in the command parameters:</p>
```
# connect with localhost on 6379
Tokiota.Redis.Console.exe 
# connect with myhost.redis.com on 6379
Tokiota.Redis.Console.exe myhost.redis.com
# connect with myhost.redis.com on 6380
Tokiota.Redis.Console.exe myhost.redis.com 6380
# connect with myhost.redis.com on 6380 and auto auth the the specified password
Tokiota.Redis.Console.exe myhost.redis.com 6380 mypassword
```
<p>Or you can set the connection settings in the "Tokiota.Redis.Console.config" file:</p>
```xml
  <appSettings>
    <add key="Host" value="tokiota.redis.cache.windows.net" />
    <add key="Port" value="6380" />
    <add key="Password" value="mypassword" />
  </appSettings>
```
<p>Them execute the Tokiota.Redis.Console.exe and set your credentials if you need it:</p>
```
> AUTH ********
+ OK
> SET my:key:1 hello
+ OK
> GET my:set:1
hello
> QUIT
+ OK
Press enter to exit...
```
<p>You can download the source code or the <a href="https://github.com/fernandoescolar/Tokiota.Redis/blob/master/Tokiota.Redis.Console%20binaries.zip">binaries</a>.</p>
