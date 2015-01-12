# Tokiota.Redis
A basic redis client for .net<br/>
Version: Alpha-WIP (not ready for production environments)<br/>
based on https://github.com/migueldeicaza/redis-sharp<br/>
To use the command prompt tool, go to <a href="#console">Tokiota.Redis.Console section bellow</a>.
<h2>Usage</h2>
<p>If you want to use this redis library you should instantiate a IRedisClient object.</p>
<p>For example:</p>
```csharp
var redis = new RedisClient("my.redis.cache.windows.net", 6380, "#password#");
// do something
```
<p>Or using the client pool:</p>
```csharp
IRedisClientFactory factory = new RedisClientFactory("my.redis.cache.windows.net", 6380, "#password#");
using (var redis = factory.GetCurrent())
{
   // do something
}
```
<h3>Connection commands</h3>
<p>You can access to the 'connection' commands using the Connection redis client property:</p>
```csharp
if (redis.Connection.Ping()
{
  redis.Connection.Echo("selecting db 2");
  redis.Connection.Select(2);
}
```
<p>Currently all the official connection commands (http://redis.io/commands/#connection) are working.</p>
<h3>Keys commands</h3>
<p>You can access to the 'keys' commands using the Keys redis client property:</p>
```csharp
if (redis.Keys.Exists("my:key:1")
{
  redis.Keys.Del("my:key:1");
}

if (redis.Keys.Exists("my:key:2")
{
  redis.Keys.Expire("my:key:2", 120 /*seconds*/);
}
```
<p>Currently all the official keys commands (http://redis.io/commands/#generic) are working, except SORT and SCAN.</p>
<h3>Strings commands</h3>
<p>You can access to the 'strings' commands using the Strings redis client property:</p>
```csharp
if (redis.Keys.Exists("my:key:1")
{
  redis.Strings.Incr("my:key:1");
}

if (!redis.Keys.Exists("my:key:2")
{
  redis.Strings.Set("my:key:2", "hi redis!");
}

Console.WriteLine(redis.Strings.GetString("my:key:2"));
```
<p>Currently all the official strings commands (http://redis.io/commands/#string) are working.</p>
<h3>Hashes commands</h3>
<p>Currently all the official keys commands (http://redis.io/commands/#hash) are working, except HSCAN.</p>
<h3>Lists commands</h3>
<p>Currently all the official keys commands (http://redis.io/commands/#list) are working.</p>
<h3>Sets commands</h3>
<p>Currently all the official keys commands (http://redis.io/commands/#set) are working, except SSCAN.</p>
<h3>Sorted Sets commands</h3>
<p>Not implemented...</p>
<h3>Transactions commands</h3>
<p>Not implemented...</p>
<h3>Scripting commands</h3>
<p>Not implemented...</p>
<h3>Pub/Sub commands</h3>
<p>Not implemented...</p>

<h2>Cache demo</h2>
```csharp
public class RedisCache
{
  private static readonly IRedisClientFactory factory = new RedisClientFactory("my.redis.cache.windows.net", 6380, "#password#");
  
  public T Get<T>(string key)
  {
      using (var redis = factory.GetCurrent())
      {
          if (typeof(T) == typeof(string)) return (T)(object)redis.Strings.GetString(key);
          if (typeof(T) == typeof(byte[])) return (T)(object)redis.Strings.Get(key);

          var data = redis.Strings.Get(key);
          return Deserialize<T>(data);
      }
  }
  
  public void Set<T>(string key, T value, TimeSpan? expiration = null)
  {
      using (var redis = factory.GetCurrent())
      {
          byte[] bytes = null;
          if (typeof(T) == typeof(string)) bytes = value.ToString().ToByteArray();
          if (typeof(T) == typeof(byte[])) bytes = (byte[])(object)value;
          if (bytes == null) bytes = Serialize<T>(value);

          if (expiration.HasValue)
              redis.Strings.Set(key, bytes, (int)expiration.Value.TotalSeconds);
          else
              redis.Strings.Set(key, bytes);
      }
  }
  
  private static byte[] Serialize<T>(T value);
  
  private static T Deserialize<T>(byte[] value);
}
```
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
