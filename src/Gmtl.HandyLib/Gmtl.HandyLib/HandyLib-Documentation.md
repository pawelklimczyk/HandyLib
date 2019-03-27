
# Gmtl.HandyLib


## Extensions.HLObjectExtensions

Usefull extensions for System.Object


### M:Gmtl.HandyLib.PropertyList(obj)

List all properties and their values

| Name | Description |
| ---- | ----------- |
| obj | *System.Object*<br> |


#### Returns

List of all properies and their values


## HLCloner

Clones an object


## HLCrypter

HLCrypter encrypt and decrypt strings


### M:Gmtl.HandyLib.DecryptString(encryptedText)

Decrypt string

| Name | Description |
| ---- | ----------- |
| encryptedText | *System.String*<br>text to decrypt |


#### Returns

decrypted text using entropy seed


### M:Gmtl.HandyLib.EncryptString(plainText)

Encrypt string

| Name | Description |
| ---- | ----------- |
| plainText | *System.String*<br>text to encrypt |


#### Returns

encrypted text using entropy seed


### M:Gmtl.HandyLib.SetEntropy(seed)

Set seed for encryption and decryption

| Name | Description |
| ---- | ----------- |
| seed | *System.String*<br>new seed |

## HLDateTime

Handy methods related to System.DateTime


### M:Gmtl.HandyLib.FromUnixTimestamp(timestamp)

Convert unix timestamp to System.DateTime

| Name | Description |
| ---- | ----------- |
| timestamp | *System.Int64*<br>Unix timestamp to be converted |


#### Returns

DateTime representation of unix timestamp


### .NowUnixTimestamp

Return Linux timestamp for provided date


#### Remarks

 int unixTimestamp = HLDateTime.NowUnixTimestamp; 


### M:Gmtl.HandyLib.ToUnixTimestamp(dateTime)

Convert System.DateTime to unix timestamp

| Name | Description |
| ---- | ----------- |
| dateTime | *System.DateTime*<br>DateTime struct to be converted |


#### Returns

unix timestamp representation for DateTime


### M:Gmtl.HandyLib.HLDllEmbeddedResource.GetTextResource(resourceName)

Return content of the file in calling assembly

| Name | Description |
| ---- | ----------- |
| resourceName | *System.String*<br> path to the file included as embedded resource. IMPORTANT: replace '\\' with '.' eg. Scripts\\run.bat => Scripts.run.bat |


#### Returns

content of the file


### M:Gmtl.HandyLib.HLDllEmbeddedResource.GetTextResource(resourceName, assembly)

Return content of the file

| Name | Description |
| ---- | ----------- |
| resourceName | *System.String*<br> path to the file included as embedded resource. IMPORTANT: replace '\\' with '.' eg. Scripts\\run.bat => Scripts.run.bat |
| assembly | *System.Reflection.Assembly*<br>Assembly with resource |


#### Returns

content of the file


## T:Gmtl.HandyLib.HLExceptionHelper

Handy methods related to System.Exception


### M:Gmtl.HandyLib.HLExceptionHelper.ToXmlString(System.Exception,System.Boolean)

Return XML serialized string of Exception provided in parameter


## T:Gmtl.HandyLib.HLListPage`1

Provides base class for 'pagination'


## T:Gmtl.HandyLib.HLRandomizer

Handy random data provider


### P:Gmtl.HandyLib.HLRandomizer.RandomDouble

Random double provider


#### Remarks

 double randomDouble = HLRandomizer.RandomDouble.Next(1.0, 100.0); 


### P:Gmtl.HandyLib.HLRandomizer.RandomString

Random string provider


#### Remarks

 string randomString1 = HLRandomizer.RandomString.Next(); string randomString2 = HLRandomizer.RandomString.Next(100);//max length string randomString3 = HLRandomizer.RandomString.NextExact(10); string randomString4 = HLRandomizer.RandomString.Next(10, 100);//min 100 and max 100 


## T:Gmtl.HandyLib.HLSerializer

Serializaton and deserialization helper class


### M:Gmtl.HandyLib.HLSerializer.DeserializeFromXml``1(xml)

Deserializes string to specified object type

| Name | Description |
| ---- | ----------- |
| xml | *System.String*<br>serialized object |


#### Returns

Deserialized object


### M:Gmtl.HandyLib.HLSerializer.DeserializeFromXmlFile``1(filename)

Deserializes object from specified filename

| Name | Description |
| ---- | ----------- |
| filename | *System.String*<br>File storing serialized object |


#### Returns

Deserialized object


### M:Gmtl.HandyLib.HLSerializer.SerializeToXml``1(objectToSerialize, useNamespaces)

Serializes object to XML

| Name | Description |
| ---- | ----------- |
| objectToSerialize | *``0*<br>Object to serialize |
| useNamespaces | *System.Boolean*<br>If true, adds namespaces to output string |


#### Returns

Serialized object


### M:Gmtl.HandyLib.HLSerializer.SerializeToXmlFile``1(objectToSerialize, useNamespaces, filename)

Serializes object and saves it into a file

| Name | Description |
| ---- | ----------- |
| objectToSerialize | *``0*<br>Object to serialize |
| useNamespaces | *System.String*<br>If true, adds namespaces to output string |
| filename | *System.Boolean*<br>Filename where save the serialized object |

### P:Gmtl.HandyLib.HLSingleton`1.Instance

Class instance


## T:Gmtl.HandyLib.HLString

Handy methods related to System.String


### M:Gmtl.HandyLib.HLString.ValueOrDefault(System.String,System.String)

Returns input if it's not null or whitespace, defaultValue otherwise


#### Remarks

 HLString.ValueOrDefault("test", "replaced"); //'test' returned HLString.ValueOrDefault("", "replaced"); //'replaced' returned HLString.ValueOrDefault(null, "replaced"); //'replaced' returned 


### M:Gmtl.HandyLib.HLString.ValueOrEmpty(System.String)

Returns input if it's not null or whitespace, String.Empty otherwise


#### Remarks

 HLString.ValueOrEmpty("test"); //'test' returned HLString.ValueOrEmpty(null); //'String.Empty' returned 


### M:Gmtl.HandyLib.IHLMailNotifier.TestConfiguration

Will try to send message to itself


#### Returns




## T:Gmtl.HandyLib.OperationResult`1

Generic class working as a wrapper


### P:Gmtl.HandyLib.OperationResult`1.Message

Extra info send with operation result (optional)


### P:Gmtl.HandyLib.OperationResult`1.Result

Result value


### P:Gmtl.HandyLib.OperationResult`1.Status

Operation status


## T:Gmtl.HandyLib.OperationStatus

Status of executed operation

