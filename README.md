# NetClient.Rest

[![Build](https://ci.appveyor.com/api/projects/status/r6t2rvc0qru109ni?svg=true)](https://ci.appveyor.com/project/skthomasjr/netclient-rest)
[![Release](https://img.shields.io/github/release/skthomasjr/NetClient.Rest.svg?maxAge=2592000)](https://github.com/skthomasjr/NetClient.Rest/releases)
[![NuGet](https://img.shields.io/nuget/v/NetClient.Rest.svg)](https://www.nuget.org/packages/NetClient.Rest)
[![License](https://img.shields.io/github/license/skthomasjr/NetClient.Rest.svg?maxAge=2592000)](LICENSE.md)
[![Author](https://img.shields.io/badge/author-Scott%20K.%20Thomas%2C%20Jr.-blue.svg?maxAge=2592000)](https://www.linkedin.com/in/skthomasjr)
[![Join the chat at https://gitter.im/skthomasjr/NetClient.Rest](https://badges.gitter.im/skthomasjr/NetClient.Rest.svg)](https://gitter.im/skthomasjr/NetClient.Rest?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

An asynchronous REST API client that allows you to make API calls using LINQ syntax. Note: Add, edit, and delete functionality is coming soon.

Create a class that models the return data. Support for JsonSerializationSettings is built-in for more precise control of how data is serialized and deserialized.
```c#
public class RawBlock
{
  public int Block_Index { get; set; }
  
  public string Hash { get; set; }
}
```
Create a client for the API you want to abstract.
```c#
[BaseUri("https://blockchain.info")]
public class BlockchainClient : RestClient
{
  [Route("/rawblock/{Block_Index}")]
  public Resource<RawBlock> RawBlocks { get; set; }
}
```
Use linq syntax to interact with the API.
```c#
private static void Main(string[] args)
{
  var client = new BlockchainClient { OnError = ex => Console.WriteLine(ex.Message) };
  var rawBlocks = from r in client.RawBlocks where r.Block_Index == 417260 select r;
  var rawBlock = rawBlocks.ToArray().SingleOrDefault();
  
  Console.WriteLine(rawBlock?.Block_Index);
  Console.ReadKey();
}
```
