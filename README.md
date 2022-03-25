# Altinn.ApiClients.Dan

## About
This is a .NET5 client for [data.altinn.no](https://data.altinn.no) APIs that makes it easy to consume [published datasets](https://altinn.github.io/docs/utviklingsguider/data.altinn.no/beviskoder/) in your .NET5 based integrations.

## Installation

Get latest release from [nuget.org](https://www.nuget.org/packages/Altinn.ApiClients.Dan). Pre-release packages are available on [Github](https://github.com/Altinn/altinn-apiclient-dan/packages/)

## Usage

Altinn.ApiClients.Dan relies on [Altinn.ApiClients.Maskinporten](https://github.com/Altinn/altinn-apiclient-maskinporten/) for authentication against data.altinn.no. You will need to configure a client provisioned with the scopes you need (at minimum `altinn:dataaltinnno`, which is openly available), and configure a secret (enterprise certificate or RSA private key) used for signing requests to Maskinporten. See [Altinn.ApiClients.Maskinporten](https://github.com/Altinn/altinn-apiclient-maskinporten/) documentation on the different options available and how you can implement your custom provider if needed. In this example, we use a private key within a PKCS#12-formatted file.

### Add configuration

The IOptions-pattern is used for loading configuration. You need two sections - one for data.altinn.no (subscription key and environment) and one for configuring the Maksinporten-client.Add the sections to your appsettings.json (or other configuration store). You can name the sections what you want; you'll refer them in the next step.

```jsonc
{
  "MyMaskinportenSettings": {
      // "ver2" is test environment, use "prod" for production. 
      // Use "ver2" for both non-production Dan-environments
      "Environment": "ver2", 
      "ClientId": "12345678-abcd-def01-2345-67890123456",
      "Scope": "altinn:dataaltinnno", // add additional scopes here space separated
      "CertificatePkcs12Path": "path/to/your/cert-with-privatekey.p12",
      "CertificatePkcs12Password": "password-for-p12-file"
  },
  "MyDanSettings": {
      // "dev" is the development environment for bleeding edge, 
      // "staging" is the pre-prod environment and "prod" is production
      "Environment": "dev",
      "SubscriptionKey": "08f396cb9162478b9fd00daa3e161ef6"
  }
}
```

### Configure service

In Startup.cs, add the following to your `ConfigureServices`-method:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Configuration for DAN (environment and subscription key)
    services.Configure<DanSettings>(Configuration.GetSection("MyDanSettings"));
  
    // Configuration for Maskinporten, using a local PKCS#12 file containing 
    // private certificate for signing Maskinporten requests
    services.Configure<MaskinportenSettings<Pkcs12ClientDefinition>>(
      Configuration.GetSection("MyMaskinportenSettings"));
  
    // This registers an IDanClient for injection in your application code
    services.AddDanClient<Pkcs12ClientDefinition>();

    // This does the same, but configures it to use Newtonsoft.Json instead of
    // the default System.Text.Json deserializer using custom serializer settings
    /*
    services.AddDanClient<Pkcs12ClientDefinition>(sp => new DanConfiguration
    {
        // Use Newtonsoft.Json instead of System.Text.Json
        Deserializer = new JsonNetDeserializer {
            SerializerSettings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy_MM_dd"
            }
        }
    });
    */
}
```

### Use in application code

```csharp
public class MyDanClient 
{
    private readonly IDanClient _danClient;
    
    public MyDanClient(IDanClient danClient)
    {
        _danClient = danClient;
    }

    public void DoStuff()
    {
        var datasetname = "UnitBasicInformation";
        var subject = "991825827";
        
        // Gets a generic dataset which can be iterated
        DataSet dataset = await _danClient.GetDataSet(datasetname, subject);
        foreach (var dsv in dataset.Values)
        {
          // Do something with dsv.Name and dsv.Value
        }

        // If you have a model for the dataset you're getting you can map it directly
        UnitBasicInformation result = await _danClient.GetDataSet<UnitBasicInformation>(datasetname, subject);
        
    }
}
```
See `SampleWebApp` for more usage examples.
