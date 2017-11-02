<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

- [HLCrypter](#T-Gmtl-HandyLib-HLCrypter 'Gmtl.HandyLib.HLCrypter')
  - [DecryptString(encryptedText)](#M-Gmtl-HandyLib-HLCrypter-DecryptString-System-String- 'Gmtl.HandyLib.HLCrypter.DecryptString(System.String)')
  - [EncryptString(plainText)](#M-Gmtl-HandyLib-HLCrypter-EncryptString-System-String- 'Gmtl.HandyLib.HLCrypter.EncryptString(System.String)')
  - [SetEntropy(seed)](#M-Gmtl-HandyLib-HLCrypter-SetEntropy-System-String- 'Gmtl.HandyLib.HLCrypter.SetEntropy(System.String)')
- [HLDateTime](#T-Gmtl-HandyLib-HLDateTime 'Gmtl.HandyLib.HLDateTime')
  - [NowUnixTimestamp](#P-Gmtl-HandyLib-HLDateTime-NowUnixTimestamp 'Gmtl.HandyLib.HLDateTime.NowUnixTimestamp')
  - [FromUnixTimestamp(timestamp)](#M-Gmtl-HandyLib-HLDateTime-FromUnixTimestamp-System-Int64- 'Gmtl.HandyLib.HLDateTime.FromUnixTimestamp(System.Int64)')
  - [ToUnixTimestamp(dateTime)](#M-Gmtl-HandyLib-HLDateTime-ToUnixTimestamp-System-DateTime- 'Gmtl.HandyLib.HLDateTime.ToUnixTimestamp(System.DateTime)')
- [HLDllEmbeddedResource](#T-Gmtl-HandyLib-HLDllEmbeddedResource 'Gmtl.HandyLib.HLDllEmbeddedResource')
  - [GetTextResource(resourceName,assembly)](#M-Gmtl-HandyLib-HLDllEmbeddedResource-GetTextResource-System-String,System-Reflection-Assembly- 'Gmtl.HandyLib.HLDllEmbeddedResource.GetTextResource(System.String,System.Reflection.Assembly)')
  - [GetTextResource(resourceName)](#M-Gmtl-HandyLib-HLDllEmbeddedResource-GetTextResource-System-String- 'Gmtl.HandyLib.HLDllEmbeddedResource.GetTextResource(System.String)')
- [HLExceptionHelper](#T-Gmtl-HandyLib-HLExceptionHelper 'Gmtl.HandyLib.HLExceptionHelper')
  - [ToXmlString()](#M-Gmtl-HandyLib-HLExceptionHelper-ToXmlString-System-Exception,System-Boolean- 'Gmtl.HandyLib.HLExceptionHelper.ToXmlString(System.Exception,System.Boolean)')
- [HLListPage\`1](#T-Gmtl-HandyLib-HLListPage`1 'Gmtl.HandyLib.HLListPage`1')
- [HLObjectExtensions](#T-Gmtl-HandyLib-Extensions-HLObjectExtensions 'Gmtl.HandyLib.Extensions.HLObjectExtensions')
  - [PropertyList(obj)](#M-Gmtl-HandyLib-Extensions-HLObjectExtensions-PropertyList-System-Object- 'Gmtl.HandyLib.Extensions.HLObjectExtensions.PropertyList(System.Object)')
- [HLRandomizer](#T-Gmtl-HandyLib-HLRandomizer 'Gmtl.HandyLib.HLRandomizer')
  - [RandomDouble](#P-Gmtl-HandyLib-HLRandomizer-RandomDouble 'Gmtl.HandyLib.HLRandomizer.RandomDouble')
  - [RandomString](#P-Gmtl-HandyLib-HLRandomizer-RandomString 'Gmtl.HandyLib.HLRandomizer.RandomString')
- [HLSingleton\`1](#T-Gmtl-HandyLib-HLSingleton`1 'Gmtl.HandyLib.HLSingleton`1')
  - [Instance](#P-Gmtl-HandyLib-HLSingleton`1-Instance 'Gmtl.HandyLib.HLSingleton`1.Instance')
- [HLString](#T-Gmtl-HandyLib-HLString 'Gmtl.HandyLib.HLString')
  - [ValueOrDefault()](#M-Gmtl-HandyLib-HLString-ValueOrDefault-System-String,System-String- 'Gmtl.HandyLib.HLString.ValueOrDefault(System.String,System.String)')
  - [ValueOrEmpty()](#M-Gmtl-HandyLib-HLString-ValueOrEmpty-System-String- 'Gmtl.HandyLib.HLString.ValueOrEmpty(System.String)')
- [OperationalStatus](#T-Gmtl-HandyLib-OperationalStatus 'Gmtl.HandyLib.OperationalStatus')
- [OperationResult\`1](#T-Gmtl-HandyLib-OperationResult`1 'Gmtl.HandyLib.OperationResult`1')
  - [Message](#P-Gmtl-HandyLib-OperationResult`1-Message 'Gmtl.HandyLib.OperationResult`1.Message')
  - [Result](#P-Gmtl-HandyLib-OperationResult`1-Result 'Gmtl.HandyLib.OperationResult`1.Result')
  - [Status](#P-Gmtl-HandyLib-OperationResult`1-Status 'Gmtl.HandyLib.OperationResult`1.Status')

<a name='assembly'></a>
# Gmtl.HandyLib [#](#assembly 'Go To Here') [=](#contents 'Back To Contents')

<a name='T-Gmtl-HandyLib-HLCrypter'></a>
## HLCrypter [#](#T-Gmtl-HandyLib-HLCrypter 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

HLCrypter encrypt and decrypt strings

<a name='M-Gmtl-HandyLib-HLCrypter-DecryptString-System-String-'></a>
### DecryptString(encryptedText) `method` [#](#M-Gmtl-HandyLib-HLCrypter-DecryptString-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypt string

##### Returns

decrypted text using entropy seed

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| encryptedText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | text to decrypt |

<a name='M-Gmtl-HandyLib-HLCrypter-EncryptString-System-String-'></a>
### EncryptString(plainText) `method` [#](#M-Gmtl-HandyLib-HLCrypter-EncryptString-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypt string

##### Returns

encrypted text using entropy seed

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| plainText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | text to encrypt |

<a name='M-Gmtl-HandyLib-HLCrypter-SetEntropy-System-String-'></a>
### SetEntropy(seed) `method` [#](#M-Gmtl-HandyLib-HLCrypter-SetEntropy-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Set seed for encryption and decryption

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| seed | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | new seed |

<a name='T-Gmtl-HandyLib-HLDateTime'></a>
## HLDateTime [#](#T-Gmtl-HandyLib-HLDateTime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Handy methods related to System.DateTime

<a name='P-Gmtl-HandyLib-HLDateTime-NowUnixTimestamp'></a>
### NowUnixTimestamp `property` [#](#P-Gmtl-HandyLib-HLDateTime-NowUnixTimestamp 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Return Linux timestamp for provided date

##### Remarks

```
int unixTimestamp = HLDateTime.NowUnixTimestamp;
```

<a name='M-Gmtl-HandyLib-HLDateTime-FromUnixTimestamp-System-Int64-'></a>
### FromUnixTimestamp(timestamp) `method` [#](#M-Gmtl-HandyLib-HLDateTime-FromUnixTimestamp-System-Int64- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Convert unix timestamp to System.DateTime

##### Returns

DateTime representation of unix timestamp

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timestamp | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | Unix timestamp to be converted |

<a name='M-Gmtl-HandyLib-HLDateTime-ToUnixTimestamp-System-DateTime-'></a>
### ToUnixTimestamp(dateTime) `method` [#](#M-Gmtl-HandyLib-HLDateTime-ToUnixTimestamp-System-DateTime- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Convert System.DateTime to unix timestamp

##### Returns

unix timestamp representation for DateTime

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateTime | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | DateTime struct to be converted |

<a name='T-Gmtl-HandyLib-HLDllEmbeddedResource'></a>
## HLDllEmbeddedResource [#](#T-Gmtl-HandyLib-HLDllEmbeddedResource 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

<a name='M-Gmtl-HandyLib-HLDllEmbeddedResource-GetTextResource-System-String,System-Reflection-Assembly-'></a>
### GetTextResource(resourceName,assembly) `method` [#](#M-Gmtl-HandyLib-HLDllEmbeddedResource-GetTextResource-System-String,System-Reflection-Assembly- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Return content of the file

##### Returns

content of the file

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resourceName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | path to the file included as embedded resource. IMPORTANT: replace '\\' with '.' eg. Scripts\\run.bat => Scripts.run.bat |
| assembly | [System.Reflection.Assembly](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.Assembly 'System.Reflection.Assembly') | Assembly with resource |

<a name='M-Gmtl-HandyLib-HLDllEmbeddedResource-GetTextResource-System-String-'></a>
### GetTextResource(resourceName) `method` [#](#M-Gmtl-HandyLib-HLDllEmbeddedResource-GetTextResource-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Return content of the file in calling assembly

##### Returns

content of the file

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resourceName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | path to the file included as embedded resource. IMPORTANT: replace '\\' with '.' eg. Scripts\\run.bat => Scripts.run.bat |

<a name='T-Gmtl-HandyLib-HLExceptionHelper'></a>
## HLExceptionHelper [#](#T-Gmtl-HandyLib-HLExceptionHelper 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Handy methods related to System.Exception

<a name='M-Gmtl-HandyLib-HLExceptionHelper-ToXmlString-System-Exception,System-Boolean-'></a>
### ToXmlString() `method` [#](#M-Gmtl-HandyLib-HLExceptionHelper-ToXmlString-System-Exception,System-Boolean- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Return XML serialized string of Exception provided in parameter

##### Parameters

This method has no parameters.

<a name='T-Gmtl-HandyLib-HLListPage`1'></a>
## HLListPage\`1 [#](#T-Gmtl-HandyLib-HLListPage`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Provides base class for 'pagination'

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | List item type |

<a name='T-Gmtl-HandyLib-Extensions-HLObjectExtensions'></a>
## HLObjectExtensions [#](#T-Gmtl-HandyLib-Extensions-HLObjectExtensions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib.Extensions

##### Summary

Usefull extensions for System.Object

<a name='M-Gmtl-HandyLib-Extensions-HLObjectExtensions-PropertyList-System-Object-'></a>
### PropertyList(obj) `method` [#](#M-Gmtl-HandyLib-Extensions-HLObjectExtensions-PropertyList-System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

List all properties and their values

##### Returns

List of all properies and their values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='T-Gmtl-HandyLib-HLRandomizer'></a>
## HLRandomizer [#](#T-Gmtl-HandyLib-HLRandomizer 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Handy random data provider

<a name='P-Gmtl-HandyLib-HLRandomizer-RandomDouble'></a>
### RandomDouble `property` [#](#P-Gmtl-HandyLib-HLRandomizer-RandomDouble 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Random double provider

##### Remarks

```
double randomDouble = HLRandomizer.RandomDouble.Next(1.0, 100.0);
```

<a name='P-Gmtl-HandyLib-HLRandomizer-RandomString'></a>
### RandomString `property` [#](#P-Gmtl-HandyLib-HLRandomizer-RandomString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Random string provider

##### Remarks

```
string randomString1 = HLRandomizer.RandomString.Next();
            string randomString2 = HLRandomizer.RandomString.Next(100);//max length
            string randomString3 = HLRandomizer.RandomString.NextExact(10);
            string randomString4 = HLRandomizer.RandomString.Next(10, 100);//min 100 and max 100
```

<a name='T-Gmtl-HandyLib-HLSingleton`1'></a>
## HLSingleton\`1 [#](#T-Gmtl-HandyLib-HLSingleton`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

<a name='P-Gmtl-HandyLib-HLSingleton`1-Instance'></a>
### Instance `property` [#](#P-Gmtl-HandyLib-HLSingleton`1-Instance 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Class instance

<a name='T-Gmtl-HandyLib-HLString'></a>
## HLString [#](#T-Gmtl-HandyLib-HLString 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Handy methods related to System.String

<a name='M-Gmtl-HandyLib-HLString-ValueOrDefault-System-String,System-String-'></a>
### ValueOrDefault() `method` [#](#M-Gmtl-HandyLib-HLString-ValueOrDefault-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns input if it's not null or whitespace, defaultValue otherwise

##### Parameters

This method has no parameters.

##### Remarks

```
HLString.ValueOrDefault("test", "replaced"); //'test' returned
            HLString.ValueOrDefault("", "replaced"); //'replaced' returned
            HLString.ValueOrDefault(null, "replaced"); //'replaced' returned
```

<a name='M-Gmtl-HandyLib-HLString-ValueOrEmpty-System-String-'></a>
### ValueOrEmpty() `method` [#](#M-Gmtl-HandyLib-HLString-ValueOrEmpty-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns input if it's not null or whitespace, String.Empty otherwise

##### Parameters

This method has no parameters.

##### Remarks

```
HLString.ValueOrEmpty("test"); //'test' returned
            HLString.ValueOrEmpty(null); //'String.Empty' returned
```

<a name='T-Gmtl-HandyLib-OperationalStatus'></a>
## OperationalStatus [#](#T-Gmtl-HandyLib-OperationalStatus 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Status of executed operation

<a name='T-Gmtl-HandyLib-OperationResult`1'></a>
## OperationResult\`1 [#](#T-Gmtl-HandyLib-OperationResult`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Gmtl.HandyLib

##### Summary

Generic class working as a wrapper

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | type |

<a name='P-Gmtl-HandyLib-OperationResult`1-Message'></a>
### Message `property` [#](#P-Gmtl-HandyLib-OperationResult`1-Message 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Extra info send with operation result (optional)

<a name='P-Gmtl-HandyLib-OperationResult`1-Result'></a>
### Result `property` [#](#P-Gmtl-HandyLib-OperationResult`1-Result 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Result value

<a name='P-Gmtl-HandyLib-OperationResult`1-Status'></a>
### Status `property` [#](#P-Gmtl-HandyLib-OperationResult`1-Status 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Operation status
