# MCPing.NET
MCPint.NET (Minecraft Pink NET) is a DotNET solution for getting basic information about the Minecraft server supporting Minecraft Query

### Query
This method uses GameSpy4 protocol, and requires enabling `query` listener in your `server.properties` like this:

> *enable-query=true*<br>
> *query.port=25565*

## Example
```C#
using GamerVII.MCPing.Status;
using GamerVII.MCPing.Status.Models;

...

MCPing minecarftPing = new MCPing("YOUR_IP_ADDRESS");
var status = minecarftPing.GetStatus();
```

## License
[MIT](LICENSE)
