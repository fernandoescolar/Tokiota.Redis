<h1 id="console">Tokiota.Redis.Console</h1>
<p>To use the redis client console tool don't forget to change the connection values in the "Tokiota.Redis.Console.config" file:</p>
```xml
  <appSettings>
    <add key="Host" value="tokiota.redis.cache.windows.net" />
    <add key="Port" value="6380" />
    <add key="UseSsl" value="true" />
  </appSettings>
```
<p>Them execute the Tokiota.Redis.Console.exe and set your credentials if you need it:</p>
```
> AUTH #mypassword#
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
